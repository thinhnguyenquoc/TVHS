using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Web.Controllers
{
    public class PredictionController : Controller
    {
        IPredictionService _iPredictionService;
        IProgramService _iProgramService;
        public PredictionController(IPredictionService iPredictionService, IProgramService iProgramService)
        {
            _iPredictionService = iPredictionService;
            _iProgramService = iProgramService;
        }
        //
        // GET: /QuantityPredictation/
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<ViewModelProgram> progamList = _iProgramService.GetAllProgram();
            foreach (var prog in progamList) { 
                items.Add(new SelectListItem { Text = prog.Name, Value = prog.ProgramCode });
            }
            ViewBag.programCode = items;
            List<SelectListItem> items2 = new List<SelectListItem>();
            items2.Add(new SelectListItem { Text = "1", Value = "1" });
            items2.Add(new SelectListItem { Text = "2", Value = "2" });
            items2.Add(new SelectListItem { Text = "3", Value = "3" });
            items2.Add(new SelectListItem { Text = "4", Value = "4" });
            items2.Add(new SelectListItem { Text = "5", Value = "5" });
            ViewBag.timeFrames = items2;
            return View();
        }

        [HttpPost]
        public ActionResult Predict(string programCode, string timeFrames)
        {
            var program = _iProgramService.GetProgramByCode(programCode);
            int noTimes = Convert.ToInt32(timeFrames);
            int result = _iPredictionService.QuantityPredict(programCode, noTimes);
            ViewBag.expectedResult = result;
            ViewBag.timeFrames = timeFrames;
            return View(program);
        }
	}
}