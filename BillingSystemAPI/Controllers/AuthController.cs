using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BillingSystemAPI.Models;

namespace BillingSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        // POST api/auth/login
        // Validates credentials and checks that the Role is 'Biller'
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            string connStr = _config.GetConnectionString("BillingDB")!;

            try
            {
                using var conn = new MySqlConnection(connStr);
                conn.Open();

                // Parameterized query — checks credentials and Biller role
                string sql = @"SELECT UserID, FullName, Role
                               FROM   Users
                               WHERE  Username = @Username
                               AND    Password = @Password
                               AND    Role     = 'Biller';";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Username", request.Username.Trim());
                cmd.Parameters.AddWithValue("@Password", request.Password);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Credentials matched and Role is Biller — login success
                    var response = new LoginResponse
                    {
                        UserID = reader.GetInt32("UserID"),
                        FullName = reader.GetString("FullName"),
                        Role = reader.GetString("Role"),
                        Message = "Login successful."
                    };
                    return Ok(response);
                }
                else
                {
                    // No match — wrong credentials or not a Biller
                    return Unauthorized(
                        "Invalid username or password, or account is not authorized for mobile access.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
        }
    }
}

