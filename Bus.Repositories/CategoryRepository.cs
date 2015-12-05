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
    public class CategoryRepository : ICategoryRepository
    {
        TVHSContext context;
        public CategoryRepository(TVHSContext _context)
        {
            context = _context;
        }

        public IQueryable<Category> All
        {
            get { return context.Categorys; }
        }

        public IQueryable<Category> AllIncluding(params Expression<Func<Category, object>>[] includeProperties)
        {
            IQueryable<Category> query = context.Categorys;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Category Find(int id)
        {
            return context.Categorys.Find(id);
        }

        public void InsertOrUpdate(Category Category)
        {
            if (Category.Id == default(int))
            {
                // New entity
                context.Categorys.Add(Category);
            }
            else
            {
                var InDb = context.Programs.Find(Category.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Categorys.Add(Category);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Category);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Category = context.Categorys.Find(id);
            if (Category != null)
                context.Categorys.Remove(Category);
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
