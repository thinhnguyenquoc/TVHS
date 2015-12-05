using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface ICategoryRepository : IDisposable
    {
        IQueryable<Category> All { get; }
        IQueryable<Category> AllIncluding(params Expression<Func<Category, object>>[] includeProperties);
        Category Find(int id);
        void InsertOrUpdate(Category Program);
        void Delete(int id);
        void Save();
    }
}
