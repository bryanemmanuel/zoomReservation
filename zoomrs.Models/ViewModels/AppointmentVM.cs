using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.Models.ViewModels
{
    public class AppointmentVM
    {

        [ValidateNever]
        public Appointment Appointment { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ScheduleList { get; set; }
        public IEnumerable<DisableDate> DisableDate { get; set; }
        public List<Schedule> ScheduleData { get; set; }
    }
}
