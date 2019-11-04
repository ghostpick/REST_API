using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REST_API.Interfaces;
using REST_API.Models;
using REST_API.Utils;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        #region Private region        
        /// <summary>
        /// The user repo
        /// </summary>
        private readonly IConfigRepository _repo;
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigsController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public ConfigsController(IConfigRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Config>> GetProperties()
        {
            return await _repo.GetProperties();
        }

        /// <summary>
        /// Updates the product keys.
        /// </summary>
        /// <param name="modulus">The modulus.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Config>> UpdateProductKeys(string modulus, string exponent)
        {

            return await _repo.UpdateProductKeysAsync(modulus, exponent);
        }
    }
}