using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;

namespace TVHS.Web.Controllers
{
    public class SaleController : Controller
    {
        ISaleService _iSaleService;
        public SaleController(ISaleService iSaleService)
        {
            _iSaleService = iSaleService;
        }
        // GET: Sale
        public ActionResult Index()
        {
            var result = _iSaleService.GetAllSale();
            return View(result);
        }

        // GET: Sale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sale/Create
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

        // GET: Sale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sale/Edit/5
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

        // GET: Sale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sale/Delete/5
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
                    _iSaleService.AddSaleFromFile(fileStream, fileName);
                    //var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    //file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
