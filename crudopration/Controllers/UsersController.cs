using crudopration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crudopration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext userContext;

        public UsersController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [HttpGet]
        [Route("getuserslist")]
        public List<Users> GetUsers()
        {
            return userContext.users.ToList();
        }

        //single user get API
        [HttpGet]
        [Route("getusers")]
        public Users GetUsers(int id)
        {
            return userContext.users.Where(x=> x.ID == id).FirstOrDefault();

        }

        [HttpPost]
        [Route("AddUser")]
        public string AddUser(Users users)
        {
            string rersponse = string.Empty;
            userContext.Add(users);
            userContext.AddAsync(users);
            return "User added";
        }

        [HttpPut]
        [Route("UpdateUser")]
        public string UpdateUser(Users user)
        {
            userContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            userContext.SaveChanges();
            return "User Updatd";
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public string DeleteUser(Users user)
        {
            userContext.Remove(user);
            userContext.SaveChanges();
            return "User deleted";
        }
    }
}
