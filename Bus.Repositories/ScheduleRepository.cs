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
    public class ScheduleRepository : IScheduleRepository
    {
        TVHSContext context;
        public ScheduleRepository(TVHSContext _context)
        {
            context = _context;
        }
        
        public IQueryable<Schedule> All
        {
            get { return context.Schedules; }
        }

        public IQueryable<Schedule> AllIncluding(params Expression<Func<Schedule, object>>[] includeProperties)
        {
            IQueryable<Schedule> query = context.Schedules;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Schedule Find(int id)
        {
            return context.Schedules.Find(id);
        }

        public void InsertOrUpdate(Schedule Schedule)
        {
            if (Schedule.Id == default(int))
            {
                // New entity
                context.Schedules.Add(Schedule);
            }
            else
            {
                var InDb = context.Programs.Find(Schedule.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Schedules.Add(Schedule);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Schedule);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Schedule = context.Levels.Find(id);
            if (Schedule != null)
                context.Levels.Remove(Schedule);
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
