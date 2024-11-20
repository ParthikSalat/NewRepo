using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AdminController : Controller
    {
        string apiUrl = "https://localhost:7121/api/AdminTbs/";
        HttpClient client = new HttpClient();
        // GET: AdminController

        public async Task<ActionResult> Dashboard()
        {
            try
            {
                // Fetch data from API
                var users = await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}UserTbs/");
                var organizers = await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}OrganizerTbs/");
                var events = await client.GetFromJsonAsync<List<EventTb>>($"{apiUrl}EventTbs/");

                // Ensure data is not null and fetch counts
                var totalUsers = users?.Count ?? 0;
                var totalOrganizers = organizers?.Count ?? 0;
                var totalEvents = events?.Count ?? 0;

                // Log for debugging
                Console.WriteLine($"Total Users: {totalUsers}, Total Organizers: {totalOrganizers}, Total Events: {totalEvents}");

                // Pass data to view
                ViewBag.TotalUsers = totalUsers;
                ViewBag.TotalOrganizers = totalOrganizers;
                ViewBag.TotalEvents = totalEvents;

                // Example chart data
                ViewBag.MonthlyEarnings = new[] { 100, 200, 150, 300, 250, 400, 350 };
                ViewBag.VisitSeparation = new { Mobile = 36.5, Tablet = 30.8, Desktop = 7.7, Other = 25.0 };
            }
            catch (Exception ex)
            {
                // Log the exception and show an error message
                Console.WriteLine($"Error fetching data: {ex.Message}");
                ViewBag.Error = "Error fetching data: " + ex.Message;
            }

            return View();
        }

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
                
                HttpContext.Session.SetString("email", admin.AdminEmail);
                HttpContext.Session.SetInt32("adminid", admin.AdminId);

                
                return RedirectToAction("AdminIndex", "Admin");
            }
            else
            {
                ViewBag.Error = "Invalid email or password";  
            }

            return View();
        }

        public async Task< ActionResult> Index()
        {
            var model = await client.GetFromJsonAsync<List<AdminTb>>($"{apiUrl}");
            return View(model);
        }

        public async Task<ActionResult> AdminIndex()
        {
            var model = await client.GetFromJsonAsync<List<AdminTb>>($"{apiUrl}");
            return View(model);
        }

        // GET: AdminController/Details/5
        public async Task< ActionResult> Details(int id)
        {
            var model = await client.GetFromJsonAsync<AdminTb>($"{apiUrl}{id}");
            return View(model);
        }

        // GET: AdminController/Create
        public async Task< ActionResult> Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public async Task< ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public async Task< ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
