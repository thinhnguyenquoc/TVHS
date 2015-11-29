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
    public class ScheduleService : IScheduleService
    {
        IScheduleRepository _iScheduleRepository;
        IHelper _iHelper;
        public ScheduleService(IScheduleRepository iScheduleRepository, IHelper iHelper)
        {
            _iScheduleRepository = iScheduleRepository;
            _iHelper = iHelper;
        }
        
        public List<ViewModelSchedule> GetAllSchedule()
        {
            return Mapper.Map<List<Schedule>, List<ViewModelSchedule>>(_iScheduleRepository.All.ToList());
        }

        public void AddSchedule(ViewModelSchedule vSchedule)
        {
            _iScheduleRepository.InsertOrUpdate(Mapper.Map<ViewModelSchedule, Schedule>(vSchedule));
            _iScheduleRepository.Save();        
        }

        public ViewModelSchedule GetDetail(int Id)
        {
            var result = _iScheduleRepository.Find(Id);
            if (result != null)
            {
                return Mapper.Map<Schedule, ViewModelSchedule>(result);
            }
            return null;
        }

        public void InsertOrUpdate(ViewModelSchedule vSchedule)
        {
            try
            {
                _iScheduleRepository.InsertOrUpdate(Mapper.Map<ViewModelSchedule, Schedule>(vSchedule));
                _iScheduleRepository.Save();
            }
            catch (Exception e)
            {

            }
        }

        public ViewModelSchedule Delete(ViewModelSchedule vSchedule)
        {
            try
            {
                _iScheduleRepository.Delete(vSchedule.Id);
                _iScheduleRepository.Save();
                return vSchedule;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void AddScheduleFromFile(Stream inputStream, string fileName)
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
                string scheduleSign = ConfigurationManager.AppSettings["ScheduleSign"];
                string showDaySign = ConfigurationManager.AppSettings["ShowDaySign"];
                string headerRowKey = ConfigurationManager.AppSettings["HeaderRowKey"];
                for (int i = 0; i < _iWorkbook.NumberOfSheets; i++)
                {
                    ISheet sheet = _iWorkbook.GetSheetAt(i);                
                    if (_iHelper.CheckSchedule(sheet, scheduleSign))
                    {
                        var listTime = _iHelper.GetTimeList(sheet, showDaySign);
                        var startDay = listTime.FirstOrDefault();
                        var lastDay = listTime.LastOrDefault();
                        var startPoint = _iHelper.StartPoint(sheet, headerRowKey);
                        while (startDay <= lastDay)
                        {
                            // add data
                            for (int j = startPoint.First() + 1; j <= sheet.LastRowNum; j++)
                            {
                                var row = sheet.GetRow(j);
                                Schedule schedule = new Schedule();
                                if (row.GetCell(startPoint.Last()) != null && row.GetCell(startPoint.Last() + 4) != null)
                                {                     
                                    schedule.ProgramCode = row.GetCell(startPoint.Last() + 4).StringCellValue.ToString();
                                    if (schedule.ProgramCode != "")
                                    {
                                        var mytime = row.GetCell(startPoint.Last() + 1).DateCellValue;
                                        schedule.Date = new DateTime(startDay.Year, startDay.Month, startDay.Day, mytime.Hour, mytime.Minute, mytime.Second);
                                        _iScheduleRepository.InsertOrUpdate(schedule);
                                    }
                                }
                            }                         
                            startDay = startDay.AddDays(1);
                        }
                        _iScheduleRepository.Save();
                    }
                }
            }
        }
    }
}
