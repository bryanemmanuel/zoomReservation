using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoomrs.DataAccess.Repository.IRepository;
using zoomrs.Models;

namespace zoomrs.DataAccess.Repository
{
    public class DisableDateRepository : Repository<DisableDate>, IDisableDateRepository
    {
        private ApplicationDbContext _db;

        public DisableDateRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(DisableDate obj)
        {
            var objFromDb = _db.DisableDate.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.date = obj.date;



            }
        }
    }
}
