using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Utility;
using System.Security.Claims;

namespace Book_Store.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //get userId
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            if (claim != null){ //user has logged-in
                var userId = claim.Value;
                if(HttpContext.Session.GetInt32(SD.SessionCart) == null){
                    //Session variable SD.SessionCart is not set till now then set it
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
                } 
                
                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else{ //if user is no longer logged in 
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }   
}


