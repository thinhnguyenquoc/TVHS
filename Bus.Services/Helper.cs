using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
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
                var cell = sheet.GetRow(i).Cells.Where(x => x.StringCellValue.ToLower().Contains(anchorKey.ToLower())).LastOrDefault();
                if (cell != null)
                {
                    target.Add(cell.RowIndex);
                    target.Add(cell.ColumnIndex);
                    return target;
                }
            }
            return null;
        }
    }
}