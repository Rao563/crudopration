using crudopration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crudopration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;

        public UsersController(UserContext userContext)
        {
            _userContext = userContext;
        }

        // Get the list of all users
        [HttpGet]
        [Route("getuserslist")]
        public ActionResult<List<Users>> GetUsers()
        {
            return _userContext.users.ToList();
        }

        // Get a single user by id
        [HttpGet]
        [Route("getuser/{id}")]
        public ActionResult<Users> GetUser(int id)
        {
            var user = _userContext.users.FirstOrDefault(x => x.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // Add a new user
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<string>> AddUser(Users user)
        {
            _userContext.users.Add(user);
            await _userContext.SaveChangesAsync(); // Save changes
            return "User added successfully";
        }

        // Update an existing user
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<string>> UpdateUser(Users user)
        {
            _userContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _userContext.SaveChangesAsync(); // Save changes
            return "User updated successfully";
        }

        // Delete a user
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var user = await _userContext.users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _userContext.Remove(user);
            await _userContext.SaveChangesAsync(); // Save changes
            return "User deleted successfully";
        }
    }
}
