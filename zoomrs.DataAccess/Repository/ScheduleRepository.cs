using zoomrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoomrs.DataAccess.Repository.IRepository;
using zoomrs.DataAccess;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace zoomrs.DataAccess.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private ApplicationDbContext _db;

        public ScheduleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       
        public void Update(Schedule obj)
        {
            var objFromDb = _db.Schedule.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.date = obj.date;
                objFromDb.time = obj.time;
                objFromDb.AppointmentId = obj.AppointmentId;
                objFromDb.availability = obj.availability;
                objFromDb.count = obj.count;
            }
        }

        public IEnumerable<Schedule> GetSchedulesByDate(DateOnly date)
        {
            return _db.Set<Schedule>().Where(s => s.date == date).ToList();
        }
    }
}
