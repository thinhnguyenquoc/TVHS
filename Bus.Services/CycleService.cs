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
    public class CycleService : ICycleService
    {
        ICycleRepository _iCycleRepository;
        IHelper _iHelper;
        public CycleService(ICycleRepository iCycleRepository, IHelper iHelper)
        {
            _iCycleRepository = iCycleRepository;
            _iHelper = iHelper;
        }
        
        public List<ViewModelCycle> GetAllCycle()
        {
            return Mapper.Map<List<Cycle>, List<ViewModelCycle>>(_iCycleRepository.All.ToList());
        }

        public void AddCycle(ViewModelCycle vCycle)
        {
            _iCycleRepository.InsertOrUpdate(Mapper.Map<ViewModelCycle, Cycle>(vCycle));
            _iCycleRepository.Save();        
        }

        public ViewModelCycle GetDetail(int Id)
        {
            var result = _iCycleRepository.Find(Id);
            if (result != null)
            {
                return Mapper.Map<Cycle, ViewModelCycle>(result);
            }
            return null;
        }

        public void InsertOrUpdate(ViewModelCycle vCycle)
        {
            try
            {
                _iCycleRepository.InsertOrUpdate(Mapper.Map<ViewModelCycle, Cycle>(vCycle));
                _iCycleRepository.Save();
            }
            catch (Exception e)
            {

            }
        }

        public ViewModelCycle Delete(ViewModelCycle vCycle)
        {
            try
            {
                _iCycleRepository.Delete(vCycle.Id);
                _iCycleRepository.Save();
                return vCycle;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void AddCycleFromFile(Stream inputStream, string fileName)
        {
          
        }
    }
}
