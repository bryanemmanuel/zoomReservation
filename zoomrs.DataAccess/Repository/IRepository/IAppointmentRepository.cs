﻿using zoomrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.DataAccess.Repository.IRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        void Update(Appointment obj);
    }
}
