using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Services
{
    public class MakeScheduleService : IMakeScheduleService
    {
        IPredictionService _iPredictionService;
        public MakeScheduleService(IPredictionService iPredictionService)
        {
            _iPredictionService = iPredictionService;
        }

        public List<ViewModelProgram> makeSchedule(List<ViewModelProgram> programlist, DateTime hours, DateTime limitedDate)
        {
            foreach (var item in programlist)
            {
                item.quantityList = new List<ViewModelQuantity>();
                for (int i = 0; i < 5; i++)
                {
                    ViewModelQuantity quantity = new ViewModelQuantity();
                    quantity.NoTimes = i + 1;
                    quantity.quantity = _iPredictionService.QuantityPredict(item.ProgramCode, i + 1, limitedDate);
                    item.quantityList.Add(quantity);
                }
            }
            DateTime totaltime = hours;
            //List<ViewModelProgram> result = FindMaxQuantity(programlist);
            var result = randomAlgrithm(programlist, totaltime);
            return result;
        }

        public List<ViewModelProgram> randomAlgrithm(List<ViewModelProgram> programlist, DateTime totaltime)
        {
            while (true) {
                //Guid seed = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");
                Random a = new Random();

                List<ViewModelProgram> result = new List<ViewModelProgram>();
                foreach (var item in programlist)
                {
                    ViewModelProgram program1 = new ViewModelProgram();
                    double index = a.NextDouble() * 4.0;
                    program1.Name = item.Name;
                    program1.ProgramCode = item.ProgramCode;
                    program1.quantityList = new List<ViewModelQuantity>();
                    program1.quantityList.Add(item.quantityList.ElementAt(Convert.ToInt32(index)));
                    program1.Duration = item.Duration;
                    result.Add(program1);
                }
                int totalminute = 0;
                foreach (var item in result)
                {
                    var no = item.quantityList.FirstOrDefault().NoTimes;
                    DateTime dateTime = DateTime.ParseExact(item.Duration, "h:m:s", CultureInfo.InvariantCulture);
                    totalminute = dateTime.Minute * no;
                }

                if (totalminute < totaltime.Hour * 60 + totaltime.Minute + 1)
                {
                    if (checkValidResult(result))
                    {
                        return result;
                    }
                }
            }

        }

        public bool checkValidResult(List<ViewModelProgram> programlist)
        {
            foreach (var pro in programlist)
            {
                foreach (var index in pro.quantityList)
                {
                   if(index.quantity == -1 || index.quantity == -2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<ViewModelProgram> FindMaxQuantity(List<ViewModelProgram> programlist)
        {
            List<ViewModelProgram> result = new List<ViewModelProgram>();
            var temp = programlist;
            ViewModelProgram max = new ViewModelProgram();
            foreach (var pro in programlist)
            {
                max.ProgramCode = pro.ProgramCode;
                max.ProductName = pro.ProductName;
                max.quantityList = new List<ViewModelQuantity>();
                foreach (var index in pro.quantityList)
                {
                    if (max.quantityList.Count() == 0)
                    {
                        max.quantityList.Add(index);
                    }
                    else
                    {
                        if (max.quantityList.FirstOrDefault().quantity < index.quantity)
                        {
                            max.quantityList.FirstOrDefault().quantity = index.quantity;
                            max.quantityList.FirstOrDefault().NoTimes = index.NoTimes;
                        }
                    }
                }
            }
            result.Add(max);
            ViewModelProgram a = temp.Find(x=>x.ProgramCode == max.ProgramCode);
            temp.Remove(a);
            if (temp.Count() != 0)
            {
                var result2 = FindMaxQuantity(temp);
                return result.Concat(result2).ToList();
            }
            else
            {
                return result;
            }
        }
    }
}
