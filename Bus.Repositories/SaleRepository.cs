using TVHS.Entities;
using TVHS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        TVHSContext context;
        public SaleRepository(TVHSContext _context)
        {
            context = _context;
        }
        
        public IQueryable<Sale> All
        {
            get { return context.Sales; }
        }

        public IQueryable<Sale> AllIncluding(params Expression<Func<Sale, object>>[] includeProperties)
        {
            IQueryable<Sale> query = context.Sales;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Sale Find(int id)
        {
            return context.Sales.Find(id);
        }

        public void InsertOrUpdate(Sale Sale)
        {
            if (Sale.Id == default(int))
            {
                // New entity
                context.Sales.Add(Sale);
            }
            else
            {
                var InDb = context.Programs.Find(Sale.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Sales.Add(Sale);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Sale);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Sale = context.Sales.Find(id);
            if (Sale != null)
                context.Sales.Remove(Sale);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
