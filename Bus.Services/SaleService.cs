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
    public class SaleService : ISaleService
    {
        ISaleRepository _iSaleRepository;
        IHelper _iHelper;
        public SaleService(ISaleRepository iSaleRepository, IHelper iHelper)
        {
            _iSaleRepository = iSaleRepository;
            _iHelper = iHelper;
        }
        
        public List<ViewModelSale> GetAllSale()
        {
            return Mapper.Map<List<Sale>, List<ViewModelSale>>(_iSaleRepository.All.ToList());
        }

        public void AddSale(ViewModelSale vSale)
        {
            _iSaleRepository.InsertOrUpdate(Mapper.Map<ViewModelSale, Sale>(vSale));
            _iSaleRepository.Save();        
        }

        public ViewModelSale GetDetail(int Id)
        {
            var result = _iSaleRepository.Find(Id);
            if (result != null)
            {
                return Mapper.Map<Sale, ViewModelSale>(result);
            }
            return null;
        }

        public void InsertOrUpdate(ViewModelSale vSale)
        {
            try
            {
                _iSaleRepository.InsertOrUpdate(Mapper.Map<ViewModelSale, Sale>(vSale));
                _iSaleRepository.Save();
            }
            catch (Exception e)
            {

            }
        }

        public ViewModelSale Delete(ViewModelSale vSale)
        {
            try
            {
                _iSaleRepository.Delete(vSale.Id);
                _iSaleRepository.Save();
                return vSale;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void AddSaleFromFile(Stream inputStream, string fileName)
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
                string saleCodeTabName = ConfigurationManager.AppSettings["SaleCodeTabName"];
                for (int i = 0; i < _iWorkbook.NumberOfSheets; i++)
                {
                    if (_iWorkbook.GetSheetName(i).ToLower().Contains(saleCodeTabName.ToLower()))
                    {
                        ISheet sheet = _iWorkbook.GetSheetAt(i);
                        List<Sale> listProgram = new List<Sale>();
                        for (int j = 3; j <= sheet.LastRowNum; j++)
                        {
                            var row = sheet.GetRow(j);
                            if (row.GetCell(0) != null && !string.IsNullOrEmpty(row.GetCell(0).NumericCellValue.ToString()))
                            {                            
                                for (int k = 2; k <= row.LastCellNum; k++)
                                {
                                    if (sheet.GetRow(2).GetCell(k) != null && row.GetCell(0).NumericCellValue.ToString() != "" && row.GetCell(0).NumericCellValue.ToString() != "0")
                                    {
                                        Sale sale = new Sale();
                                        sale.ProductCode = Convert.ToInt32(row.GetCell(0).NumericCellValue.ToString());
                                        sale.Date = Convert.ToDateTime(sheet.GetRow(2).GetCell(k).DateCellValue.ToString());
                                        sale.Quantity = row.GetCell(k) != null ? Convert.ToInt32(row.GetCell(k).NumericCellValue.ToString()) : 0;
                                        _iSaleRepository.InsertOrUpdate(sale);
                                    }
                                }
                            }
                        }
                        _iSaleRepository.Save();
                    }
                }
               
            }
        }
    }
}
