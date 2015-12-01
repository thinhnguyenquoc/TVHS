using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface ITimeFrameRepository : IDisposable
    {
        IQueryable<TimeFrame> All { get; }
        IQueryable<TimeFrame> AllIncluding(params Expression<Func<TimeFrame, object>>[] includeProperties);
        TimeFrame Find(int id);
        void InsertOrUpdate(TimeFrame TimeFrame);
        void Delete(int id);
        void Save();
    }
}
