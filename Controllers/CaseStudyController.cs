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
            // 30/08/2015
            var choosenList2 = _iScheduleService.GetAllSchedule().Where(x => x.Date < new DateTime(2015, 8, 30, 23, 59, 59) && x.Date > new DateTime(2015, 8, 30, 0, 0, 0)).Select(x => x.ProgramCode).Distinct().ToList();
            List<ViewModelProgram> progamList2 = _iProgramService.GetAllProgramsHaveQuantity(new DateTime(2015, 8, 30)).Where(x => choosenList2.Contains(x.ProgramCode)).ToList();
            ViewBag.realProgram = progamList2;
            List<ViewModelProgram> progamList1 = _iProgramService.GetAllProgram().Where(x => choosenList2.Contains(x.ProgramCode)).ToList();
            ViewBag.originalProgram = progamList1;

            // 31/08/2015
            var choosenList2next = _iScheduleService.GetAllSchedule().Where(x => x.Date < new DateTime(2015, 8, 31, 23, 59, 59) && x.Date > new DateTime(2015, 8, 31, 0, 0, 0)).Select(x => x.ProgramCode).Distinct().ToList();
            List<ViewModelProgram> progamList2next = _iProgramService.GetAllProgramsHaveQuantity(new DateTime(2015, 8, 31)).Where(x => choosenList2next.Contains(x.ProgramCode)).ToList();
            ViewBag.realProgramnext = progamList2next;
            List<ViewModelProgram> progamList1next = _iProgramService.GetAllProgram().Where(x => choosenList2next.Contains(x.ProgramCode)).ToList();
            ViewBag.originalProgramnext = progamList1next;

            // predict 31/08/2015
            var result = _iMakeScheduleService.makeSchedule(progamList2next, 12, new DateTime(2015, 8, 31));
            ViewBag.predictResult = result;

            // real result 31/08/2015
            var realResult = _iProgramService.GetProgramQuantity(progamList2next, new DateTime(2015, 8, 31));
            ViewBag.realresult = realResult;

            // compare 
            var compare = new List<ViewModelProgram>();
            for (int i = 0; i < result.Count(); i++)
            {
                if (result[i].quantityList.FirstOrDefault().NoTimes == realResult[i].quantityList.FirstOrDefault().NoTimes)
                {
                    var temp = new ViewModelProgram();
                    temp.ProgramCode = result[i].ProgramCode;
                    temp.Name = result[i].Name;
                    temp.quantityList = new List<ViewModelQuantity>();
                    ViewModelQuantity a1 = new ViewModelQuantity()
                    {
                        NoTimes = result[i].quantityList.FirstOrDefault().NoTimes,
                        quantity = result[i].quantityList.FirstOrDefault().quantity - realResult[i].quantityList.FirstOrDefault().quantity
                    };
                    temp.quantityList.Add(a1);
                    temp.quantityList.Add(result[i].quantityList.FirstOrDefault());
                    temp.quantityList.Add(realResult[i].quantityList.FirstOrDefault());
                    compare.Add(temp);
                }
            }
            ViewBag.compare = compare;
            return View();
        }       
    }
}