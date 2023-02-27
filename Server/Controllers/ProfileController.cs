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
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
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
