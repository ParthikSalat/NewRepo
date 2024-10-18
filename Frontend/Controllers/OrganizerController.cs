using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class OrganizerController : Controller
    {
        string apiUrl = "https://localhost:7121/api/OrganizerTbs";
        HttpClient client=new HttpClient();
        // GET: OrganizerController

        public async Task<ActionResult> Index()
        {
            var a = await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}");
            return View(a);
        }


        public async Task<ActionResult> login(OrganizerTb model)
        {
            var data = await client.GetFromJsonAsync<List<OrganizerTb>>($"{apiUrl}");
            var a = data.FirstOrDefault(o => o.OrganizerEmail == model.OrganizerEmail && o.OrganizerPassword == model.OrganizerPassword);
            if (a != null)
            {
                HttpContext.Session.SetString("email",a.OrganizerEmail);
                HttpContext.Session.SetInt32("organizerid", a.OrganizerId);

                return RedirectToAction("Index","Event");

            }
            else
            {
                ViewBag.Error = "Invalid email or password";
            }
            return View();
        }


        // GET: OrganizerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    return RedirectToAction(nameof(login));
                }
                else { return View(); }
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrganizerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: OrganizerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

       


        // POST: OrganizerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
