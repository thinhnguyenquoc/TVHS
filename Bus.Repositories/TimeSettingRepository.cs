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
    public class TimeSettingRepository : ITimeSettingRepository
    {
        TVHSContext context;
        public TimeSettingRepository(TVHSContext _context)
        {
            context = _context;
        }
        
        public IQueryable<TimeSetting> All
        {
            get { return context.TimeSettings; }
        }

        public IQueryable<TimeSetting> AllIncluding(params Expression<Func<TimeSetting, object>>[] includeProperties)
        {
            IQueryable<TimeSetting> query = context.TimeSettings;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TimeSetting Find(int id)
        {
            return context.TimeSettings.Find(id);
        }

        public void InsertOrUpdate(TimeSetting TimeSetting)
        {
            if (TimeSetting.Id == default(int))
            {
                // New entity
                context.TimeSettings.Add(TimeSetting);
            }
            else
            {
                var InDb = context.Programs.Find(TimeSetting.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.TimeSettings.Add(TimeSetting);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(TimeSetting);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var TimeSetting = context.Levels.Find(id);
            if (TimeSetting != null)
                context.Levels.Remove(TimeSetting);
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
