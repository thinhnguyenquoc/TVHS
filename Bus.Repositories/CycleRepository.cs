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
    public class CycleRepository : ICycleRepository
    {
        TVHSContext context;
        public CycleRepository(TVHSContext _context)
        {
            context = _context;
        }
        
        public IQueryable<Cycle> All
        {
            get { return context.Cycles; }
        }

        public IQueryable<Cycle> AllIncluding(params Expression<Func<Cycle, object>>[] includeProperties)
        {
            IQueryable<Cycle> query = context.Cycles;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Cycle Find(int id)
        {
            return context.Cycles.Find(id);
        }

        public void InsertOrUpdate(Cycle Cycle)
        {
            if (Cycle.Id == default(int))
            {
                // New entity
                context.Cycles.Add(Cycle);
            }
            else
            {
                var InDb = context.Programs.Find(Cycle.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Cycles.Add(Cycle);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Cycle);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Cycle = context.Levels.Find(id);
            if (Cycle != null)
                context.Levels.Remove(Cycle);
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
