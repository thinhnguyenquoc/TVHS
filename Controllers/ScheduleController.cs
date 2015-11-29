using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;

namespace TVHS.Web.Controllers
{
    public class ScheduleController : Controller
    {
        IScheduleService _iScheduleService;
        public ScheduleController(IScheduleService iScheduleService)
        {
            _iScheduleService = iScheduleService;
        }
        // GET: Schedule
        public ActionResult Index()
        {
            var result = _iScheduleService.GetAllSchedule();
            return View(result);
        }

        // GET: Schedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schedule/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    Stream fileStream = file.InputStream;
                    var fileName = Path.GetFileName(file.FileName);
                    _iScheduleService.AddScheduleFromFile(fileStream, fileName);
                    //var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    //file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
