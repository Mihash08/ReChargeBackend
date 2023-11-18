using BackendReCharge.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        List<User> users = new List<User>()
            {
                new User()
                {
                    BirthDate = new DateTime(1998, 10, 8),
                    Email = "mihsasandomirskiy@gmail.com",
                    Id = 1,
                    Name = "Евгений",
                    PhoneNumber = "+79251851096",
                    Surname = "Онегин"
                }
            };
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return users;
        }
        [HttpGet(Name = "GetUser")]
        public User GetUser(int id)
        {
            return users.Where(x => x.Id == id).First();
        }
        [HttpGet(Name = "GetUserByNumber")]
        public User GetUserByNumber(string number)
        {
            return users.Where(x => x.PhoneNumber == number).First();
        }
    }
}