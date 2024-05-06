using Microsoft.AspNetCore.Mvc;
using zoomrs.DataAccess;
using zoomrs.DataAccess.Repository.IRepository;
using zoomrs.Models;
using zoomrs.Models.ViewModels;
using zoomrs.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nest;
using zoomrs.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;
using System.Globalization;
using System.Net;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Text;
using System.Diagnostics.Metrics;
using Microsoft.CodeAnalysis.Options;
using System.Drawing;

namespace zoomrs.Areas.users.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public AppointmentController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
		public IActionResult Cancel(int id)
		{

            var appointment = _unitOfWork.Appointment
                    .GetAll()
                    .Join(_unitOfWork.Department.GetAll(),
                          appointment => appointment.DepartmentId,
                          department => department.Id,
                          (appointment, department) => new { Appointment = appointment, Department = department })
                    .FirstOrDefault(u => u.Appointment.Id == id);

            // Retrieve schedule data associated with the appointment
            var scheduleData = _unitOfWork.Schedule
                .GetAll()
                .Where(schedule => schedule.AppointmentId == id)
                .ToList();

            // Create an instance of AppointmentVM and populate its properties
            AppointmentVM appointmentVM = new AppointmentVM
            {
                Appointment = appointment?.Appointment, // Use ?. to handle null if no appointment found
                ScheduleData = scheduleData
            };


            // Pass the populated AppointmentVM object to the view
            return View(appointmentVM);
        }
        public IActionResult Index()
        {
            //var disabledDates = _unitOfWork.DisableDate.GetAll();

            var appointmentVM = new AppointmentVM()
            {
                Appointment = new(),
                DepartmentList = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.DepartmentName,
                    Value = i.Id.ToString()
                }),
            };

            var disabledDates = _unitOfWork.DisableDate.GetAll().Select(d => d.date.ToString("yyyy-MM-dd")).ToList();
            ViewData["DisableDate"] = disabledDates;



            return View(appointmentVM);
        }
        public IActionResult test()
        {
            AppointmentVM appointmentVM = new()
            {
                Appointment = new(),
                DepartmentList = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.DepartmentName,
                    Value = i.Id.ToString()
                }),

            };
            var disabledDates = _unitOfWork.DisableDate.GetAll().Select(d => d.date.ToString("yyyy-MM-dd")).ToList();
            ViewData["DisableDate"] = disabledDates;
            return View(appointmentVM);
         
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancelling(AppointmentVM obj, int appId, int schedID)
        {
            var theId = appId;
			var theReason = obj.Appointment.Cancelled;

			var getSched1 = _unitOfWork.Schedule.GetAll(u => u.Id == schedID).FirstOrDefault();
			if (getSched1 == null)
			{
				// Handle the scenario where no corresponding Schedule record is found
				return RedirectToAction("Index");
			}

			// Retrieve the AppointmentId from the Schedule record
			var appointmentId = getSched1.AppointmentId;
			var getApp1 = _unitOfWork.Appointment.GetAll(u => u.Id == appointmentId).FirstOrDefault();
			if (getApp1 == null)
			{
				return RedirectToAction("Index");
			}

			// Update the Schedule and Appointment records
			getSched1.availability = "Cancelled";
			var email = getApp1.Email;
			getApp1.Cancelled = theReason;

			// Update the records in the database
			_unitOfWork.Schedule.Update(getSched1);
			_unitOfWork.Appointment.Update(getApp1);

			// Save changes to the database

			_unitOfWork.Save();




			return RedirectToAction("Index", "Home");
		}

	    [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AppointmentVM obj, string selectedTimeSlots, string selectedDate)
        {
            //var timeSlots = selectedTimeSlots;
         
            //Schedule
            int[] forSchedule = null;
            int count2 = 0;
            int count3 = 0;
            string[] selectedTimeSlotsArray = selectedTimeSlots.Split(',');

            if (selectedTimeSlotsArray != null)
            {
                forSchedule = new int[selectedTimeSlotsArray.Length];

                for (int index = 0; index < selectedTimeSlotsArray.Length; index++)
                {
                    if (int.TryParse(selectedTimeSlotsArray[index], out int timeSlot))
                    {
                        forSchedule[index] = timeSlot;
                        count2 = count2 + forSchedule[index];
                        count3++;
                    }

                }
            }

            //Appointment
            int[] forAppointment = null;
            if (forSchedule != null && forSchedule.Length > 0)
            {
                List<int> appointmentTimeSlots = new List<int>();
                foreach (int temp in forSchedule)
                {
                    var appointments = _unitOfWork.Schedule.GetAll().Where(schedule => schedule.Id == temp);
                    appointmentTimeSlots.AddRange(appointments.Select(schedule => schedule.time));
                }
                forAppointment = appointmentTimeSlots.ToArray();
            }

            int count = 0;//for code holder
            for (int a = 0; a < forAppointment.Length; a++)
            {
                count = count + forAppointment[a];

            }



            //var department = _unitOfWork.Department.GetAll().Where(Department => Department.Id == obj.Appointment.DepartmentId);

            //string departmenName = department.DepartmentName;
            var department = _unitOfWork.Department.GetAll()
                    .Where(d => d.Id == obj.Appointment.DepartmentId)
                    .FirstOrDefault();

            string departmentName = null; // Initialize departmentName variable

            if (department != null)
            {
                // Retrieve the department name from the department record
                departmentName = department.Abbreviation;
            }

            var deptcheck = departmentName;
            var countcheck = count2;
            var checker = forAppointment;
            var checker2 = forSchedule;


            string code = departmentName + "_" + selectedDate + "_" + count2.ToString();

            // Create and add the appointment to the database
            var appointment = new Appointment
            {
                // Populate appointment properties from AppointmentVM obj
                FirstName = obj.Appointment.FirstName,
                LastName = obj.Appointment.LastName,
                Sex = obj.Appointment.Sex,
                Email = obj.Appointment.Email,
                reason = obj.Appointment.reason,
                PhoneNum = obj.Appointment.PhoneNum,
                DepartmentId = obj.Appointment.DepartmentId,
                transactionId = code, // Generate transaction ID

            };

            // Add the appointment to the unit of work
            _unitOfWork.Appointment.Add(appointment);

            // Save changes to the database to generate the appointment ID
            _unitOfWork.Save();

            // Access the appointment ID generated after adding the appointment
            var appointmentId = appointment.Id;

            // Update the schedule records
            //foreach (int scheduleId in forSchedule)
            //{

            var schedule = new Schedule
            {
                // Populate appointment properties from AppointmentVM obj
                //string selectedTimeSlots, string selectedDate
                date = DateOnly.Parse(selectedDate),
                time = count2,
                AppointmentId = appointmentId,
                availability = "booked",
                count = count3

            };

            // Add the appointment to the unit of work
            _unitOfWork.Schedule.Add(schedule);



            // Save changes to the database
            _unitOfWork.Save();


            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("City Government of Calamba", "no-reply@calambacity.gov.ph")); // Set your name and email address as the sender
            message.To.Add(new MailboxAddress("", obj.Appointment.Email)); // Set the recipient's email address
            message.Subject = "Appointment Confirmation"; // Set the email subject

            BodyBuilder bodyBuilder = new BodyBuilder();

            StringBuilder sb = new StringBuilder();
			sb.Append("<h4>Transaction Id: " + code + "</h4> ");
			sb.Append("<div style = 'color:red; '> This is the appointment confirmation email body.</div> ");
			sb.Append("<h6>Name: "+ obj.Appointment.FirstName+ " " + obj.Appointment.LastName + "</h6> ");
			sb.Append("<h6>Date: " + schedule.date +"</h6> ");
			sb.Append("<h6>Time: </h6> ");
			//--------- item.time
			int val = schedule.time;
			int countt = schedule.count;
            int countt2 = 0;
		   List<string> getTimes = new List<string>(); // Using List<string> instead of string[]

					for (int i = 256; i >= 1; i = i / 2)
					{
						if (i<val && countt2<countt)
						{
							switch (i)
							{
								case 1:
									getTimes.Add("8:00AM-9:00AM");
									break;
								case 2:
									getTimes.Add("9:00AM-10:00AM");
									break;
								case 4:
									getTimes.Add("10:00AM-11:00AM");
									break;
								case 8:
									getTimes.Add("11:00AM-12:00PM");
									break;
								case 16:
									getTimes.Add("12:00PM-1:00PM");
									break;
								case 32:
									getTimes.Add("1:00PM-2:00PM");
									break;
								case 64:
									getTimes.Add("2:00PM-3:00PM");
									break;
								case 128:
									getTimes.Add("3:00PM-4:00PM");
									break;
								case 256:
									getTimes.Add("4:00PM-5:00PM");
									break;
								default:
									break;
							}
                            countt2++;
						}

				
			        }
			//-------
			for (int a = getTimes.Count - 1; a >= 0; a--)
			{
				sb.Append("<h6>" + getTimes[a] + " </h6> ");
			}

			sb.Append("<span style='opacity: 0;'>" + DateTime.Now + "</span>");
            bodyBuilder.HtmlBody = sb.ToString();

            message.Body = bodyBuilder.ToMessageBody();


            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("mail.calambacity.gov.ph", 587, SecureSocketOptions.None);
                client.Authenticate("no-reply@calambacity.gov.ph", "NOREPpassword101$");

                // Send the email
                client.Send(message);
                client.Disconnect(true);
            }

			return RedirectToAction("Index", "Home");
		}

        public ActionResult GetAvailableTimeSlots(DateOnly selectedDate)
        {
            List<SelectListItem> timeSlots = new List<SelectListItem>
            {
                new SelectListItem { Text = "08:00 AM - 09:00 AM", Value = "1", Selected = false },
                new SelectListItem { Text = "09:00 AM - 10:00 AM", Value = "2", Selected = false },
                new SelectListItem { Text = "10:00 AM - 11:00 AM", Value = "4", Selected = false },
                new SelectListItem { Text = "11:00 AM - 12:00 PM", Value = "8", Selected = false },
                new SelectListItem { Text = "12:00 PM - 01:00 PM", Value = "16", Selected = false },
                new SelectListItem { Text = "01:00 PM - 02:00 PM", Value = "32", Selected = false },
                new SelectListItem { Text = "02:00 PM - 03:00 PM", Value = "64", Selected = false },
                new SelectListItem { Text = "03:00 PM - 04:00 PM", Value = "128", Selected = false },
                new SelectListItem { Text = "04:00 PM - 05:00 PM", Value = "256", Selected = false }
            };

            var availableTimeSlots = _unitOfWork.Schedule
                .GetAll()
                .Where(schedule => schedule.date == selectedDate)
                .ToList();

            List<int> getTimes = new List<int>();

            foreach (var schedule in availableTimeSlots)
            {
                int time = schedule.time;
                int count = schedule.count;

                int count2 = 0;
                for (int i = 256; i >= 1; i = i / 2)
                {
                    if ((i & time) == i && count2 < count)
                    {
                        getTimes.Add(i);
                        count2++;
                    }
                }
            }

            // Disable options in timeSlots based on getTimes
            foreach (var timeSlot in timeSlots)
            {
                if (getTimes.Contains(int.Parse(timeSlot.Value)))
                {
                    timeSlot.Disabled = true;
                }
            }

            return Json(timeSlots);
        }






        private string ConvertTimeSlot(int timeSlot)
        {
            switch (timeSlot)
            {
                case 1:
                    return "08:00am - 09:00am";
                case 2:
                    return "09:00am - 10:00am";
                case 4:
                    return "10:00am - 11:00am";
                case 8:
                    return "11:00am - 12:00pm";
                case 16:
                    return "12:00pm - 01:00pm";
                case 32:
                    return "01:00pm - 02:00pm";
                case 64:
                    return "02:00pm - 03:00pm";
                case 128:
                    return "03:00pm - 04:00pm";
                case 256:
                    return "04:00pm - 05:00pm";
                default:
                    return ""; 
            }
        }





        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var deparmentlist = _unitOfWork.Appointment.GetAll(includeProperties: "Department");
            return Json(new { data = deparmentlist });
        }

       
        #endregion
    }
}
