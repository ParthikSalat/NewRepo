using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class OrganizerController : Controller
    {
        string apiUrl = "https://localhost:7121/api/OrganizerTbs/";
        HttpClient client=new HttpClient();
        // GET: OrganizerController

        public async Task<ActionResult> AdminOrgIndex()
        {
            var o = await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}");
            return View(o);
        }
        public async Task<ActionResult> Index()
        {
            var a = await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}");
            return View(a);
        }
        // GET: OrganizerController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: OrganizerController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginPost(OrganizerTb model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);  // Return with validation errors
            }

            var data = await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}");
            var organizer = data.FirstOrDefault(o => o.OrganizerEmail == model.OrganizerEmail && o.OrganizerPassword == model.OrganizerPassword);
          
            if (organizer != null)
            {
                // Store session data
                HttpContext.Session.SetString("email", organizer.OrganizerEmail);
                HttpContext.Session.SetInt32("organizerid", organizer.OrganizerId);

                // Redirect to Index action in Event controller
                return RedirectToAction("Index", "Event");
            }
            else
            {
                ViewBag.Error = "Invalid email or password";  // Show error if login fails
            }

            return View();
        }



        // GET: OrganizerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var data = await client.GetFromJsonAsync<OrganizerTb>($"{apiUrl}{id}");
            return View(data);

        }

        // GET: OrganizerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrganizerTb data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await client.PostAsJsonAsync($"{apiUrl}", data);
                    String username = HttpContext.Session.GetString("name");
                    return RedirectToAction(nameof(Login));
                }
                else { return View(); }
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await client.GetFromJsonAsync<OrganizerTb>($"{apiUrl}{id}");
            String username = HttpContext.Session.GetString("name");
            return View(data);
        }

        // POST: OrganizerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OrganizerTb collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await client.PutAsJsonAsync($"{apiUrl}{id}",collection);
                    return RedirectToAction("AdminOrgIndex");
                }
                return View();
               
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await client.GetFromJsonAsync<OrganizerTb>($"{apiUrl}{id}");
            return View(data);
        }

       


        // POST: OrganizerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, OrganizerTb collection)
        {
            try
            {
                var data = await client.DeleteAsync($"{apiUrl}{id}");
                return RedirectToAction("AdminOrgIndex");
            }
            catch
            {
                return View();
            }
        }

      


    }
}
