using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVHS.Entities;
using TVHS.Repositories.Interfaces;
using TVHS.Services.Interfaces;

namespace TVHS.Services
{
    public class PredictionService : IPredictionService
    {
        IProgramRepository _iProgramRepository;
        IScheduleRepository _iScheduleRepository;
        ISaleRepository _iSaleRepository;
        ICategoryRepository _iCategoryRepository;
        IProductRepository _iProductRepository;
        DateTime limitedDate;
        List<Program> allPrograms;
        List<Product> allProducts;
        List<Category> allCategorys;
        List<Sale> allSales;
        List<Schedule> allSchedules;
        public PredictionService(IProgramRepository iProgramRepository,
            IProductRepository iProductRepository,
            IScheduleRepository iScheduleRepository, 
            ISaleRepository iSaleRepository, 
            ICategoryRepository iCategoryRepository)
        { 
            _iProgramRepository = iProgramRepository;
            _iScheduleRepository = iScheduleRepository;
            _iSaleRepository = iSaleRepository;
            _iCategoryRepository = iCategoryRepository;
            _iProductRepository = iProductRepository;
            limitedDate = new DateTime(2015, 9, 1);
            allPrograms = _iProgramRepository.All.ToList();
            allProducts = _iProductRepository.All.ToList();
            allCategorys = _iCategoryRepository.All.ToList();
            allSales = _iSaleRepository.All.ToList();
            allSchedules = _iScheduleRepository.All.ToList();
        }

        public bool CheckLive(string programCode)
        {
            var program = allPrograms.Where(x => x.ProgramCode == programCode).FirstOrDefault();
            DateTime dateTime = DateTime.ParseExact(program.Duration, "h:m:s", CultureInfo.InvariantCulture);
            if (dateTime.Minute > 26)
            {
                return true;
            }
            if (program.Name.ToLower().Contains("live"))
            {
                return true;
            }
            return false;
        }

        public int QuantityPredict(string programCode, int noTimes)
        {
            var program = allPrograms.Where(x => x.ProgramCode == programCode).FirstOrDefault();
            var quantity = QuantityPredictByProgram(programCode, noTimes);
            if(quantity == -2)
                return quantity;
            else if (quantity == -1)
            {
                var programGroup = allPrograms.Where(x => x.ProductId == program.ProductId).ToList<Program>();
                // find other programs belong to one product
                if (programGroup.Count() > 1)
                {
                    foreach(var pro in programGroup){
                        if(pro.ProgramCode != programCode && (CheckLive(pro.ProgramCode) == CheckLive(programCode))){
                            var q = QuantityPredictByProgram(programCode, noTimes);
                            if (q != -1 && q != -2 )
                            {
                                return q;
                            }
                        }
                    }
                }
                // this product has only one program
                //find other products have the same category with this product
                var product = allProducts.Where(x => x.Id == program.ProductId).FirstOrDefault();
                var category = allCategorys.Where(x => x.Id == product.ParentId).FirstOrDefault();
                if(category == null)
                    return -1;
                else
                {
                    var q = recursiveCategory(category.Id, noTimes, CheckLive(programCode));
                    if (q != -1 && q != -2)
                    {
                        return q;
                    }
                    return -1;
                }
            }
            else
            {
                return quantity;
            }

        }

        public int recursiveCategory(int categorytId, int noTimes, bool checklive)
        {
            var categoryLeafs = GetCategoryProduct(categorytId);
            var category = allCategorys.Where(x => x.Id == categorytId).FirstOrDefault();
            foreach (var leaf in categoryLeafs)
            {
                var q = QuantityPredictByCategoryProduct(leaf.Id, noTimes, checklive);
                if (q != -1 && q != -2)
                {
                    return q;
                }
            }
            if (category.ParentId != null)
            {
                return recursiveCategory(category.ParentId.Value, noTimes, checklive);
            }
            else
            {
                return -1;
            }

        }

        public List<Category> GetCategoryProduct(int categorytId)
        {
            var result = new List<Category>();
            var category = allCategorys.Where(x => x.Id == categorytId).FirstOrDefault();
           
            List<Category> children = allCategorys.Where(x => x.ParentId == categorytId).ToList();
            if (children.Count() == 0)
            {
                result.Add(category);
                return result;
            }
            else
            {
                foreach (var child in children)
                {
                    var result1 = GetCategoryProduct(child.Id);
                    result = result.Concat(result1).ToList();
                }
                return result;
            }
        }

        public int QuantityPredictByCategoryProduct(int categorytId, int noTimes, bool checklive)
        {
            var productGroup = allProducts.Where(x => x.ParentId == categorytId).ToList();
            if (productGroup.Count() > 1)
            {
                foreach (var prod in productGroup)
                {                   
                    var q = QuantityPredictByProduct(prod.Id, noTimes, checklive);
                    if (q != -1 && q != -2)
                    {
                        return q;
                    }                   
                }
            }
            return -1;
        }

        public int QuantityPredictByProduct(int productId, int noTimes, bool checklive)
        {
            var programGroup = allPrograms.Where(x => x.ProductId == productId).ToList<Program>();
            // find other programs belong to one product
            if (programGroup.Count() > 0)
            {
                foreach (var pro in programGroup)
                {
                    if (CheckLive(pro.ProgramCode) == checklive)
                    {
                        var q = QuantityPredictByProgram(pro.ProgramCode, noTimes);
                        if (q != -1 && q != -2)
                        {
                            return q;
                        }
                    }
                }
            }
            return -1;
        }

        public int QuantityPredictByProgram(string programCode, int noTimes)
        {
            var program = allPrograms.Where(x => x.ProgramCode == programCode).FirstOrDefault();
            if (program != null)
            {
                //var scheduleDay = allSchedules.Where(x => x.ProgramCode == program.ProgramCode && x.Date.Year != 1 && x.Date < limitedDate).GroupBy(x => EntityFunctions.TruncateTime(x.Date)).ToList().Where(x => x.Count() == noTimes).ToList();
                var scheduleDay = allSchedules.Where(x => x.ProgramCode == program.ProgramCode && x.Date.Year != 1 && x.Date < limitedDate).GroupBy(x => x.Date.Date).ToList().Where(x => x.Count() == noTimes).ToList();               
                // can not find any days when this program is showed noTimes
                if (scheduleDay.Count() == 0)
                {                    
                    return -1;
                }
                // found the days 
                else
                {
                    // find the day which is the closest today
                    var day = scheduleDay.OrderBy(x => x.Key).LastOrDefault().Key;
                    // get the quantity of the recent day
                    var quan = allSales.Where(x => x.ProductCode == program.ProductId && x.Date == day && x.Quantity != 0).FirstOrDefault();
                    if (quan != null)
                        return quan.Quantity;
                    else
                        return -1;
                }
            }
            return -2;           
        }
    }
}
