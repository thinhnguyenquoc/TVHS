using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Web.Controllers
{
    public class MakeScheduleController : Controller
    {       
        IProgramService _iProgramService;
        IMakeScheduleService _iMakeScheduleService;
        IScheduleService _iScheduleService;

        public MakeScheduleController(IProgramService iProgramService, IMakeScheduleService iMakeScheduleService, IScheduleService iScheduleService)
        {
            _iProgramService = iProgramService;
            _iMakeScheduleService = iMakeScheduleService;
            _iScheduleService = iScheduleService;
        }

         // GET: MakeSchedule
        public ActionResult BroadcastSchedule(List<ViewModelCheckBox> list)
        {
            var choosenList = list.Where(x => x.Checked == true).Select(x=>x.Id).ToList();
            List<ViewModelProgram> progamList = _iProgramService.GetAllProgramsHaveProduct().Where(x=>choosenList.Contains(x.Id)).ToList();
            var result = _iMakeScheduleService.makeSchedule(progamList, new DateTime(2015, 9, 1 , 9,0,0), new DateTime(2015, 9, 1));
            return View(result);
        }

        //// GET: MakeSchedule
        //public ActionResult BroadcastSchedule(List<ViewModelCheckBox> list)
        //{
        //    var choosenList = list.Where(x => x.Checked == true).Select(x => x.Id).ToList();
        //    List<ViewModelProgram> progamList = _iProgramService.GetAllProgramsHaveProduct().Where(x => choosenList.Contains(x.Id)).ToList();
        //    var choosenList2 = _iScheduleService.GetAllSchedule().Where(x => x.Date < new DateTime(2015, 8, 30, 23, 59, 59) && x.Date > new DateTime(2015, 8, 30, 0, 0, 0)).Select(x => x.ProgramCode).Distinct().ToList();
        //    List<ViewModelProgram> progamList2 = _iProgramService.GetAllProgramsHaveProduct().Where(x => choosenList2.Contains(x.ProgramCode)).ToList();
        //    var result = _iMakeScheduleService.makeSchedule(progamList2);
        //    return View(result);
        //}

        public ActionResult Index()
        {
            List<ViewModelProgram> progamList = _iProgramService.GetAllProgramsHaveProduct().ToList();
            List<ViewModelCheckBox> model = new List<ViewModelCheckBox>();
            foreach (var item in progamList)
            {
                var temp = new ViewModelCheckBox();
                temp.Id = item.Id;
                temp.ProgramCode = item.ProgramCode;
                temp.Name = item.Name;
                temp.Checked = false;
                model.Add(temp);
            }
            return View(model);
        }
    }
}