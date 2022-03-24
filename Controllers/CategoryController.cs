using App.Data.Interface;
using App.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModels;
using webapp.Services;

namespace webapp.Controllers
{
    public class CategoryController : Controller
    {



        private readonly ICategoryServices categoryServices;


        public CategoryController(ICategoryServices _categoryServices)
        {
            this.categoryServices = _categoryServices;


        }
        // GET: CategoryController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await categoryServices.GetAll();
            return View(model);

        }
        // GET: CategoryController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CategoryViewModel model = await categoryServices.Get(id);
            return View(model);
        }



        // GET: CategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await categoryServices.Add(model);
                    return RedirectToAction(nameof(Index));
                }
                return View();

            }
            catch
            {
                return View();
            }
        }
        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }
            CategoryViewModel model = await categoryServices.Get(id);
            return View(model);
        }
        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {

                    await categoryServices.Update(model);
                    return RedirectToAction(nameof(Index));
                }
                return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            CategoryViewModel model = await categoryServices.Get(id);
            return View(model);

        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryViewModel collection)
        {
            try
            {
                if (id != null)
                {
                    categoryServices.Remove(id);
                    return RedirectToAction(nameof(Index));
                }
                return View();
               
            }
            catch
            {
                return View();
            }
        }
    }
}
