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
        //Funtionality Created and tested
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Funtionality Created and tested
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

        //Funtionality Created and tested
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Funtionality Created and tested
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

        //Funtionality Created and tested
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public IActionResult AddToCart(int userId, int songId)
        {
            // Call ADO.net logic to add the song to the cart
            userDataAccess.AddToCart(userId, songId);

            // Redirect back to the Music/Index view or another appropriate page
            return RedirectToAction("Index", "Music");
        }
    }
}
