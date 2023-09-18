using BookStoer_Razor.Data;
using BookStoer_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoer_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Category Category { get; set; }
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();

                /*Success flash message*/
                TempData["Success"] = "Category created successfully";

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
