using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AdminController : Controller
    {
        private readonly string apiUrl = "https://localhost:7121/api/AdminTbs/";
        private readonly HttpClient client = new HttpClient();
        public async Task<ActionResult> TotalData()
        {
            try
            {
                // Fetch total counts of Users, Organizers, and Events from the API
                var totalsResponse = await client.GetFromJsonAsync<Dictionary<string, int>>($"{apiUrl}totals");
                if (totalsResponse != null)
                {
                    ViewBag.TotalUsers = totalsResponse.GetValueOrDefault("totalUsers", 0);
                    ViewBag.TotalOrganizers = totalsResponse.GetValueOrDefault("totalOrganizers", 0);
                    ViewBag.TotalEvents = totalsResponse.GetValueOrDefault("totalEvents", 0);
                }

                // Example data for Monthly Earnings chart
                ViewBag.MonthlyEarnings = new[] { 500, 700, 800, 650, 900, 1100, 950 };

                // Example data for Visit Separation chart
                ViewBag.VisitSeparation = new Dictionary<string, double>
        {
            { "Mobile", 36.5 },
            { "Tablet", 30.8 },
            { "Desktop", 7.7 },
            { "Other", 25.0 }
        };
            }
            catch (Exception ex)
            {
                // Log and display errors
                ViewBag.Error = $"Error fetching data: {ex.Message}";
            }

            // Return the TotalData view
            return View();
        }


        // GET: AdminController/Dashboard
        public async Task<ActionResult> Dashboard()
        {
            try
            {
                // Fetch total counts of Users, Organizers, and Events from the API
                var totalsResponse = await client.GetFromJsonAsync<dynamic>($"{apiUrl}totals");
                if (totalsResponse != null)
                {
                    ViewBag.TotalUsers = totalsResponse.TotalUsers;
                    ViewBag.TotalOrganizers = totalsResponse.TotalOrganizers;
                    ViewBag.TotalEvents = totalsResponse.TotalEvents;
                }

                // Example data for Monthly Earnings chart (replace with real data if needed)
                ViewBag.MonthlyEarnings = new[] { 500, 700, 800, 650, 900, 1100, 950 };

                // Example data for Visit Separation chart (replace with real data if needed)
                ViewBag.VisitSeparation = new Dictionary<string, double>
                {
                    { "Mobile", 36.5 },
                    { "Tablet", 30.8 },
                    { "Desktop", 7.7 },
                    { "Other", 25.0 }
                };
            }
            catch (Exception ex)
            {
                // Log and display errors
                ViewBag.Error = $"Error fetching data: {ex.Message}";
            }

            // Return the Dashboard view
            return View();
        }

        // Admin Login
        public async Task<ActionResult> AdminLogin(AdminTb model)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminLogin", model);
            }

            var data = await client.GetFromJsonAsync<List<AdminTb>>($"{apiUrl}");
            var admin = data.FirstOrDefault(o => o.AdminEmail == model.AdminEmail && o.AdminPassword == model.AdminPassword);

            if (admin != null)
            {
                // Save admin details in session
                HttpContext.Session.SetString("email", admin.AdminEmail);
                HttpContext.Session.SetInt32("adminid", admin.AdminId);

                return RedirectToAction("TotalData", "Admin");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            var model = await client.GetFromJsonAsync<List<AdminTb>>($"{apiUrl}");
            return View(model);
        }

        // Admin Index for listing admins
        public async Task<ActionResult> AdminIndex()
        {
            var model = await client.GetFromJsonAsync<List<AdminTb>>($"{apiUrl}");
            return View(model);
        }

        // GET: AdminController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await client.GetFromJsonAsync<AdminTb>($"{apiUrl}{id}");
            return View(model);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminTb model)
        {
            try
            {
                await client.PostAsJsonAsync($"{apiUrl}", model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await client.GetFromJsonAsync<AdminTb>($"{apiUrl}{id}");
            return View(model);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AdminTb model)
        {
            try
            {
                await client.PutAsJsonAsync($"{apiUrl}{id}", model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: AdminController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await client.GetFromJsonAsync<AdminTb>($"{apiUrl}{id}");
            return View(model);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await client.DeleteAsync($"{apiUrl}{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id });
            }
        }

        public async Task<ActionResult> AdminLogout()
        {
            HttpContext.Session.Clear(); // Clear the session

            // Redirect to login page or any other page after logout
            return RedirectToAction("adminlogin", "admin");
        }

    }
}
