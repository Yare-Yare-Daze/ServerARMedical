using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Services;
using SharedLibrary;
using SharedLibrary.Requests;

namespace Server.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        public Profile Post(CreateProfileRequest request) 
        {
            var userId = int.Parse(User.FindFirst("id").Value);

            var user = _gameDBContext.Users.Include(u => u.ProfileList).First(u => u.Id == userId);

            var profile = new Profile()
            {
                Name = request.Name,
                User = user
            };

            _gameDBContext.Add(profile);
            _gameDBContext.SaveChanges();

            return profile;
        }
    }
}
