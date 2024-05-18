using INTPROG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INTPROG.Pages
{
    public class RegisterPageModel : PageModel
    {
        private readonly RegisterModel _registerModel;

        public RegisterPageModel(RegisterModel registerModel)
        {
            _registerModel = registerModel;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
