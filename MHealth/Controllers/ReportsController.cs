﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;
using Microsoft.AspNetCore.Identity;
using DAL.Utils;
using Microsoft.AspNetCore.Authorization;
using MHealth.Data;
using Microsoft.AspNetCore.Http;
using MHealth.Data.ViewModel;
using MHealth.Helper;

namespace MHealth.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IProfileService _profileService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly ICreditService _creditService;
        private readonly IAppointmentService _appointmentService;

        public ReportsController(IClinicianService clinicianService, UserManager<ApplicationUser> userManager, IProfileService profileService, IPrescriptionService prescriptionService, ICreditService creditService, IAppointmentService appointmentService)
        {
            _clinicianService = clinicianService;
            _userManager = userManager;
            _prescriptionService = prescriptionService;
            _profileService = profileService;
            _creditService = creditService;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> CliniciansViewsByClient(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _clinicianService.Get().Include(x => x.mp_profile_match);
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> AppointmentsCancelledByClients(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _profileService.Get().Include(x => x.mp_appointment).Where(e => e.profile_type == 1
            && e.mp_appointment != null && (e.mp_appointment.Count(x => x.status == 171 && x.cancelled_by == e.id) > 0));
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_profile>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> AppointmentsCancelledByClinicians(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _clinicianService.Get().Include(x => x.mp_appointment).Where(e => e.mp_appointment != null
            && (e.mp_appointment.Count(x => x.status == 171 && x.cancelled_by == e.id) > 0));
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> AppointmentsCompletedByClinicians(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _clinicianService.Get().Include(x => x.mp_appointment).Where(e => e.mp_appointment != null);
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> DailyAppointmentsByClinicians(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _clinicianService.Get().Include(x => x.mp_appointment).Where(e => e.mp_appointment != null && (e.mp_appointment.Count(x => x.status == 234 && x.clinician_id == e.id && x.start_date.Date == DateTime.Now.Date) > 0));
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> PrescriptionByClinicians(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _prescriptionService.Get().Include(x => x.clinician_).Where(x => x.clinician_ != null);
            return View(await PaginatedList<mp_prescription>.CreateAsync(profiles.OrderByDescending(e => e.created_at).AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> PaymentsByService(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _creditService.Get().Include(x => x.appointment_).Include(y => y.appointment_.appointment_sub_typeNavigation).Where(x => x.appointment_ != null && x.appointment_.appointment_sub_typeNavigation != null);
            return View(await PaginatedList<mp_credit>.CreateAsync(profiles.OrderByDescending(e => e.created_at).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> SessionClients(int? pageNumber)
        {
            int pageSize = 25;
            var profiles = _profileService.Get().Where(e => e.profile_type == 1).Include(x => x.mp_appointment);
            return View(await PaginatedList<mp_profile>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> SessionsByClient(Guid client_id, int? pageNumber)
        {
            int pageSize = 25;
            var appointments = _appointmentService.Get().Where(x => x.client_id == client_id).Include(e => e.clinician_)
                .Include(e => e.client_)
                .Include(e => e.mp_credit)
                .Include(e => e.appointment_typeNavigation)
                .Include(e => e.appointment_serviceNavigation);

            return View(await PaginatedList<mp_appointment>.CreateAsync(appointments.OrderByDescending(e => e.start_date).AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> ClientBeforeClinician(int? pageNumber)
        {
            int pageSize = 25;

            var profiles = _clinicianService.Get().Include(x => x.mp_appointment)
                .ThenInclude(z => z.mp_appointment_log).Where(e => e.mp_appointment != null
                && e.mp_appointment.Count(x => x.status == 170) > 0 && e.mp_appointment.Select(x => x.mp_appointment_log).Count() > 0);

            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> CliniciansRating(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _clinicianService.Get().Include(x => x.mp_appointment).Where(e => e.mp_appointment != null);
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

    }
}
