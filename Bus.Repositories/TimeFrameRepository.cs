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
    public class TimeFrameRepository : ITimeFrameRepository
    {
        TVHSContext context;
        public TimeFrameRepository(TVHSContext _context)
        {
            context = _context;
        }
        
        public IQueryable<TimeFrame> All
        {
            get { return context.TimeFrames; }
        }

        public IQueryable<TimeFrame> AllIncluding(params Expression<Func<TimeFrame, object>>[] includeProperties)
        {
            IQueryable<TimeFrame> query = context.TimeFrames;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TimeFrame Find(int id)
        {
            return context.TimeFrames.Find(id);
        }

        public void InsertOrUpdate(TimeFrame TimeFrame)
        {
            if (TimeFrame.Id == default(int))
            {
                // New entity
                context.TimeFrames.Add(TimeFrame);
            }
            else
            {
                var InDb = context.Programs.Find(TimeFrame.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.TimeFrames.Add(TimeFrame);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(TimeFrame);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var TimeFrame = context.Levels.Find(id);
            if (TimeFrame != null)
                context.Levels.Remove(TimeFrame);
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
