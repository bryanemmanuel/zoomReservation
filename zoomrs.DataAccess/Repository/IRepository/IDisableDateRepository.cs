using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoomrs.Models;

namespace zoomrs.DataAccess.Repository.IRepository
{
    public interface IDisableDateRepository : IRepository<DisableDate>
    {
        void Update(DisableDate obj);
    }
}
