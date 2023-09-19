// Import the necessary namespaces for the controller to function properly
using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc; // This provides controller-specific functionalities

// Declare the namespace for the controller
namespace BulkyWeb.Controllers
{
    // Define the CategoryController which inherits from the base Controller class
    public class CategoryController : Controller
    {
        // Declare a private field for the ApplicationDbContext which is used to interact with the database
        private readonly ApplicationDbContext _db;

        // The constructor for the CategoryController
        // It takes ApplicationDbContext as a parameter which is injected by ASP.NET Core's built-in dependency injection
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; // Initialize the private field _db with the passed-in ApplicationDbContext instance
        }

        // The Index action method which handles the default request to the CategoryController
        public IActionResult Index()
        {
            // Retrieve all categories from the database and store them in a List
            List<Category> objCategoryList = _db.Categories.ToList();

            // Return the view associated with this action and pass in the list of categories
            return View(objCategoryList);
        }

        // The Create action method creates a new category
        public IActionResult Create()
        {
            return View();
        }

        // The Create action method to POST a new Category
        [HttpPost]
        public IActionResult Create(Category obj) // adding the (Category obj) so the values of the new category will be added to the db
        {
            // check if obj.Name is equal to obj.DisplayOrder.ToString
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order can not exactly match the Name."); // custom error message
            }

            // check if the model state (category object) is valid = examine all the validations
            if (ModelState.IsValid) 
            { 
                _db.Categories.Add(obj); // keep track of what is being added to the db
                _db.SaveChanges(); // save new Category to the db
                TempData["success"] = "Category created successfully"; // TempData success message
                return RedirectToAction("Index"); // If valid, redirect to Category Index view to see the updated list of Categories
            }
            return View(); // else, if invalid return to the view (where error messages will be displayed)
        }

        // The Edit action method edits an existing category
        public IActionResult Edit(int? id) // we need the id of the Category being edited, we make it nullable with the "?"
        {
            if(id == null || id == 0) // if ID is null or if ID = 0, not a valid ID, return NotFound
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id); // Find method to find category in db based on id
            // Category categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id); // FirstOrDefault method to find category in db based on id
            // Category categoryFromDb = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); // Where Condition method to find category in db based on id
            if (categoryFromDb == null){
                return NotFound();
            }
            return View(categoryFromDb); // if category is found, pass it to our view and display it
        }

        // The Edit action method to POST an edited/updated Category
        [HttpPost]
        public IActionResult Edit(Category obj) // adding the (Category obj) so the values of the edited category will be added to the db
        {
            // Client Side Validation: check if the model state (category object) is valid = examine all the validations
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // based on the id that is populated in the object, the specified category is updated
                _db.SaveChanges(); // save updated Category to the db
                TempData["success"] = "Category updated successfully"; // TempData success message
                return RedirectToAction("Index"); // If valid, redirect to Category Index view to see the updated list of Categories
            }
            return View(); // else, if invalid return to the view (where error messages will be displayed)
        }

        // The Delete action method deletes an existing category
        public IActionResult Delete(int? id) // we need the id of the Category being deleted, we make it nullable with the "?"
        {
            if (id == null || id == 0) // if ID is null or if ID = 0, not a valid ID, return NotFound
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id); // Find method to find category in db based on id
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb); // if category is found, pass it to our view and display it
        }

        // The DeletePost action method to delete a Category
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) // adding the id so the db knows which category to delete
        {
            Category? obj = _db.Categories.Find(id); // first we need to find the category to be deleted in the db
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj); // delete category
            _db.SaveChanges(); // save update to the db
            TempData["success"] = "Category deleted successfully"; // TempData success message
            return RedirectToAction("Index"); // redirect to Category Index view to see the updated list of Categories
        }
    }
}
