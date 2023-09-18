using BookStoer_Razor.Data;
using BookStoer_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoer_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Category> CategoryList { get; set; }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
