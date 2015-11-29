using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface ISaleRepository : IDisposable
    {
        IQueryable<Sale> All { get; }
        IQueryable<Sale> AllIncluding(params Expression<Func<Sale, object>>[] includeProperties);
        Sale Find(int id);
        void InsertOrUpdate(Sale Sale);
        void Delete(int id);
        void Save();
    }
}
