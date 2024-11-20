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
                // Fetch total counts of Users, Organizers, and Events
                var totalUsers = (await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}UserTbs/")).Count;
                var totalOrganizers = (await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}OrganizerTbs/")).Count;
                var totalEvents = (await client.GetFromJsonAsync<List<EventTb>>($"{apiUrl}EventTbs/")).Count;

                // Pass the data to the ViewBag for display
                ViewBag.TotalUsers = totalUsers;
                ViewBag.TotalOrganizers = totalOrganizers;
                ViewBag.TotalEvents = totalEvents;

                // Example data for the Monthly Earnings chart (replace with dynamic data if available)
                ViewBag.MonthlyEarnings = new[] { 500, 700, 800, 650, 900, 1100, 950 };

                // Example data for Visit Separation chart (replace with dynamic data if available)
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
                // Pass error details to the view in case of an exception
                ViewBag.Error = "Error fetching data: " + ex.Message;
            }

            // Return the Dashboard view
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
