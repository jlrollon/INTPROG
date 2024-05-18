using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace INTPROG.Models
{
    public class RegisterModel : PageModel 
    {
        private string connectionString = "Data Source=JLROLLON\\SQLEXPRESS;Initial Catalog=DB_INTPROG;Integrated Security=True;Encrypt=False;";

        [BindProperty]
        [Required(ErrorMessage = "Fullname is required.")]
        public string fullname { get; set; }  

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }  

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string register = @"EXEC SP_REGISTER
                                      @NAME = @fullname,
                                      @EMAIL = @email,
                                      @PASSWORD = @password";

                    using (SqlCommand cmd = new SqlCommand(register, connection))
                    {
                        cmd.Parameters.AddWithValue("@fullname", fullname);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();
                        TempData["message"] = "Registration Successful !";
                    }
                }
                return RedirectToPage("/register");
            }
            catch (SqlException sqlEx)
            {
                ModelState.AddModelError("", $"SQL Error: {sqlEx.Message}");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
