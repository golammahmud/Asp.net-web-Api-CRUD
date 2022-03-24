using App.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModel.ViewModels;
using webapp.Data;
using webapp.Services;


namespace webapp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductServices productServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ApplicationDbContext _context;


        public ProductController(IProductServices _productServices, ICategoryServices categoryServices , ApplicationDbContext context)
        {
            this.productServices = _productServices;
            this._categoryServices = categoryServices;
            this._context = context;


        }


        // GET: ProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await productServices.GetAll();
            return View(model);

        }

        // GET: ProductController/Details/5
        [HttpGet]
        public async Task<IActionResult>  Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductViewModel model = await productServices.Get(id);
            return View(model);
        }

        // GET: ProductController/Create
        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            //ProductViewModel p = new ProductViewModel();
            //var model = await _categoryServices.GetAll();


            loadCategory();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0) { 
                         await productServices.Add(model);
                    return RedirectToAction(nameof(Index));
                    }
                   
                }
                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           
            if (id==0)
            {
                return NotFound();
            }
            ProductViewModel model = await productServices.Get(id);
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {

                    await productServices.Update(model);
                    return RedirectToAction(nameof(Index));
                }
                return View();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: ProductController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ProductViewModel model = await productServices.Get(id);
            return View(model);
           
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductViewModel collection)
        {
            try
            {
                if (id != 0)
                {
                    await productServices.Remove(id);
                    return RedirectToAction(nameof(Index));
                }
                return View();
               
            }
            catch
            {
                return View();
            }
        }

        private void loadCategory()
        {
            try
            {
                List<Category> productCategories = new List<Category>();
                productCategories = _context.Category.ToList();
                productCategories.Insert(0,new Category { Id=0,Name="Select Categories"});

                ViewBag.categories = productCategories;


            }
            catch(Exception  ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
