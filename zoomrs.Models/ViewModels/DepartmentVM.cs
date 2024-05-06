using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.Models.ViewModels
{
    public class DepartmentVM
    {

        [ValidateNever]
        public Department Department { get; set; }
    }
}
