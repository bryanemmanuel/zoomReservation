using zoomrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoomrs.DataAccess.Repository.IRepository;

namespace zoomrs.DataAccess.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Department obj)
        {
            var objFromDb = _db.Department.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.DepartmentName = obj.DepartmentName;
                objFromDb.Abbreviation = obj.Abbreviation;


            }
        }
    }
}
