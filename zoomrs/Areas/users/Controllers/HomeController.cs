using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Diagnostics;
using zoomrs.DataAccess.Repository.IRepository;
using zoomrs.Models;

namespace zoomrs.Controllers
{
    public class HomeController : Controller
    {

		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;


		public HomeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
		}

		public IActionResult Index()
        {
            return View();
        }
		
		public IActionResult Test()
		{
			return View();
		}
		public ActionResult getDetails(string revId)
		{
			var appointments = _unitOfWork.Appointment
				.GetAll()
				.Where(a => a.transactionId == revId)
				.Join(
					_unitOfWork.Schedule.GetAll(),
					appointment => appointment.Id,
					schedule => schedule.AppointmentId,
					(appointment, schedule) => new
					{
						ScheduleId = schedule.Id,
						schedule.date,
						schedule.time,
                        schedule.count,
                        schedule.availability,
						appointment.Id,
						appointment.FirstName,
						appointment.LastName,
						appointment.Sex,
						appointment.reason,
						appointment.Email,
						appointment.PhoneNum,
						appointment.transactionId,
						appointment.DepartmentId,
						appointment.Cancelled
					}
				)
				.Join(
					_unitOfWork.Department.GetAll(),
					appointmentSchedule => appointmentSchedule.DepartmentId,
					department => department.Id,
					(appointmentSchedule, department) => new
					{
						appointmentSchedule.ScheduleId,
						appointmentSchedule.date,
						appointmentSchedule.time,
                        appointmentSchedule.count,
                        appointmentSchedule.availability,
						appointmentSchedule.Id,
						appointmentSchedule.FirstName,
						appointmentSchedule.LastName,
						appointmentSchedule.Sex,
						appointmentSchedule.reason,
						appointmentSchedule.Email,
						appointmentSchedule.PhoneNum,
						appointmentSchedule.transactionId,
						appointmentSchedule.DepartmentId,
						appointmentSchedule.Cancelled,
						DepartmentName = department.DepartmentName,
						DepartmentAbbreviation = department.Abbreviation
					}
				).FirstOrDefault();

			return Json(appointments);
		}





		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}