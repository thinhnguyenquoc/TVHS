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
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _iCategoryRepository;
        IHelper _iHelper;
        public CategoryService(ICategoryRepository iCategoryRepository, IHelper iHelper)
        {
            _iCategoryRepository = iCategoryRepository;
            _iHelper = iHelper;
        }
        
        public List<ViewModelCategory> GetAllCategory()
        {
            return Mapper.Map<List<Category>, List<ViewModelCategory>>(_iCategoryRepository.All.ToList());
        }

        public void AddCategory(ViewModelCategory vCategory)
        {
            _iCategoryRepository.InsertOrUpdate(Mapper.Map<ViewModelCategory, Category>(vCategory));
            _iCategoryRepository.Save();        
        }

        public ViewModelCategory GetDetail(int Id)
        {
            var result = _iCategoryRepository.Find(Id);
            if (result != null)
            {
                return Mapper.Map<Category, ViewModelCategory>(result);
            }
            return null;
        }

        public void InsertOrUpdate(ViewModelCategory vCategory)
        {
            try
            {
                _iCategoryRepository.InsertOrUpdate(Mapper.Map<ViewModelCategory, Category>(vCategory));
                _iCategoryRepository.Save();
            }
            catch (Exception e)
            {

            }
        }

        public ViewModelCategory Delete(ViewModelCategory vCategory)
        {
            try
            {
                var thisCate = _iCategoryRepository.Find(vCategory.Id);
                if (thisCate != null)
                {
                    if (thisCate.ParentId != null)
                    {
                        var allChildren = _iCategoryRepository.All.Where(x => x.ParentId == vCategory.Id).ToList();
                        foreach (var i in allChildren)
                        {
                            i.ParentId = thisCate.ParentId;
                            _iCategoryRepository.InsertOrUpdate(i);
                        }
                        _iCategoryRepository.Save();
                    }
                    _iCategoryRepository.Delete(vCategory.Id);
                    _iCategoryRepository.Save();
                }
                return vCategory;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
    }
}
