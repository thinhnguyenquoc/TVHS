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
    public class ProductRepository : IProductRepository
    {
        TVHSContext context;
        public ProductRepository(TVHSContext _context)
        {
            context = _context;
        }

        public IQueryable<Product> All
        {
            get { return context.Products; }
        }

        public IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = context.Products;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Product Find(int id)
        {
            return context.Products.Find(id);
        }

        public void InsertOrUpdate(Product Product)
        {
            if (Product.Id == default(int))
            {
                // New entity
                context.Products.Add(Product);
            }
            else
            {
                var InDb = context.Programs.Find(Product.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Products.Add(Product);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Product);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Product = context.Products.Find(id);
            if (Product != null)
                context.Products.Remove(Product);
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
