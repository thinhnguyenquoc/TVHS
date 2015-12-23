using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TVHS.Services.Interfaces
{
    public interface ICycleService
    {
        List<ViewModelCycle> GetAllCycle();
        void AddCycle(ViewModelCycle vCycle);
        ViewModelCycle GetDetail(int Id);
        void InsertOrUpdate(ViewModelCycle vCycle);
        ViewModelCycle Delete(ViewModelCycle vCycle);
        void AddCycleFromFile(Stream inputStream, string fileName);
    }
}
