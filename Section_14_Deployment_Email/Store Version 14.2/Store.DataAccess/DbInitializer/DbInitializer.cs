using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Data;
using Store.Models;
using Store.Utility;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Store.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db){
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void Initialize(){
            //push migrations if they are not applied        
            if (_db.Database.GetPendingMigrations().Count() > 0){
                _db.Database.Migrate();
            }            

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult()) {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();                
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(
                    new ApplicationUser {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        Name = "Admin",
                        PhoneNumber = "5141234567",
                        StreetAddress = "1234 Avenue De Courtrai",
                        City = "Montreal",
                        State = "QC",
                        PostalCode = "H3W1A2"
                    }, "Admin@123").GetAwaiter().GetResult();   //Here "Admin@123" is the password

                //once user is created retrieve the user from database to assign role as Admin
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return; //return back to application
        }
    }
}

