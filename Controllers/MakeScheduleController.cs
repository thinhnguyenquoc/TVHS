using System;
using System.Collections.Generic;
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

        public MakeScheduleController(IProgramService iProgramService, IMakeScheduleService iMakeScheduleService)
        {
            _iProgramService = iProgramService;
            _iMakeScheduleService = iMakeScheduleService;
        }

         // GET: MakeSchedule
        public ActionResult Index()
        {
            List<ViewModelProgram> progamList = _iProgramService.GetAllProgramsHaveProduct().OrderByDescending(x=>x.Id).Skip(10).Take(10).ToList();
            var result = _iMakeScheduleService.makeSchedule(progamList);
            return View(result);
        }
    }
}