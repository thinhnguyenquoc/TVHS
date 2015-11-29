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
    public class ProgramRepository : IProgramRepository
    {
        TVHSContext context;
        public ProgramRepository(TVHSContext _context)
        {
            context = _context;
        }

        public IQueryable<Program> All
        {
            get { return context.Programs; }
        }

        public IQueryable<Program> AllIncluding(params Expression<Func<Program, object>>[] includeProperties)
        {
            IQueryable<Program> query = context.Programs;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Program Find(int id)
        {
            return context.Programs.Find(id);
        }

        public void InsertOrUpdate(Program Program)
        {
            if (Program.Id == default(int))
            {
                // New entity
                context.Programs.Add(Program);
            }
            else
            {
                var InDb = context.Programs.Find(Program.Id);

                // Activity does not exist in database and it's new one
                if (InDb == null)
                {
                    context.Programs.Add(Program);
                    return;
                }

                // Activity already exist in database and modify it
                context.Entry(InDb).CurrentValues.SetValues(Program);
                context.Entry(InDb).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var Program = context.Programs.Find(id);
            if (Program != null)
                context.Programs.Remove(Program);
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
