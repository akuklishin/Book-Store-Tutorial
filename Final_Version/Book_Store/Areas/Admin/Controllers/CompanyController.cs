using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;

namespace Book_Store.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) //Update + Insert
        {
            Company Company = new Company(); 
            if (id == null || id == 0)
            {
                //create
                return View(Company);
            }
            else
            {
                //update
                Company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(Company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {

                if (CompanyObj.Id == null || CompanyObj.Id == 0)
                {
                    //create
                    _unitOfWork.Company.Add(CompanyObj);
                    /*Success flash message*/
                    TempData["Success"] = "Company created successfully";

                }
                else
                {
                    //update
                    _unitOfWork.Company.Update(CompanyObj);
                    /*Success flash message*/
                    TempData["Success"] = "Company updated successfully";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
            
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();


            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
