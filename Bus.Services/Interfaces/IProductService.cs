using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Services.Interfaces
{
    public interface IProductService
    {
        List<ViewModelProduct> GetAllProduct();
        void AddProduct(ViewModelProduct vproduct);
        ViewModelProduct GetDetail(int Id);
        void InsertOrUpdate(ViewModelProduct vproduct);
        ViewModelProduct Delete(ViewModelProduct vproduct);
    }
}
