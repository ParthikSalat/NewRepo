using EventAPI.Models;
using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class UserController : Controller
    {

        // GET: UserController
        string apiUrl = "https://localhost:7121/api/UserTbs";
        HttpClient client = new HttpClient();
        public async Task<ActionResult> Index()
        {

            var userName = HttpContext.Session.GetString("name");
            if (userName != null)
            {
                try
                {

                    var users = await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}");
                    return View(users);
                }
                catch (Exception ex)
                {

                    return View("Error");
                }
            }


            return RedirectToAction("Login");
        }


        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserTb login_data)
        {

            var data = await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}");


            var user = data.FirstOrDefault(option =>
                option.UserName == login_data.UserName &&
                option.UserPassword == login_data.UserPassword);

            if (user != null || !data.Any())
            {
                HttpContext.Session.SetString("name", user.UserName);

                return RedirectToAction("eventhome","event");
            }
            else
            {
                ViewBag.Error = "Invalid Username or Password";
            }

            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserTb data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await client.PostAsJsonAsync($"{apiUrl}", data);
                    return RedirectToAction(nameof(Login));
                }
                else { return View(); }
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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

        public async Task<ActionResult> logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("eventhome", "event");

        }
    }
}
