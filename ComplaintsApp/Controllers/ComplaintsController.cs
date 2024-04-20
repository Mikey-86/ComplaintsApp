using ComplaintsApp.Data;
using ComplaintsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Net.Http.Headers;

namespace ComplaintsApp.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ComplaintsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Complaints()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SubmitComplaint(Complaint complaintObj)
        {
            HttpClient client = new HttpClient();

            var jsonVariable = JsonConvert.SerializeObject(complaintObj);
            var content = new StringContent(jsonVariable, Encoding.UTF8, "application/json");

            if (complaintObj != null)
            {
                var response = await _httpClient.PostAsync("https://localhost:5000/api/complaints", content);

                if (response.IsSuccessStatusCode)
                {
                    //Showing success and allowing the user to return to form
                    return View("Success");
                }

            }
            else
            {
                //Showing error
                return View(SubmitComplaint);
            }

            return View();
        }

        //For dashboard
        public async Task<IActionResult> AllComplaints()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Getting token
                    //var token = HttpContext.Request.Cookies["MySessionCookie"];
                    //if (token != null)
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        // Send a GET request to the API endpoint
                        HttpResponseMessage response = await client.GetAsync("https://localhost:5000/api/complaints");

                        // Check if the response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a string before converting
                            string responseData = await response.Content.ReadAsStringAsync();

                            List<Complaint> complaintObjList = JsonConvert.DeserializeObject<List<Complaint>>(responseData);

                            return View(complaintObjList);
                        }
                        else
                        {

                            return View("SubmitComplaint");
                        }
                    //}
                }
                catch (Exception ex)
                {

                    return View("SubmitComplaint");
                }
            }


            return View();
        }

        public async Task<IActionResult> ComplaintDetails(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Getting token
                    //var token = HttpContext.Request.Cookies["MySessionCookie"];
                    //if (token != null)
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        string url = "https://localhost:5000/api/complaints/" + ID;
                        // Send a GET request to the API endpoint
                        HttpResponseMessage response = await client.GetAsync(url);

                        // Check if the response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a string before converting
                            string responseData = await response.Content.ReadAsStringAsync();

                            Complaint complaintObjList = JsonConvert.DeserializeObject<Complaint>(responseData);

                            return View(complaintObjList);
                        }
                        else
                        {

                            return View("SubmitComplaint");
                        }
                    //}
                }
                catch (Exception ex)
                {

                    return View("SubmitComplaint");
                }
            }

            return View();
        }
    }
}
