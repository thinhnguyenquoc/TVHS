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
            programCode = "AZS974"; //Máy khoan cầm tay 103 món D.I.Y
            List<string> tfs = new List<string>();
            tfs.Add("4");
            tfs.Add("6");
            tfs.Add("8");
            int result = _iPredictionService.QuantityPredict(programCode, tfs);
            ViewBag.programCode = programCode;
            ViewBag.timeFrames = timeFrames;
            ViewBag.expectedResult = result;
            return View();
        }
	}
}