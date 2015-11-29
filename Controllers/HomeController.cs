using TVHS.Services.Interfaces;
using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TVHS.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductService _iProductService;
        public HomeController(IProductService IProductService)
        {
            _iProductService = IProductService;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //string connetionString = null;
            //SqlConnection cnn ;
            //connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["BusConnectionString"].ConnectionString;
            //cnn = new SqlConnection(connetionString);
            //try
            //{
            //    cnn.Open();
            //    ViewBag.hello = "Connection Open ! ";
            //    cnn.Close();
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.hello = "Can not open connection ! ";
            //    return View();
            //}
            List<ViewModelProduct> result = _iProductService.GetAllProduct();
            return View(result);
        }

        [HttpPost]
        public ActionResult add(ViewModelProduct vproduct)
        {
            _iProductService.AddProduct(vproduct);
            return RedirectToAction("Index");
        }
	}
}