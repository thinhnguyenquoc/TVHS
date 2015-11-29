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
                for (int i = 0; i < _iWorkbook.NumberOfSheets; i++)
                {
                    if (_iWorkbook.GetSheetName(i).ToLower().Contains(programCodeTabName.ToLower()))
                    {
                        ISheet sheet = _iWorkbook.GetSheetAt(i);
                        List<Program> listProgram = new List<Program>();
                        // get anchor STT
                        string headerRowKey = ConfigurationManager.AppSettings["HeaderRowKey"];
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
                                        if (row.GetCell(index + 1) != null)
                                        {
                                            program.Name = row.GetCell(index + 1).StringCellValue.ToString();
                                        }
                                        else
                                        {
                                            program.Name = "";
                                        }
                                        if (row.GetCell(index + 2) != null)
                                        {
                                            program.Duration = "0:" + Convert.ToDateTime(row.GetCell(index + 2).DateCellValue.ToString()).Minute.ToString() + ":" + Convert.ToDateTime(row.GetCell(index + 2).DateCellValue.ToString()).Second.ToString();
                                        }
                                        else
                                        {
                                            program.Duration = "";
                                        }
                                        program.ProgramCode = row.GetCell(index + 3).StringCellValue.ToString();
                                       
                                        if (row.GetCell(index + 4) != null)
                                        {
                                            program.Note = row.GetCell(index + 4).StringCellValue.ToString();
                                        }
                                        else
                                        {
                                            program.Note = "";
                                        }
                                        // add more
                                        if (row.GetCell(index + 7) != null)
                                        {
                                            program.Category = row.GetCell(index + 7).StringCellValue.ToString();
                                        }
                                        else
                                        {
                                            program.Category = "";
                                        }

                                        if (row.GetCell(index + 8) != null)
                                        {
                                            program.Price = Convert.ToInt32(row.GetCell(index + 8).NumericCellValue.ToString());
                                        }
                                        else
                                        {
                                            program.Price = null;
                                        }
                                    }

                                    
                                    var existProgram = _iProgramRepository.All.Where(x => x.ProgramCode == program.ProgramCode).FirstOrDefault();
                                    if (existProgram != null)
                                    {
                                        program.Id = existProgram.Id;
                                    }
                                    _iProgramRepository.InsertOrUpdate(program);                                   
                                }
                                else
                                {
                                    break;
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
