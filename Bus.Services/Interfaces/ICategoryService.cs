using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TVHS.Services.Interfaces
{
    public interface ICategoryService
    {
        List<ViewModelCategory> GetAllCategory();
        void AddCategory(ViewModelCategory vCategory);
        ViewModelCategory GetDetail(int Id);
        void InsertOrUpdate(ViewModelCategory vCategory);
        ViewModelCategory Delete(ViewModelCategory vCategory);
    }
}
