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

namespace TVHS.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _iProductRepository;
        public ProductService(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }
        
        public List<ViewModelProduct> GetAllProduct()
        {
            return Mapper.Map<List<Product>, List<ViewModelProduct>>(_iProductRepository.All.ToList());
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
    }
}
