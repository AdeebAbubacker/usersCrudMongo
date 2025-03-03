

using Microsoft.AspNetCore.Mvc;
using UsersCrud.Models;
using UsersCrud.Services;


namespace UsersCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userService;

        public UsersController(UserServices userService)
        {
            _userService = userService;
        }

        // ✅ GET ALL USERS: api/users
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }

        // ✅ GET A SINGLE USER BY ID: api/users/{id}
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // ✅ CREATE A NEW USER: api/users
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        // ✅ UPDATE A USER: api/users/{id}
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
                return NotFound();

            _userService.Update(id, userIn);
            return NoContent();
        }

        // ✅ DELETE A USER: api/users/{id}
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
                return NotFound();

            _userService.Remove(id);
            return NoContent();
        }
    }
}
