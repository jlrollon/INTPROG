using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace INTPROG.Pages
{
    public class LoginModel : PageModel
    {

        private string connectionString = "Data Source=JLROLLON\\SQLEXPRESS;Initial Catalog=DB_INTPROG;Integrated Security=True;Encrypt=False;";

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }

        public string errorMsg { get; set; }

        public IActionResult OnPost()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "EXEC SP_LOGIN @EMAIL = @email, @PASSWORD = @password";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            HttpContext.Session.SetString("email", email);
                            return RedirectToPage("/Dashboard");
                        }
                        else
                        {
                            HttpContext.Session.Remove("email");
                            errorMsg = "Invalid username or password!";
                            return Page();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL-related exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
                errorMsg = "An error occurred while accessing the database";
                return Page();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
                errorMsg = "An error occurred while processing your request";
                return Page();
            }
        }
    }
}
