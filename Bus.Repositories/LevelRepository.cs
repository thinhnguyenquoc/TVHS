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
    public class LevelRepository : ILevelRepository
    {
        TVHSContext context;
        public LevelRepository(TVHSContext _context)
        {
            context = _context;
        }
        
        public IQueryable<Level> All
        {
            get { return context.Levels; }
        }

        public IQueryable<Level> AllIncluding(params Expression<Func<Level, object>>[] includeProperties)
        {
            IQueryable<Level> query = context.Levels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Level Find(int id)
        {
            return context.Levels.Find(id);
        }

        public void InsertOrUpdate(Level Level)
        {
            if (Level.Id == default(int))
            {
                // New entity
                context.Levels.Add(Level);
            }
            else
            {
                var InDb = context.Programs.Find(Level.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Levels.Add(Level);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Level);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Level = context.Levels.Find(id);
            if (Level != null)
                context.Levels.Remove(Level);
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
