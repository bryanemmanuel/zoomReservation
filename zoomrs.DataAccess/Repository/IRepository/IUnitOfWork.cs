using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IAppointmentRepository Appointment { get; }
        IDepartmentRepository Department { get; }
        IScheduleRepository Schedule { get; }

        IDisableDateRepository DisableDate { get; }
        //IcoffeeListRepository coffeeList { get; }
        //ISizeListRepository SizeList { get; }
        //ICompanyRepository Company { get; }
        void Save();
    }
}
