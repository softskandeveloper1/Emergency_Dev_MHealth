using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using MHealth.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class PrescriptionController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IProfileService _profileService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IEmailSender _emailSender;
        private readonly IPharmacyService _pharmacyService;

        public PrescriptionController(IClinicianService clinicianService, UserManager<ApplicationUser> userManager, IProfileService profileService, IPrescriptionService prescriptionService, IEmailSender emailSender, IPharmacyService pharmacyService)
        {
            _clinicianService = clinicianService;
            _userManager = userManager;
            _profileService = profileService;
            _prescriptionService = prescriptionService;
            _emailSender = emailSender;
            _pharmacyService = pharmacyService;
        }

        

        public IActionResult MyPrescriptions()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.Get().FirstOrDefault(e => e.user_id == user_id);
            var prescriptions = _prescriptionService.Get().Where(e => e.profile_id == profile.id).Include(e => e.clinician_).Include(e => e.mp_prescription_drug);
            return View(prescriptions);
          
        }

        public IActionResult Prescriptions(string memberid=null)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
            var prescriptions = _prescriptionService.Get().Where(e => e.clinician_id ==clinician.id).Include(e=>e.profile_).Include(e=>e.mp_prescription_drug);
            return View(prescriptions);
        }

        public IActionResult Detail(long id)
        {
            return View(_prescriptionService.Get().Include(e => e.profile_).Include(e => e.mp_prescription_drug).FirstOrDefault(e => e.id == id));
        }

        public IActionResult NewPrescription(Guid? profile_id)
        {
            if (profile_id.HasValue)
            {
                var profile = _profileService.Get(profile_id.Value);
                return View(profile);
            }
            return View(new mp_profile());
        }

        [HttpPost]
        public async Task<IActionResult> PostPrescription(mp_prescription prescription)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
            prescription.clinician_id = clinician.id;
            prescription.created_by = user_id;

            var prescription_id = _prescriptionService.AddPrescription(prescription);

            var collection = Request.Form;
            var drugs = collection["drug"].ToList();
            var dosages = collection["dosage"].ToList();

            var drug_text = "";

            var prescription_drugs = new List<mp_prescription_drug>();
            for (var i = 0; i < drugs.Count; i++)
            {
                prescription_drugs.Add(new mp_prescription_drug
                {
                    drug = drugs[i],
                    dosage = dosages[i],
                    prescription_id = prescription_id
                });

                drug_text += drugs[i] + " " + dosages[i] + ",";
            }

            _prescriptionService.AddPrescriptionDrugs(prescription_drugs);

            //get the profile information
            var profile = _profileService.Get(prescription.profile_id);

            var notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 7,
                read = 0,
                user_id = profile.user_id,
                notification = "Hi " + profile.last_name + " " + profile.first_name + ", Your prescription information has been updated, check your prescriptions for the details.",
                title = "New Prescription"
            };

            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(profile.email, "New Prescription - MySpace MyTime",
                  $"Hi " + profile.last_name + " " + profile.first_name + ", Your prescription information has been updated, check your prescriptions for the details.");

            if (prescription.pharmacy_id.HasValue)
            {
                //get the email of the pharmacy
                var pharmacy = _pharmacyService.Get().FirstOrDefault(e => e.id == prescription.pharmacy_id.Value);
                if (pharmacy != null && !string.IsNullOrEmpty(pharmacy.email))
                {
                    await _emailSender.SendEmailAsync(profile.email, "New Prescription - MySpace MyTime",
                 $"The following prescriptions have been sent for " + profile.last_name + " " + profile.first_name + " - " + profile.unique_id.ToString("D10") + " .<br/>" + drug_text);
                }
            }

            return Ok(200);
        }
    }
}