using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Web.Controllers
{
    public class ProgramController : Controller
    {
        IProgramService _iProgramService;
        public ProgramController(IProgramService iProgramService)
        {
            _iProgramService = iProgramService;
        }
        // GET: Program
        public ActionResult Index()
        {
            var result = _iProgramService.GetAllProgram();
            return View(result);
        }

        // GET: Program/Details/5
        public ActionResult Details(int id)
        {
            var result = _iProgramService.GetDetail(id);
            return View(result);
        }

        // GET: Program/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Program/Create
        [HttpPost]
        public ActionResult Create(ViewModelProgram collection)
        {
            try
            {
                // TODO: Add insert logic here
                _iProgramService.AddProgram(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Program/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _iProgramService.GetDetail(id);
            return View(result);
        }

        // POST: Program/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ViewModelProgram collection)
        {
            try
            {
                // TODO: Add update logic here
                _iProgramService.InsertOrUpdate(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Program/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _iProgramService.GetDetail(id);
            return View(result);
        }

        // POST: Program/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ViewModelProgram collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = _iProgramService.Delete(collection);
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
                    _iProgramService.AddProgramFromFile(fileStream, fileName);
                    //var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    //file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
