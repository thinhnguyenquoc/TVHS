using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVHS.Services.Interfaces;
using TVHS.ViewModels;

namespace TVHS.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _iCategoryService;
        public CategoryController(ICategoryService iCategoryService){
            _iCategoryService = iCategoryService;
        }
        //
        // GET: /Category/
        public ActionResult Index()
        {
            List<ViewModelCategory> result = _iCategoryService.GetAllCategory();
            List<ViewModelCategory> tree = new List<ViewModelCategory>();
            CreateTree(result, tree);
            return View(tree);
        }

        public void CreateTree (List<ViewModelCategory> result, List<ViewModelCategory> tree){
            int RootId = -1;
            for(int i =0 ; i < result.Count(); i++){
                if(result[i].ParentId == null){
                    RootId = i;
                    break;
                }   
            }
            drawTree(result, RootId, tree);
        }

        public void drawTree(List<TVHS.ViewModels.ViewModelCategory> tree, int rootId, List<ViewModelCategory> result){
            result.Add(tree[rootId]);
            int k = -1;
            for (int i = 0; i < tree.Count(); i++)
            {
                if (tree[i].ParentId == tree[rootId].Id)
                {
                    k = i;
                }
            }
            //leaf
            if(k == -1){
                
            }
            else{
                for (int i = 0; i < tree.Count(); i++)
                {
                    if (tree[i].ParentId == tree[rootId].Id)
                    {
                        drawTree(tree, i, result);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult AddChildNode(ViewModelCategory vm)
        {
            _iCategoryService.InsertOrUpdate(vm);
            return Json("Succeed!",JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteNode(ViewModelCategory vm)
        {
            _iCategoryService.Delete(vm);
            return Json("Succeed!", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RetrieveNode(ViewModelCategory vm)
        {
            var current = _iCategoryService.GetDetail(vm.Id);
            vm.ParentId = current.ParentId;
            _iCategoryService.InsertOrUpdate(vm);
            return Json("Succeed!", JsonRequestBehavior.AllowGet);
        }
	}
}