using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TVHS.Services.Interfaces
{
    public interface IScheduleService
    {
        List<ViewModelSchedule> GetAllSchedule();
        void AddSchedule(ViewModelSchedule vSchedule);
        ViewModelSchedule GetDetail(int Id);
        void InsertOrUpdate(ViewModelSchedule vSchedule);
        ViewModelSchedule Delete(ViewModelSchedule vSchedule);
        void AddScheduleFromFile(Stream inputStream, string fileName);
    }
}
