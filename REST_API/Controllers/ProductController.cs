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
    public class ProductController : ControllerBase
    {
        #region Private region        
        /// <summary>
        /// The user repo
        /// </summary>
        private readonly IProductRepository _repo;
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _repo.Get();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="productCode">The product code.</param>
        /// <returns></returns>
        [HttpGet("{productCode}")]
        public async Task<ActionResult<Product>> GetAsync(string productCode)
        {
            var entity = await _repo.Get(productCode) ?? new Product();

            if (entity != null && entity.Id != null)
            {
                return entity;

            }
            return NotFound();
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product entity)
        {
            Product entityCreated = await _repo.Create(entity);

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
        public async Task<ActionResult<bool>> Update(Product entity)
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
        /// Deletes the specified product code.
        /// </summary>
        /// <param name="productCode">The product code.</param>
        /// <returns></returns>
        [HttpDelete("{productCode}")]
        public async Task<ActionResult<bool>> DeleteAsync(string productCode)
        {
            var ent = await _repo.Get(productCode);
            if (ent != null && ent.Id != null)
            {
                if(await _repo.Remove(productCode) == true)
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