using TVHS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories.Interfaces
{
    public interface ITimeSettingRepository : IDisposable
    {
        IQueryable<TimeSetting> All { get; }
        IQueryable<TimeSetting> AllIncluding(params Expression<Func<TimeSetting, object>>[] includeProperties);
        TimeSetting Find(int id);
        void InsertOrUpdate(TimeSetting TimeSetting);
        void Delete(int id);
        void Save();
    }
}
