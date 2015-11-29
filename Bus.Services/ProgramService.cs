﻿using AutoMapper;
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
    public class ProgramService : IProgramService
    {
        IProgramRepository _iProgramRepository;
        IHelper _iHelper;
        public ProgramService(IProgramRepository iProgramRepository, IHelper iHelper)
        {
            _iProgramRepository = iProgramRepository;
            _iHelper = iHelper;
        }
        
        public List<ViewModelProgram> GetAllProgram()
        {
            return Mapper.Map<List<Program>, List<ViewModelProgram>>(_iProgramRepository.All.ToList());
        }

        public void AddProgram(ViewModelProgram vProgram)
        {
            _iProgramRepository.InsertOrUpdate(Mapper.Map<ViewModelProgram, Program>(vProgram));
            _iProgramRepository.Save();        
        }

        public ViewModelProgram GetDetail(int Id)
        {
            var result = _iProgramRepository.Find(Id);
            if (result != null)
            {
                return Mapper.Map<Program, ViewModelProgram>(result);
            }
            return null;
        }

        public void InsertOrUpdate(ViewModelProgram vProgram)
        {
            try
            {
                _iProgramRepository.InsertOrUpdate(Mapper.Map<ViewModelProgram, Program>(vProgram));
                _iProgramRepository.Save();
            }
            catch (Exception e)
            {

            }
        }

        public ViewModelProgram Delete(ViewModelProgram vProgram)
        {
            try
            {
                _iProgramRepository.Delete(vProgram.Id);
                _iProgramRepository.Save();
                return vProgram;
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
                string programCodeTabName = ConfigurationManager.AppSettings["ProgramCodeTabName"];
                // get anchor STT
                string headerRowKey = ConfigurationManager.AppSettings["HeaderRowKey"];
                for (int i = 0; i < _iWorkbook.NumberOfSheets; i++)
                {
                    if (_iWorkbook.GetSheetName(i).ToLower().Contains(programCodeTabName.ToLower()))
                    {
                        ISheet sheet = _iWorkbook.GetSheetAt(i);
                        List<Program> listProgram = new List<Program>();                      
                        List<int> target = _iHelper.StartPoint(sheet, headerRowKey);
                        if (target != null)
                        {
                            for (int j = target.FirstOrDefault() + 1; j <= sheet.LastRowNum; j++)
                            {
                                var row = sheet.GetRow(j);
                                if (row.GetCell(target.LastOrDefault()) != null && !string.IsNullOrEmpty(row.GetCell(target.LastOrDefault() + 3).StringCellValue.ToString()))
                                {                                    
                                    Program program = new Program();
                                    int index = target.LastOrDefault();
                                    if ((row.GetCell(index + 3) != null))
                                    {
                                        program.Name = row.GetCell(index + 1) != null?row.GetCell(index + 1).StringCellValue.ToString():"";                                        
                                        program.Duration = row.GetCell(index + 2) != null ? "0:" + Convert.ToDateTime(row.GetCell(index + 2).DateCellValue.ToString()).Minute.ToString() + ":" + Convert.ToDateTime(row.GetCell(index + 2).DateCellValue.ToString()).Second.ToString() : "";                                
                                        program.ProgramCode = row.GetCell(index + 3).StringCellValue.ToString();
                                        program.Note = row.GetCell(index + 4) != null ? row.GetCell(index + 4).StringCellValue.ToString() : "";                                    
                                        // add more
                                        program.Category = row.GetCell(index + 7) != null ? row.GetCell(index + 7).StringCellValue.ToString() : "";                                     
                                        program.Price = row.GetCell(index + 8) != null ? Convert.ToInt32(row.GetCell(index + 8).NumericCellValue.ToString()) : 0;                                      
                                    }                                   
                                    var existProgram = _iProgramRepository.All.Where(x => x.ProgramCode == program.ProgramCode).FirstOrDefault();
                                    if (existProgram != null)
                                    {
                                        program.Id = existProgram.Id;
                                    }
                                    _iProgramRepository.InsertOrUpdate(program);                                   
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