using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface ILevelRepository : IDisposable
    {
        IQueryable<Level> All { get; }
        IQueryable<Level> AllIncluding(params Expression<Func<Level, object>>[] includeProperties);
        Level Find(int id);
        void InsertOrUpdate(Level Level);
        void Delete(int id);
        void Save();
    }
}
