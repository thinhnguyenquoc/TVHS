using AutoMapper;
using TVHS.Entities;
using TVHS.Repositories;
using TVHS.Repositories.Interfaces;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Configuration;

namespace TVHS.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _iProductRepository;
        IProgramRepository _iProgramRepository;
        ICategoryRepository _iCategoryRepository;
        IHelper _iHelper;
        public ProductService(IProductRepository iProductRepository, IHelper iHelper, IProgramRepository iProgramRepository,
            ICategoryRepository iCategoryRepository)
        {
            _iProductRepository = iProductRepository;
            _iHelper = iHelper;
            _iProgramRepository = iProgramRepository;
            _iCategoryRepository = iCategoryRepository;
        }
        
        public List<ViewModelProduct> GetAllProduct()
        {
            List<ViewModelProduct> productList = Mapper.Map<List<Product>, List<ViewModelProduct>>(_iProductRepository.All.ToList());
            foreach(var i in productList){
                var cate = _iCategoryRepository.Find(i.Id);
                if(cate != null)
                    i.ParentName = cate.Name;
            }
            return productList;
        }

        public void AddProduct(ViewModelProduct vproduct)
        {
            _iProductRepository.InsertOrUpdate(Mapper.Map<ViewModelProduct, Product>(vproduct));
            _iProductRepository.Save();        
        }

        public ViewModelProduct GetDetail(int Id)
        {
            var result = _iProductRepository.Find(Id);
            if (result != null)
            {
                return Mapper.Map<Product, ViewModelProduct>(result);
            }
            return null;
        }

        public void InsertOrUpdate(ViewModelProduct vproduct)
        {
            try
            {
                _iProductRepository.InsertOrUpdate(Mapper.Map<ViewModelProduct, Product>(vproduct));
                _iProductRepository.Save();
            }
            catch (Exception e)
            {

            }
        }

        public ViewModelProduct Delete(ViewModelProduct vproduct)
        {
            try
            {
                _iProductRepository.Delete(vproduct.Id);
                _iProductRepository.Save();
                return vproduct;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void AddProgramFromFile(Stream inputStream, string fileName)
        {
            IWorkbook _iWorkbook = null;
            if (fileName.Contains(".xlsx"))
            {
                _iWorkbook = new XSSFWorkbook(inputStream);
            }
            else if (fileName.Contains(".xls"))
            {
                _iWorkbook = new HSSFWorkbook(inputStream);
            }

            if (_iWorkbook != null)
            {
                string productCodeTabName = ConfigurationManager.AppSettings["ProductCodeTabName"];
                // get anchor STT
                string headerRowKey = ConfigurationManager.AppSettings["HeaderRowKey"];
                for (int i = 0; i < _iWorkbook.NumberOfSheets; i++)
                {
                    if (_iWorkbook.GetSheetName(i).ToLower().Contains(productCodeTabName.ToLower()))
                    {
                        ISheet sheet = _iWorkbook.GetSheetAt(i);
                        List<int> target = _iHelper.StartPoint(sheet, headerRowKey);
                        if (target != null)
                        {
                            for (int j = target.FirstOrDefault() + 1; j <= sheet.LastRowNum; j++)
                            {
                                var row = sheet.GetRow(j);
                                if (row.GetCell(target.LastOrDefault()) != null)
                                {
                                    string productname = row.GetCell(target.LastOrDefault() + 1).StringCellValue.ToString();
                                    var product = _iProductRepository.All.Where(x => x.Name.Contains(productname)).FirstOrDefault();
                                    List<string> mabanglist = new List<string>();
                                    int k = target.LastOrDefault();
                                    while (k <= row.LastCellNum)
                                    {
                                        if (row.GetCell(k + 2) != null) { 
                                            var mabang = row.GetCell(k + 2).StringCellValue;
                                            mabanglist.Add(mabang);
                                            k += 2;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    foreach (var item in mabanglist)
                                    {
                                        var program = _iProgramRepository.All.Where(x => x.ProgramCode == item).FirstOrDefault();
                                        if (program != null)
                                        {
                                            program.ProductId = product.Id;
                                            _iProgramRepository.InsertOrUpdate(program);
                                        }
                                    }
                                }
                                _iProgramRepository.Save();
                            }
                        }
                    }
                }
            }
        }
    }
}
