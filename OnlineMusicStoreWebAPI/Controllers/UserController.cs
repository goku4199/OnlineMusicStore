using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMusicStoreWebAPI.Models;
using OnlineMusicStoreWebAPI.Repository;

namespace OnlineMusicStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDataAccess _userDataAccess;

        public UserController(UserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        //Working
        // POST: api/User/Register
        [HttpPost("Register")]
        public ActionResult<User> Register(User user)
        {
            _userDataAccess.RegisterUser(user);
            return Ok(user);
        }

        //Working
        // POST: api/User/Login
        [HttpPost("Login")]
        public ActionResult<User> Login(User user)
        {
            var authenticatedUser = _userDataAccess.ValidateUser(user);

            if (authenticatedUser != null)
            {
                return Ok(authenticatedUser);
            }

            return Unauthorized();
        }

        //Working
        // POST: api/User/AddToCart
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int userId, int songId)
        {
            _userDataAccess.AddToCart(userId, songId);
            return NoContent();
        }

        //Working
        // GET: api/User/Cart
        [HttpGet("Cart")]
        public ActionResult<Cart> Cart(int userId)
        {
            var cart = _userDataAccess.GetCartSongs(userId);
            if (cart != null)
            {
                return Ok(cart);
            }

            return NotFound();
        }
    }
}
