using Microsoft.AspNetCore.Mvc;
using OnlineMusicStore.Models;
using OnlineMusicStore.Repository;

namespace OnlineMusicStore.Controllers
{
    public class UserController : Controller
    {

        private readonly UserDataAccess userDataAccess;

        public UserController(UserDataAccess userDataAccess)
        {
            this.userDataAccess = userDataAccess;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Call ADO.net logic to add user to the database
                userDataAccess.RegisterUser(user);
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            // Call ADO.net logic to validate user credentials
            var authenticatedUser = userDataAccess.ValidateUser(user);

            if (authenticatedUser != null)
            {
                // Successful login, set user information in session
                HttpContext.Session.SetInt32("UserId", authenticatedUser.UserId);
                HttpContext.Session.SetString("Username", authenticatedUser.Username);

                // Redirect to home or another page
                return RedirectToAction("Index", "Music");
            }

            // Failed login, show error message
            ModelState.AddModelError("", "Invalid username or password");
            return View(user);
        }
    }
}
