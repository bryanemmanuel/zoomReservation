using zoomrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.DataAccess.Repository.IRepository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {

        IEnumerable<Schedule> GetSchedulesByDate(DateOnly selectedDate);
        void Update(Schedule obj);
    }
}
