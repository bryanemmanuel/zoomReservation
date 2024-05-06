using zoomrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoomrs.DataAccess.Repository;
using zoomrs.DataAccess.Repository.IRepository;

namespace zoomrs.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Appointment = new AppointmentRepository(_db);
            Department = new DepartmentRepository(_db);
            Schedule = new ScheduleRepository(_db);
            DisableDate = new DisableDateRepository(_db);
            // every table
            //coffeeList = new coffeeListRepository(_db);
            //SizeList = new SizeListRepository(_db); // SizeList same name in IUnitOfWork
            //Company = new CompanyRepository(_db);
        }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IAppointmentRepository Appointment { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IScheduleRepository Schedule { get; private set; }
        public IDisableDateRepository DisableDate { get; private set; }
        // every table
        //public IcoffeeListRepository coffeeList { get; private set; }// every table
        //public ISizeListRepository SizeList { get; private set; }
        //public ICompanyRepository Company { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
