using zoomrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using zoomrs.DataAccess;
using zoomrs.DataAccess.Repository.IRepository;

namespace zoomrs.DataAccess.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Appointment obj)
        {
            var objFromDb = _db.Appointment.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.FirstName = obj.FirstName;
                objFromDb.LastName = obj.LastName;
                objFromDb.Sex = obj.Sex;
                objFromDb.reason = obj.reason;
                objFromDb.Email = obj.Email;
                objFromDb.PhoneNum = obj.PhoneNum;
                objFromDb.transactionId = obj.transactionId;
                objFromDb.DepartmentId = obj.DepartmentId;
                objFromDb.Cancelled = obj.Cancelled;
            }
        }
    }
}
