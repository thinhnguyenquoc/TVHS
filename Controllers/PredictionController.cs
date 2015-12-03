using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;

namespace TVHS.Web.Controllers
{
    public class PredictionController : Controller
    {
        IPredictionService _iPredictionService;
        public PredictionController(IPredictionService iPredictionService)
        {
            _iPredictionService = iPredictionService;
        }
        //
        // GET: /QuantityPredictation/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Predict(string programCode, string timeFrames)
        {
            //List<string> tfs = timeFrames.Split(',').ToList();
            programCode = "AZS811"; //Máy khoan cầm tay 103 món D.I.Y
            int noTimes = Convert.ToInt32(timeFrames);
            int result = _iPredictionService.QuantityPredict(programCode, noTimes);
            ViewBag.programCode = programCode;
            ViewBag.timeFrames = timeFrames;
            ViewBag.expectedResult = result;
            return View();
        }
	}
}