using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key] // primary key is Id
        public int Id { get; set; }
        
        [Required] // Required annotation sets Category name as mandatory
        [MaxLength(30)] // MaxLength sets Category name to be no longer than 30 characters
        [DisplayName("Category Name")] // Add Data Annotation to ensure display name is "Category Name"
        public string Name { get; set; }

        // Add Data Annotation to ensure display name is "Display Order"
        [DisplayName("Display Order")] 
        // Range Annotation sets Display Order int validation to be between 1-100
        // A custom error message is also added
        [Range(1, 100, ErrorMessage ="Display Order must be between 1-100")] 
        public int DisplayOrder { get; set; }
    }
}
