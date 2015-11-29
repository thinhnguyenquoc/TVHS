using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Services.Interfaces
{
    public interface IHelper
    {
        List<int> StartPoint(ISheet sheet, string anchorKey);
        bool CheckSchedule(ISheet sheet, string anchorKey);
        List<DateTime> GetTimeList(ISheet sheet, string anchorKey);
    }
}
