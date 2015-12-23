using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface ICycleRepository : IDisposable
    {
        IQueryable<Cycle> All { get; }
        IQueryable<Cycle> AllIncluding(params Expression<Func<Cycle, object>>[] includeProperties);
        Cycle Find(int id);
        void InsertOrUpdate(Cycle Cycle);
        void Delete(int id);
        void Save();
    }
}
