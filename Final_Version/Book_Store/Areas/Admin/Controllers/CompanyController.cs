using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;

namespace Book_Store.Areas.Admin.Controllers
{
    //setting area of access
    [Area("Admin")]
    //setting role of access
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        //dependency injections
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

        //combined method for create+update
        public IActionResult Upsert(int? id) //Update + Insert
        {

            Company Company = new Company(); 

            //if hidden id input is not populated (== null) then create
            if (id == null || id == 0)
            {
                //create
                return View(Company);
            }
            //otherwise update
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
            //if everithing passed the validation
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

                //save to db
                _unitOfWork.Save();

                //redirect to action
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
            
        }

        #region API CALLS
        //display companies
        [HttpGet]
        public IActionResult GetAll()
        {
            //get list of companies
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            //retrun JSONified result for DataTable
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            //get company by id
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

            //if company not found
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            //remove and save
            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();


            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
