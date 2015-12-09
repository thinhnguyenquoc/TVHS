using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TVHS.Services.Interfaces
{
    public interface IProgramService
    {
        List<ViewModelProgram> GetAllProgram();
        void AddProgram(ViewModelProgram vprogram);
        ViewModelProgram GetDetail(int Id);
        void InsertOrUpdate(ViewModelProgram vprogram);
        ViewModelProgram Delete(ViewModelProgram vprogram);
        void AddProgramFromFile(Stream inputStream, string fileName);
        ViewModelProgram GetProgramByCode(string code);
        List<ViewModelProgram> GetAllProgramsHaveProduct();
        List<ViewModelProgram> GetAllProgramsHaveQuantity(DateTime limitDate);
        List<ViewModelProgram> GetProgramQuantity(List<ViewModelProgram> programList, DateTime date);
    }
}
