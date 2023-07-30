using Microsoft.AspNetCore.Mvc;
using Pearogram.IRepository;
using Pearogram.Models;

namespace Pearogram.Controllers
{
    public class ProductController : Controller
    {

  
        private readonly IProductRepository productRepository;
        private readonly ISupplierRepository supplierRepository;

        public ProductController(IProductRepository productRepository ,ISupplierRepository supplierRepository)
        {
            this.productRepository = productRepository;
            this.supplierRepository = supplierRepository;   
        }
        public IActionResult Index()
        {
            //List<Product> products = _context.Products.ToList();
            List<Product> products = productRepository.GetAll();
            return View(products);
        }

        public async Task< IActionResult> Details(int id)
        {
         
            Product product=await productRepository.GetById(id);  
            if(product != null)
            {

                return View(product);
            }

            TempData["ProductDetails"] = " product Details Not founded";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
           ViewBag.SupplierID =  supplierRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
               var result=await productRepository.Insert(product);
                if (result == 1)
                {
                    TempData["successInsert"] = " product is added successfully";
                    return RedirectToAction(nameof(Index));
                }         
                else { TempData["failInsert"] = " product  failed added"; }
                return View(product);
            }
            TempData["failInsert"] = " product  failed added";
            ViewBag.SupplierID = supplierRepository.GetAll();
            return  View(product); ;
        }

        [HttpGet]
        public async Task<IActionResult> Updata(int id)
        {
            Product product=await productRepository.GetById(id);
            ViewBag.SupplierID = supplierRepository.GetAll();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Updata(Product product)
        {
            if (ModelState.IsValid)
            {
                int Result =await productRepository.Update(product);
                if (Result == 1)
                {
                    TempData["successUpdate"] = " product is Update successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["failUpdate"] = " product  failed Update";
                ViewBag.SupplierID = supplierRepository.GetAll();
                return View(product);
            }
            TempData["failUpdate"] = " product  failed Update";
            ViewBag.SupplierID = supplierRepository.GetAll();
            return View(product);
        }

        public async Task<IActionResult> Delete (int id) 
        {
            int result = await productRepository.Delete(id);
            if (result == 1)
            {
                TempData["successDelete"] = " product is Delete successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["failDelete"] = " product  failed Delete";
            return RedirectToAction(nameof(Index));
        }

        #region Validation
        public async Task<IActionResult> IsExists(string productName, int SupplierID)
        {
            Product p = await productRepository.GetByName(productName);
            if (p != null && p.SupplierID == SupplierID)
                return Json(false);
            return Json(true);
        }
        #endregion
    }
}
