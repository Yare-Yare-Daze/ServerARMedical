using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        }

        [HttpGet("{id}")]
        public Profile Get([FromRoute] int id)
        {
            var profile = new Profile() { ID = id };

            _profileService.DoSomething();

            return profile;
        }

        [HttpPost("{id}")]
        public IActionResult Edit([FromRoute] int id,[FromBody] CreateProfileRequest request)
        {
            var profileIdsAvailable = JsonConvert.DeserializeObject<List<int>>(User.FindFirst("profiles").Value);

            if (!profileIdsAvailable.Contains(id)) return Unauthorized();

            var profile = _gameDBContext.Profiles.First(p => p.ID == id);

            profile.Name = request.Name;

            _gameDBContext.SaveChanges();

            return Ok();
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

            profile.User = null;

            return profile;
        }
    }
}
