using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class RefundController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRefundService _appointmentRefundService;
        public RefundController(IAppointmentService appointmentService, IAppointmentRefundService appointmentRefundService)
        {
            _appointmentService = appointmentService;
            _appointmentRefundService = appointmentRefundService;
        }

        public async Task<IActionResult> Refunds(int? page)
        {
            int pageSize = 25;
            var refunds = _appointmentRefundService.Get()
                .Include(e => e.appointment_).ThenInclude(e=>e.mp_credit)
                .Include(e=>e.appointment_).ThenInclude(e=>e.client_)
                .Include(e => e.appointment_).ThenInclude(e => e.clinician_)
                .Include(e => e.appointment_).ThenInclude(e => e.appointment_typeNavigation)
                .Include(e => e.appointment_).ThenInclude(e => e.appointment_serviceNavigation);

            return View(await PaginatedList<mp_appointment_refund>.CreateAsync(refunds.OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));
        }
    }
}