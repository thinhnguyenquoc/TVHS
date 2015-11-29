using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        IQueryable<Product> All { get; }
        IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties);
        Product Find(int id);
        void InsertOrUpdate(Product Program);
        void Delete(int id);
        void Save();
    }
}
