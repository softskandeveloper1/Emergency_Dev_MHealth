using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Helper
{
    public class NotificationHelper
    {
        private readonly IEmailSender _emailSender;
        public NotificationHelper(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task AppointmentScheduled(mp_appointment appointment, IList<ApplicationUser> admins)
        {
            var appointment_service = Options.GetAppointmentServices().FirstOrDefault(e => e.id == appointment.appointment_service);

            var appointment_details = "<strong>Date of appointment:</strong> " + appointment.start_date.ToString("dd MMM, yyyy hh:mm tt") + "<br/>";
            appointment_details += "<strong>Appointment type: </strong>" + Options.GetAppointmentTypeName(appointment.appointment_type) + "<br/>";
            appointment_details += "<strong>Appointment activity:</strong>" + Options.GetAppointmentSubActivityName(appointment.appointment_activity_sub_id) + "<br/>";
            appointment_details += "<strong>Appointment service:</strong>" + appointment_service.name + "<br/>";
            appointment_details += "<strong>Duration: </strong>" + appointment_service.time_minutes + " minutes <br/>";


            var notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 3,
                read = 0,
                user_id = appointment.client_.user_id
            };
            notification.title = "Appointment scheduled successfully";
            notification.notification = "Appointment scheduled successfully.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Provider : </strong>" + appointment.clinician_.last_name + " " + appointment.clinician_.first_name;
            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(appointment.client_.email, "Appointment successful - MySpace MyTime",
                  $"Thanks you " + appointment.client_.last_name + " " + appointment.client_.first_name + ". Your appointment has been scheduled successfully.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Provider : </strong>" + appointment.clinician_.last_name + " " + appointment.clinician_.first_name);


            notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 4,
                read = 0,
                user_id = appointment.clinician_.user_id
            };

            //notification.user_id = appointment.clinician_.user_id;
            //notification.notification_type = 4;

            notification.title = "Appointment scheduled successfully";
            notification.notification = "Appointment scheduled successfully.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Member : </strong>" + appointment.client_.last_name + " " + appointment.client_.first_name;
            NotificationUtil.Add(notification);

            //NotificationUtil.AddNotification(notification);

            await _emailSender.SendEmailAsync(appointment.clinician_.email, "New appointment scheduled - MySpace MyTime",
                $"Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", a new appointment has been scheduled for you.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Member : </strong>" + appointment.client_.last_name + " " + appointment.client_.first_name);

            foreach (var admin in admins)
            {
                notification = new mp_notification
                {
                    created_by = "sys_admin",
                    created_by_name = "System Admin",
                    notification_type = 4,
                    read = 0,
                    user_id = admin.Id
                };

                notification.title = "Recieved Payment For An Appointment";
                notification.notification = $"Recieved Payment For An Appointment, Clinician is " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + " and Member is " + appointment.client_.last_name + " " + appointment.client_.first_name;
                NotificationUtil.Add(notification);
            }
        }
    }
}
