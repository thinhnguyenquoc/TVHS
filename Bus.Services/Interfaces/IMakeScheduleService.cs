using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVHS.ViewModels;

namespace TVHS.Services.Interfaces
{
    public interface IMakeScheduleService
    {
        List<ViewModelProgram> makeSchedule(List<ViewModelProgram> programlist, int hours, DateTime limitedDate);
    }
}
