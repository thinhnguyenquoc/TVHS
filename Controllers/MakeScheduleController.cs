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
        public ActionResult BroadcastSchedule(List<ViewModelCheckBox> list)
        {
            var choosenList = list.Where(x => x.Checked == true).Select(x=>x.Id).ToList();
            List<ViewModelProgram> progamList = _iProgramService.GetAllProgramsHaveProduct().Where(x=>choosenList.Contains(x.Id)).ToList();
            var result = _iMakeScheduleService.makeSchedule(progamList);
            return View(result);
        }

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