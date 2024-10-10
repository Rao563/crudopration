using crudopration.DetailRto;
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


        [HttpGet]
        [Route("getbyrto")]
        public ActionResult<UserRTO> GetbyRto(int id)
        {
            var user = _userContext.users
               .Where(x=>x.ID==id)
                .Select(x => new UserRTO
            {
                Name = x.Name,
                Id =x.ID,
                ContactNo = x.ContactNo
            }).ToList();
            return Ok(user);
        }


    }
}
