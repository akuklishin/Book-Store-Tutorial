using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;

namespace Book_Store.Areas.Admin.Controllers
{

    //The above path was automatically adjusted by pressing "OK" in the pop up window when moving file to area
    //But there is one more option to guide to the area:
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();


            return View(objProductList);
        }

        public IActionResult Upsert(int? id) {//Update + Insert
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new() {
                CategoryList = CategoryList,
                Product = new Product()
            };

            if(id == null || id == 0) {
                //create
                return View(productVM);
            }
            else {
                //update
                //productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties:"ProductImages");
                return View(productVM);
            }
        }

        [HttpPost]        
        public IActionResult Upsert(ProductVM productVM, List<IFormFile> files) {
            if (ModelState.IsValid){
                if (productVM.Product.Id == null || productVM.Product.Id == 0){
                    //create new product
                    _unitOfWork.Product.Add(productVM.Product);                   
                }
                else {
                    //update existing product
                    _unitOfWork.Product.Update(productVM.Product);                   
                }
                _unitOfWork.Save();
                //Once Product is saved here, productVM will the productId for new product create too.

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null) {
                    foreach (IFormFile file in files) {
                        //To rename file name with GUID (globally unique identifier) preserving original extension
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);                        
                        string productPath = @"images\products\product-" + productVM.Product.Id;
                        //images to be saved inside "wwwroot\images\products\product-ProductId"
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath)){
                            //if folder "product-ProductId" is not present it will be created
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create)){
                            file.CopyTo(fileStream);
                        }
                        ProductImage productImage = new() {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = productVM.Product.Id
                        };

                        //if Product.ProductImages == null then it will create an empty list to avoid NullPointException
                        if (productVM.Product.ProductImages == null){
                            productVM.Product.ProductImages = new List<ProductImage>();
                        }
                        //save image path inside the list of images that is Product.ProductImages
                        productVM.Product.ProductImages.Add(productImage);
                    }
                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                    /*Note: here don't need to save images url separately in ProductImages Table. On ProductRepository one line
                      of code to update ProductImage will also update the corresponding records in ProductImages table */
                }
                //to flash success message for toastr
                TempData["Success"] = "Product created/updated successfully";
                return RedirectToAction("Index");
            }
            else {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }  
        }


        //API EDIT and DELETE
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            //delete old image
            //var oldImagePath = 
            //    Path.Combine(_webHostEnvironment.WebRootPath, 
            //    productToBeDeleted.ImageUrl.TrimStart('\\'));

            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}

            _unitOfWork.Product.Remove(productToBeDeleted);
            TempData["Success"] = "Product deleted successfully";
            _unitOfWork.Save();


            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

        /* 
        public IActionResult Edit(int? id){
            if (id == null || id == 0){
                return NotFound();
            }

            *//*DbSetFinder object in DB by id, 3 different ways:*//*
            Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            *//*Product productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);*/
            /*Product productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();*//*

            if (productFromDb == null){
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj){
            if (ModelState.IsValid){
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }*/

        /*public IActionResult Delete(int? id){
            if (id == null || id == 0){
                return NotFound();
            }

            *//*DbSetFinder object in DB by id, 3 different ways:*//*
            Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null){
                return NotFound();
            }
            return View(productFromDb);
        }*/

        /*[HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id){
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null){
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }*/
    }
}


