using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.Models.ViewModels
{
    public class RoleManagementVM {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
