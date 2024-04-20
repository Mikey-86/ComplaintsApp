using Azure;
using ComplaintsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ComplaintsApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> LoginUser(Login login) 
        {
            var token = "";
            //login.RememberMe = true;

            var jsonVariable = JsonConvert.SerializeObject(login);
            var content = new StringContent(jsonVariable, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://localhost:5000/api/accounts", content);
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response into an anonymous object
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    // Access the token property from the response object
                    token = responseObject.token;
                    HttpContext.Response.Cookies.Append("MySessionCookie", token, new CookieOptions
                    {
                        Expires = login.RememberMe ? DateTimeOffset.Now.AddDays(7) : DateTimeOffset.Now.AddHours(1),
                        HttpOnly = true,
                        Secure = true 
                    });

                    return View("/Views/Complaints/Complaints.cshtml");
                }

                else { 
                    return View("Error");
                }
            }
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignupUser(Signup signupObj)
        {
            HttpClient client = new HttpClient();

            var jsonVariable = JsonConvert.SerializeObject(signupObj);
            var content = new StringContent(jsonVariable, Encoding.UTF8, "application/json");

            if (signupObj != null)
            {
                var response = await client.PostAsync("https://localhost:5000/api/accounts", content);

                if (response.IsSuccessStatusCode)
                {
                    //Showing success and allowing the user to return to Login form to login with new account
                    return View("Login");
                }

            }
            else
            {
                //Showing error
                return View("Error");
            }

            return View("Error");
        }
    }
}
