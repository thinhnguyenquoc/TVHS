using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVHS.Services.Interfaces;

namespace TVHS.Services
{
    public class Helper: IHelper
    {
        public Helper()
        {

        }

        public List<int> StartPoint(ISheet sheet, string anchorKey)
        {
            List<int> target = new List<int>();
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                var cell = sheet.GetRow(i).Cells.Where(x => x.CellType == CellType.String && x.StringCellValue.ToLower().Contains(anchorKey.ToLower())).LastOrDefault();
                if (cell != null)
                {
                    target.Add(cell.RowIndex);
                    target.Add(cell.ColumnIndex);
                    return target;
                }
            }
            return null;
        }

        public bool CheckSchedule(ISheet sheet, string anchorKey)
        {
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                var cell = sheet.GetRow(i).Cells.Where(x => x.CellType == CellType.String && x.StringCellValue.ToLower().Contains(anchorKey.ToLower())).LastOrDefault();
                if (cell != null)
                {
                    return true;
                }
            }
            return false;
        }

        public List<DateTime> GetTimeList(ISheet sheet, string anchorKey)
        {
            var datestring = "";
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                var cell = sheet.GetRow(i).Cells.Where(x => x.CellType == CellType.String && x.StringCellValue.ToLower().Contains(anchorKey.ToLower())).LastOrDefault();
                if (cell != null)
                {
                    datestring = cell.StringCellValue;
                    break;
                }
            }
            var date = datestring.Split(new string[] { anchorKey }, StringSplitOptions.None).LastOrDefault();
            List<DateTime> TimeList = new List<DateTime>();
            DateTime startday = new DateTime(1990, 1, 1);
            DateTime lastday = new DateTime(1990, 1, 1);
            var format = "d/M/yyyy";
            var provider = new CultureInfo("fr-FR");
            if (date.Contains('-'))
            {
                var datelist = date.Split('-');
                startday = DateTime.ParseExact(datelist.FirstOrDefault().Trim(), format, provider);
                lastday = DateTime.ParseExact(datelist.LastOrDefault().Trim(), format, provider);
            }
            else if ((date.Contains(',') || date.Contains(';')) && !date.Contains('&'))
            {
                var datelist = date.Split(new char[] { ';', ',' });
                lastday = DateTime.ParseExact(datelist.LastOrDefault().Trim(), format, provider);
                startday = new DateTime(lastday.Year, lastday.Month, Convert.ToInt32(datelist.FirstOrDefault().Trim()));
            }
            else
            {
                List<string> datelist = new List<string>();
                if(date.Contains("đến")){
                    datelist = date.Split(new string[] {"đến"}, StringSplitOptions.None).ToList();
                    var start = Convert.ToDateTime(datelist.FirstOrDefault().ToString(),provider);
                    var end = Convert.ToDateTime(datelist.Last().ToString(), provider);
                    while (start <= end)
                    {
                        TimeList.Add(start);
                        start = start.AddDays(1);
                    }
                    return TimeList;
                }
                else{
                    datelist = date.Split(new char[] { ';', ',', '&'}).ToList();
                }
                if (datelist.Count() == 1)
                {
                    lastday = DateTime.ParseExact(datelist.LastOrDefault().Trim(), format, provider);
                    startday = DateTime.ParseExact(datelist.FirstOrDefault().Trim(), format, provider);
                }
                else if (datelist.Count() > 2)
                {
                    lastday = DateTime.ParseExact(datelist.LastOrDefault().Trim(), format, provider);
                    if (lastday.Month != 1)
                    {
                        startday = new DateTime(lastday.Year, lastday.Month - 1, Convert.ToInt32(datelist.FirstOrDefault().Trim()));
                    }
                    else
                    {
                        startday = new DateTime(lastday.Year - 1, 12, Convert.ToInt32(datelist.FirstOrDefault().Trim()));
                    }
                }
                else
                {
                }
            }
            if (startday.Year != 1990)
            {
                while (startday <= lastday)
                {
                    TimeList.Add(startday);
                    startday = startday.AddDays(1);
                }
            }
            return TimeList;
        }
    }
}