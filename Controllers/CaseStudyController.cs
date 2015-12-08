using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Web.Controllers
{
    public class CaseStudyController : Controller
    {
        IProgramService _iProgramService;
        IMakeScheduleService _iMakeScheduleService;
        IScheduleService _iScheduleService;

        public CaseStudyController(IProgramService iProgramService, IMakeScheduleService iMakeScheduleService, IScheduleService iScheduleService)
        {
            _iProgramService = iProgramService;
            _iMakeScheduleService = iMakeScheduleService;
            _iScheduleService = iScheduleService;
        }
        // GET: CaseStudy
        public ActionResult Index()
        {
            var choosenList2 = _iScheduleService.GetAllSchedule().Where(x => x.Date < new DateTime(2015, 8, 30, 23, 59, 59) && x.Date > new DateTime(2015, 8, 30, 0, 0, 0)).Select(x => x.ProgramCode).Distinct().ToList();            
            List<ViewModelProgram> progamList2 = _iProgramService.GetAllProgramsHaveProduct().Where(x => choosenList2.Contains(x.ProgramCode)).ToList();
            ViewBag.realProgram = progamList2;
            List<ViewModelProgram> progamList1 = _iProgramService.GetAllProgram().Where(x => choosenList2.Contains(x.ProgramCode)).ToList();
            ViewBag.originalProgram = progamList1;

            ViewBag.schedule = choosenList2;
            return View();
        }

        // GET: MakeSchedule
        //public ActionResult BroadcastSchedule()
        //{
        //    //var choosenList = list.Where(x => x.Checked == true).Select(x => x.Id).ToList();
        //    //List<ViewModelProgram> progamList = _iProgramService.GetAllProgramsHaveProduct().Where(x => choosenList.Contains(x.Id)).ToList();
        //    //var choosenList2 = _iScheduleService.GetAllSchedule().Where(x => x.Date < new DateTime(2015, 8, 30, 23, 59, 59) && x.Date > new DateTime(2015, 8, 30, 0, 0, 0)).Select(x => x.ProgramCode).Distinct().ToList();
        //    //List<ViewModelProgram> progamList2 = _iProgramService.GetAllProgramsHaveProduct().Where(x => choosenList2.Contains(x.ProgramCode)).ToList();
        //    //var result = _iMakeScheduleService.makeSchedule(progamList2);
        //    //return View(result);
        //}
    }
}