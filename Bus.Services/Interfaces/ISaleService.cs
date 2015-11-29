using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TVHS.Services.Interfaces
{
    public interface ISaleService
    {
        List<ViewModelSale> GetAllSale();
        void AddSale(ViewModelSale vSale);
        ViewModelSale GetDetail(int Id);
        void InsertOrUpdate(ViewModelSale vSale);
        ViewModelSale Delete(ViewModelSale vSale);
        void AddSaleFromFile(Stream inputStream, string fileName);
    }
}
