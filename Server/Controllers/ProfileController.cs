using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly GameDBContext _gameDBContext;
        public ProfileController(IProfileService profileService, GameDBContext gameDBContext)
        {
            _profileService = profileService;
            _gameDBContext = gameDBContext;

            var user = new User()
            {
                Username = "Misha",
                PasswordHash = "SomePassword",
                Salt = "something there"
            };

            _gameDBContext.Add(user);

            _gameDBContext.SaveChanges();
        }

        [HttpGet("{id}")]
        public Profile Get([FromRoute] int id)
        {
            var profile = new Profile() { ID = id };

            _profileService.DoSomething();

            return profile;
        }

        [HttpPost]
        public Profile Post(Profile profile) 
        {
            Console.WriteLine("Profile has been added to the DB");
            return profile;
        }
    }
}
