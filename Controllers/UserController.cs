using System.Threading.Tasks;
using ContactManager.Entities;
using ContactManager.Repositories;
using ContactManager.Validation;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<User> _userValidator;

        public UserController(IUserRepository repository, IValidator<User> userValidator)
        {
            _repository = repository;
            _userValidator = userValidator;
        }

        [HttpPost("")]
        [HttpPost("/")]
        [HttpPut("")]
        [HttpPut("/")]
        public async Task<IActionResult> Upsert([FromBody]User user)
        {
            if (!_userValidator.Validate(user))
            {
                return BadRequest(_userValidator.Errors);
            }

            var id = await _repository.UpsertUser(user);
            if (!user.Id.HasValue) return Created($"{id}", id);

            return NoContent();
        }
    }
}