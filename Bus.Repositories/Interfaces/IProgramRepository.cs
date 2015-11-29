using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface IProgramRepository : IDisposable
    {
        IQueryable<Program> All { get; }
        IQueryable<Program> AllIncluding(params Expression<Func<Program, object>>[] includeProperties);
        Program Find(int id);
        void InsertOrUpdate(Program Program);
        void Delete(int id);
        void Save();
    }
}
