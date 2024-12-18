﻿using EventAPI.Models;
using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class UserController : Controller
    {

        // GET: UserController
        string apiUrl = "https://localhost:7121/api/UserTbs/";
        HttpClient client=new HttpClient();
        public async Task<ActionResult> Index()
        {
            // Check if session is set
            var userName = HttpContext.Session.GetString("name");
            if (userName != null)
            {
                try
                {
                    // Fetch data from the API
                    var users = await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}");
                    return View(users); // Pass the fetched data to the view
                }
                catch (Exception ex)
                {
                    // Log the exception and return an error view/message if needed
                    // You can use a logging library or display an error message
                    return View("Error");
                }
            }

            // Redirect to login or return a message
            return RedirectToAction("Login");
        }

        public async Task<ActionResult> AdminUserIndex()
        {
            var data = await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}");
            return View(data);
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
            // Fetch the list of users from the API
            var data = await client.GetFromJsonAsync<List<UserTb>>($"{apiUrl}");

            // Find the user that matches the provided username and password
            var user = data.FirstOrDefault(option =>
                option.UserName == login_data.UserName &&
                option.UserPassword == login_data.UserPassword); // Corrected this line

            if (user != null) // Only proceed if user is found
            {
                HttpContext.Session.SetString("name", user.UserName);
                return RedirectToAction("eventhome", "Event"); // Redirect to the Index action on successful login
            }
            else
            {
                ViewBag.Error = "Invalid Username or Password"; // Set error message
            }

            return View(); // Return the view, showing the error
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
                if(ModelState.IsValid)
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
        public async Task<ActionResult> Edit(int id)
        {
            var data = await client.GetFromJsonAsync<UserTb>($"{apiUrl}{id}");
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserTb collection)
        {
            try
            {
                await client.PutAsJsonAsync($"{apiUrl}{id}",collection);
                return RedirectToAction("AdminUserIndex");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await client.GetFromJsonAsync<UserTb>($"{apiUrl}{id}");
            return View(data);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{apiUrl}{id}");
                return RedirectToAction("AdminUserIndex");
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
