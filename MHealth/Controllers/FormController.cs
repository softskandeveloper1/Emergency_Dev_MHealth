using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Utils;
using MHealth.Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;

namespace MHealth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public FormController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IActionResult GetAppointmentForms(Guid appointment_id)
        {
            var appointment = _appointmentService.Get(appointment_id);


            var timelines = new List<AppointmentForm>();

            if (appointment != null)
            {
                var sql = string.Format("SELECT * from public.get_appointment_forms('{0}')", appointment.id);

                var cmd = new NpgsqlCommand(sql);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pro_id", NpgsqlDbType.Uuid, appointment_id);
                var dt = DataAccess.GetDataTable(cmd);
                timelines = DataUtil.DataTableToList<AppointmentForm>(dt);
            }
           

           

            return Ok(timelines);
        }
    }
}