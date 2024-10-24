using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Frontend.Controllers
{
    public class EventController : Controller
    {
        // GET: EventController
        string apiUrl = "https://localhost:7121/api/EventTbs/";
        HttpClient client=new HttpClient();
        public async Task<ActionResult> eventhome(string searchTerm)
        {
            // Fetch the list of events from the API
            var events = await client.GetFromJsonAsync<List<EventTb>>($"{apiUrl}");

            // If searchTerm is not null or empty, filter the list
            if (!string.IsNullOrEmpty(searchTerm))
            {
                events = events
                    .Where(e => e.EventName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                e.EventArtist.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Pass the search term to the view
            ViewBag.SearchTerm = searchTerm;

            return View(events);
        }

        public async Task<ActionResult> Index()
        {
            var organizerid = HttpContext.Session.GetInt32("organizerid");
            if(organizerid==null)
            {
                return RedirectToAction("login", "Organizer");

            }
            var a = await client.GetFromJsonAsync<List<EventTb>>($"{apiUrl}");
            var data=a.Where(o=>o.OrganizerId==organizerid).ToList();

            return View(data);
        }

        // GET: EventController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var data = await client.GetFromJsonAsync<EventTb>($"{apiUrl}{id}");
            return View(data);

        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventTb data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await client.PostAsJsonAsync($"{apiUrl}", data);
                    Console.WriteLine(data);
                    return RedirectToAction(nameof(Index));

                }
                else { return View(); }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await client.GetFromJsonAsync<EventTb>($"{apiUrl}{id}");
            return View(data);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventTb collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await client.PutAsJsonAsync($"{apiUrl}{id}", collection);
                    return RedirectToAction(nameof(Index));
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await client.GetFromJsonAsync<EventTb>($"{apiUrl}{id}");
            return View(data);
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public async Task<ActionResult> Delete(int id, EventTb collection)
        {
            try
            {
                var data = await client.DeleteAsync($"{apiUrl}{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Logout()
        {
            HttpContext.Session.Clear(); // Clear the session

            // Redirect to login page or any other page after logout
            return RedirectToAction("eventhome", "event");
        }

    }
}
