using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using REST_API.Models;
using REST_API.Interfaces;
using REST_API.Utils;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private region                
        /// <summary>
        /// The repo
        /// </summary>
        private readonly IUserRepository _repo;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _repo.Get();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetAsync(string username)
        {
            var entity =  await _repo.Get(username) ?? new User();

            if (entity != null && entity.Id != null)
            {
                return entity;
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> Create(User entity)
        {
            User entityCreated = await _repo.Create(entity);

            if (entityCreated != null)
            {
                return entityCreated;
            }
            else
            {
                return StatusCode(400, new { result = Messages.MESSAGE_002 });
            }
        }


        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<bool>> Update(User entity)
        {
            if (await _repo.Update(entity) == true)
            {
                return StatusCode(200, new { result = Messages.MESSAGE_003 });
            }
            else
            {
                return StatusCode(400, new { result = Messages.MESSAGE_004 });
            }
        }

        /// <summary>
        /// Deletes the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        [HttpDelete("{username}")]
        public async Task<ActionResult<bool>> Delete(string username)
        {
            var ent = await _repo.Get(username);
            if (ent != null && ent.Id != null)
            {
                if (await _repo.Remove(username) == true)
                {
                    return StatusCode(200, new { result = Messages.MESSAGE_005 });
                }
                else
                {
                    return StatusCode(400, new { result = Messages.MESSAGE_006 });
                }
            }
            else
            {
                return StatusCode(400, new { result = Messages.MESSAGE_006 });
            }
        }
    }
}