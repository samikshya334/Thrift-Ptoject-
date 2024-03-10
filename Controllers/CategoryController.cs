using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrift_Us.Services;
using Thrift_Us.ViewModel.Category;

namespace Thrift_Us.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {
        private readonly IThriftService _service;

        public CategoryController(IThriftService service)
        {
            _service = service;
        }

        [HttpGet]

        public IActionResult Index()
        {
            var data = _service.GetAllCategories();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            _service.CreateCategory(vm);
            TempData["message"] = "Category Details Saved Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Details(int id)
        {
            var category = _service.GetCategoryDetails(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]

        public IActionResult Edit(int id)
        {
            var viewModel = _service.GetCategoryForEdit(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Edit(CategoryEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.EditCategory(vm);
                TempData["message"] = "Edit Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            _service.DeleteCategory(id);
            TempData["message"] = "Edit Successfully";
            return RedirectToAction("Index");
        }
    }
}