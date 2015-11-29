using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _iProductService;
        public ProductController(IProductService iProductService, ILog logger)
        {
            _iProductService = iProductService;
            logger.Info("test");
        }
        // GET: Product
        public ActionResult Index()
        {
            List<ViewModelProduct> result = _iProductService.GetAllProduct();
            return View(result);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var result = _iProductService.GetDetail(id);
            return View(result);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ViewModelProduct vproduct)
        {
            try
            {
                // TODO: Add insert logic here
                _iProductService.AddProduct(vproduct);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _iProductService.GetDetail(id);
            return View(result);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ViewModelProduct collection)
        {
            try
            {
                // TODO: Add update logic here
                _iProductService.InsertOrUpdate(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _iProductService.GetDetail(id);
            return View(result);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ViewModelProduct collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = _iProductService.Delete(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
