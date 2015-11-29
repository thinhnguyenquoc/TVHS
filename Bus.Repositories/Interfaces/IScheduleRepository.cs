using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface IScheduleRepository : IDisposable
    {
        IQueryable<Schedule> All { get; }
        IQueryable<Schedule> AllIncluding(params Expression<Func<Schedule, object>>[] includeProperties);
        Schedule Find(int id);
        void InsertOrUpdate(Schedule Schedule);
        void Delete(int id);
        void Save();
    }
}
