using Microsoft.AspNetCore.Mvc;
using OnlineMusicStore.Models;
using OnlineMusicStore.Repository;
using System.Text.Json;
using System.Text;

namespace OnlineMusicStore.Controllers
{
    //Working
    public class UserController : Controller
    {
        private HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
        }

        //Working
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Working
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Working
        // POST: User/Register
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("https://localhost:7006/api/User/Register", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
            }

            return View(user);
        }

        //Working
        // POST: User/Login
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            user.email = "example@example.com"; // Add email value here

            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("https://localhost:7006/api/User/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var authenticatedUser = JsonSerializer.Deserialize<User>(data);

                
                HttpContext.Session?.SetInt32("UserId", authenticatedUser?.userId ?? 0);
                HttpContext.Session?.SetString("Username", authenticatedUser?.username ?? "");

                return RedirectToAction("Index", "Music");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(user);
        }

        // POST: User/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session?.Clear();
            return RedirectToAction("Login", "User");
        }

        //Working
        // POST: User/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int userId, int songId)
        {
            HttpResponseMessage response = await _client.PostAsync($"https://localhost:7006/api/user/addtocart?userId={userId}&songId={songId}", null);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Music");
            }

            return View();
        }

        //Working
        // GET: User/Cart
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var userId = HttpContext.Session?.GetInt32("UserId");

            if (userId.HasValue)
            {
                HttpResponseMessage response = await _client.GetAsync($"https://localhost:7006/api/user/cart?userId={userId.Value}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var cart = JsonSerializer.Deserialize<Cart>(data);
                    return View(cart);
                }
            }

            return RedirectToAction("Login", "User");
        }
    }
}
