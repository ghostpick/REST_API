using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using REST_API.Models;
using REST_API.Interfaces;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private region        
        /// <summary>
        /// The user repo
        /// </summary>
        private readonly IUserRepository _userRepo;
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userRepo">The user repo.</param>
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userRepo.Get();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetAsync(string username)
        {
            var user =  await _userRepo.Get(username) ?? new User();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        [HttpPost]
        public void Create(User user)
        {
           _userRepo.Create(user);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User user)
        {
            var book = _userRepo.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _userRepo.Update(id, user);

            return NoContent();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userRepo.Remove(id);
            return NoContent();
        }
    }
}