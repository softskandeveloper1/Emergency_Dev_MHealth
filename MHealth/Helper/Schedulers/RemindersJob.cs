﻿using DAL.IService;
using DAL.Models;
using DAL.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Helper.Schedulers
{
    public class RemindersJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IEmailSender _emailSender;
        public RemindersJob(IServiceProvider provider, IEmailSender emailSender)
        {
            _provider = provider;
            _emailSender = emailSender;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Logs($"{DateTime.Now} [Reminders Service called]" + Environment.NewLine);
            Task task = Task.Run(() =>
            {
                using var scope = _provider.CreateScope();
                var _apoinmentService = scope.ServiceProvider.GetService<IAppointmentService>();
                var today = DateTime.Now;
                //5 min reminder
                ReminderBefore5MinForAppoinment(_apoinmentService, today);
                //1 hour reminder
                ReminderBefore1HourForAppoinment(_apoinmentService, today);
                //24 hour reminder
                ReminderBefore24HourForAppoinment(_apoinmentService, today);
            });

            return Task.CompletedTask;
        }

        public void ReminderBefore1HourForAppoinment(IAppointmentService _apoinmentService, DateTime today)
        {
            try
            {
                Logs($"{DateTime.Now} [ReminderBefore1HourForAppoinment Service called]" + Environment.NewLine);
                var appointments = _apoinmentService.Get().Include(e => e.clinician_).Include(e => e.client_).Where(e => e.start_date > today && e.start_date <= today.AddHours(1) && e.client_ != null && (e.is_reminder_mail_send == null || e.is_reminder_mail_send == false || e.client_.is_one_hour_reminder_mail_send == null || e.client_.is_one_hour_reminder_mail_send == false)).ToList();
                Logs($"{DateTime.Now} [ReminderBefore1HourForAppoinment Service called] count => {appointments?.Count()}" + Environment.NewLine);
                foreach (var appointment in appointments)
                {

                    var appointment_details = GetAppoinmentDetails(appointment);
                    _emailSender.SendEmailAsync(appointment.clinician_.email, "You have appoinment in next 1 hour - MySpace MyTime",
                      $"Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", You have appoinment in next 1 hour.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Member : </strong>" + appointment.client_.last_name + " " + appointment.client_.first_name);

                    _emailSender.SendEmailAsync(appointment.client_.email, "You have appoinment in next 1 hour - MySpace MyTime",
                     $"Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", You have appoinment in next 1 hour.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Provider : </strong>" + appointment.clinician_.last_name + " " + appointment.clinician_.first_name);

                    appointment.is_reminder_mail_send = true;
                    appointment.client_.is_one_hour_reminder_mail_send = true;
                    _apoinmentService.Update(appointment);

                }
            }
            catch (Exception ex)
            {
                var date = DateTime.Now;
                var error = $"*************************** Start {date} ***********************************" + Environment.NewLine;
                error += "Message :" + ex.Message + Environment.NewLine;
                error += "Inner Exception :" + ex.InnerException?.Message + Environment.NewLine;
                error += "Stack Trace :" + ex.StackTrace + Environment.NewLine;
                error += $"*************************** End {date} ***********************************" + Environment.NewLine;
                Logs(error);
            }
        }

        public string GetAppoinmentDetails(mp_appointment appointment)
        {

            var appointment_service = Options.GetAppointmentServices().FirstOrDefault(e => e.id == appointment.appointment_service);
            var appointment_details = "<strong>Date of appointment:</strong> " + appointment.start_date.ToString("dd MMM, yyyy hh:mm tt") + "<br/>";
            appointment_details += "<strong>Appointment type: </strong>" + Options.GetAppointmentTypeName(appointment.appointment_type) + "<br/>";
            appointment_details += "<strong>Appointment activity:</strong>" + Options.GetAppointmentSubActivityName(appointment.appointment_activity_sub_id) + "<br/>";
            appointment_details += "<strong>Appointment service:</strong>" + appointment_service.name + "<br/>";
            appointment_details += "<strong>Duration: </strong>" + appointment_service.time_minutes + " minutes <br/>";
            return appointment_details;
        }

        public void ReminderBefore24HourForAppoinment(IAppointmentService _apoinmentService, DateTime today)
        {
            try
            {
                Logs($"{DateTime.Now} [ReminderBefore24HourForAppoinment Service called]" + Environment.NewLine);
                var appointments = _apoinmentService.Get().Include(e => e.clinician_).Include(e => e.client_).Where(e => e.start_date > today && e.start_date <= today.AddHours(24) && (e.client_ == null || e.client_.is_24_hour_reminder_mail_send == false || e.client_.is_24_hour_reminder_mail_send == null)).ToList();
                Logs($"{DateTime.Now} [ReminderBefore24HourForAppoinment Service called] count => {appointments?.Count()}" + Environment.NewLine);
                foreach (var appointment in appointments)
                {

                    var appointment_details = GetAppoinmentDetails(appointment);

                    _emailSender.SendEmailAsync(appointment.client_.email, "You have appoinment in next 24 hour - MySpace MyTime",
                     $"Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", You have appoinment in next 24 hour.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Provider : </strong>" + appointment.clinician_.last_name + " " + appointment.clinician_.first_name);
                    appointment.client_.is_24_hour_reminder_mail_send = true;
                    _apoinmentService.Update(appointment);
                }
            }
            catch (Exception ex)
            {
                var date = DateTime.Now;
                var error = $"*************************** Start {date} ***********************************" + Environment.NewLine;
                error += "Message :" + ex.Message + Environment.NewLine;
                error += "Inner Exception :" + ex.InnerException?.Message + Environment.NewLine;
                error += "Stack Trace :" + ex.StackTrace + Environment.NewLine;
                error += $"*************************** End {date} ***********************************" + Environment.NewLine;
                Logs(error);
            }
        }

        public void ReminderBefore5MinForAppoinment(IAppointmentService _apoinmentService, DateTime today)
        {
            try
            {
                Logs($"{DateTime.Now} [ReminderBefore5MinForAppoinment Service called]" + Environment.NewLine);

                var appointments = _apoinmentService.Get().Include(e => e.clinician_).Include(e => e.client_).Where(e => e.start_date > today && e.start_date <= today.AddMinutes(5) && e.client_ != null).ToList();
                Logs($"{DateTime.Now} [ReminderBefore5MinForAppoinment Service called] count => {appointments?.Count()}" + Environment.NewLine);
                foreach (var appointment in appointments)
                {

                    var appointment_details = GetAppoinmentDetails(appointment);

                    _emailSender.SendEmailAsync(appointment.client_.email, "You have appoinment in next 5 minutes - MySpace MyTime",
                     $"Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", You have appoinment in next 5 minutes.<br/><strong>Appointment details </strong><br/>" + appointment_details + "<strong>Provider : </strong>" + appointment.clinician_.last_name + " " + appointment.clinician_.first_name);

                }
            }
            catch (Exception ex)
            {
                var date = DateTime.Now;
                var error = $"*************************** Start {date} ***********************************" + Environment.NewLine;
                error += "Message :" + ex.Message + Environment.NewLine;
                error += "Inner Exception :" + ex.InnerException?.Message + Environment.NewLine;
                error += "Stack Trace :" + ex.StackTrace + Environment.NewLine;
                error += $"*************************** End {date} ***********************************" + Environment.NewLine;
                Logs(error);
            }
        }

        public void Logs(string message)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Quartz");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, $"LogsReminder-{DateTime.Now:dd-MM-yyyy}.txt");
                File.AppendAllText(path, message);
            }
            catch (Exception ex)
            {
                _emailSender.SendEmailAsync("faisalmpathan.vision@gmail.com", "LogsReminder Scheduler error - MySpace MyTime", $"Error : {ex.Message} <br/>" +
                    $"InnerException : {ex.InnerException?.Message} <br/> StackTrace: {ex.StackTrace}");
            }
           
        }
    }
}
