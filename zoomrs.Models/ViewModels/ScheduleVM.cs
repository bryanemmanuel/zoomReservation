﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.Models.ViewModels
{
    public class ScheduleVM
    {

        [ValidateNever]
        public Schedule Schedule { get; set; }
    }
}
