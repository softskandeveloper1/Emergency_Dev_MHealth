using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using MHealth.Data.ViewModel;
using MHealth.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IProfileService _profileService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IEmailSender _emailSender;
        private readonly IPharmacyService _pharmacyService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PrescriptionController(IClinicianService clinicianService, UserManager<ApplicationUser> userManager, IProfileService profileService, IPrescriptionService prescriptionService, IEmailSender emailSender, IPharmacyService pharmacyService, RoleManager<IdentityRole> roleManager)
        {
            _clinicianService = clinicianService;
            _userManager = userManager;
            _profileService = profileService;
            _prescriptionService = prescriptionService;
            _emailSender = emailSender;
            _pharmacyService = pharmacyService;
            _roleManager = roleManager;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {

            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);



            var prescription_models = new List<PrescriptionModel>();

            if (await _userManager.IsInRoleAsync(user, "client"))
            {

                var profile = _profileService.Get().FirstOrDefault(e => e.user_id == user.Id);

                var prescriptions = _prescriptionService.Get().Where(e => e.profile_id == profile.id).Include(e => e.clinician_).Include(e => e.profile_).Include(e => e.mp_prescription_drug);


                foreach (var prescription in prescriptions)
                {
                    var pharmacy = prescription.pharmacy_id.HasValue ? _pharmacyService.GetById(prescription.pharmacy_id.Value) : new mp_pharmacy();
                    prescription_models.Add(new PrescriptionModel(prescription, pharmacy));
                }

            }
            else if (await _userManager.IsInRoleAsync(user, "clinician"))
            {

                var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user.Id);
                var prescriptions = _prescriptionService.Get().Where(e => e.clinician_id == clinician.id).Include(e => e.profile_).Include(e => e.clinician_).Include(e => e.mp_prescription_drug);

                foreach (var prescription in prescriptions)
                {
                    var pharmacy = prescription.pharmacy_id.HasValue ? _pharmacyService.GetById(prescription.pharmacy_id.Value) : new mp_pharmacy();
                    prescription_models.Add(new PrescriptionModel(prescription, pharmacy));
                }
            }

            return Ok(prescription_models);
        }

        public async Task<IActionResult> Post(PrescriptionModel model)
        {
            var prescription = new mp_prescription();

            prescription.clinician_id = model.clinician_id;
            prescription.profile_id = model.profile_id;
            prescription.pharmacy_id = model.pharmacy_id;
            prescription.comment = model.comment;
            prescription.created_by = "";

            var prescription_id = _prescriptionService.AddPrescription(prescription);


            var drug_text = "";

            var prescription_drugs = new List<mp_prescription_drug>();
            for (var i = 0; i < model.drugs.Count; i++)
            {
                prescription_drugs.Add(new mp_prescription_drug
                {
                    drug = model.drugs[i].drug,
                    dosage = model.drugs[i].dosage,
                    prescription_id = prescription_id
                });

                drug_text += model.drugs[i].drug + " " + model.drugs[i].dosage + ",";
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
                    await _emailSender.SendEmailAsync(pharmacy.email, "New Prescription - MySpace MyTime",
                 $"The following prescriptions have been sent for " + profile.last_name + " " + profile.first_name + " - " + profile.unique_id.ToString("D10") + " .<br/>" + drug_text);
                }
            }

            return Ok(200);
        }
    }
}