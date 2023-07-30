using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pearogram.IRepository;
using Pearogram.Models;

namespace Pearogram.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        public SupplierController(ISupplierRepository supplierRepository)
        {
                this.supplierRepository = supplierRepository;   
        }
        // GET: SupplierController
        public ActionResult Index()
        {
            return View(supplierRepository.GetAll());
        }

   

        // GET: SupplierController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var ResultCreate =await supplierRepository.Insert(supplier);
                if (ResultCreate == true)
                {
                    TempData["successCreate"] = " Supplier is added successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["failCreate"] = " Failed to add Supplier ";
                return View(supplier); ;
            }
            TempData["failCreate"] = "  Failed to add Supplier ";
            return View(supplier); ;
        }

        // GET: SupplierController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Supplier supplier =await  supplierRepository.GetById(id);  
            if (supplier == null)
            {
                TempData["NotFoundEdit"] = " Supplier is NotFound";
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Supplier supplier)
        {
            if (ModelState.IsValid )
            {
                var Result =await supplierRepository.Update(supplier);
                if(Result == true)
                {
                    TempData["successEdit"] = " Supplier is Edit successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["failEdit"] = " Failed to Edit Supplier ";
                return View(supplier); ;
            }
            TempData["Delete"] = " Failed to Edit Supplier ";
            return View(supplier); ;
        }

       

        
        public async Task<ActionResult> Delete(int id)
        {
            var result =await supplierRepository.Delete(id);
            if (result == true)
            {
                TempData["successDelete"] = " Supplier is Delete successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["failDelete"] = " Failed to Edit Supplier";
            return RedirectToAction(nameof(Index));
        }

        #region Remote Check unique Supplier Name
        public async Task<IActionResult> UniqueSupplier(string SupplierName)
        {
            Supplier s = await supplierRepository.GetByName(SupplierName);
            if (s == null)
                return Json(true);
            return Json(false);
        }
        #endregion
    }
}
