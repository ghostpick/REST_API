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
    public class VoucherController : ControllerBase
    {
        #region Private region        
        /// <summary>
        /// The user repo
        /// </summary>
        private readonly IVoucherRepository _repo;
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="VoucherController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public VoucherController(IVoucherRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Voucher>> Get()
        {
            return await _repo.Get();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="voucherId">The voucher identifier.</param>
        /// <returns></returns>
        [HttpGet("{voucherId}")]
        public async Task<ActionResult<Voucher>> GetAsync(string voucherId)
        {
            var entity = await _repo.Get(voucherId) ?? new Voucher();

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
        public async Task<ActionResult<Voucher>> Create(Voucher entity)
        {
            Voucher entityCreated = await _repo.Create(entity);

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
        public async Task<ActionResult<bool>> Update(Voucher entity)
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
        /// <param name="VoucherId">The product code.</param>
        /// <returns></returns>
        [HttpDelete("{voucherId}")]
        public async Task<ActionResult<bool>> DeleteAsync(string voucherId)
        {
            var ent = await _repo.Get(voucherId);
            if (ent != null && ent.Id != null)
            {
                if(await _repo.Remove(voucherId) == true)
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

        /// <summary>
        /// Gets the active vouchers.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        [HttpGet("GetActiveVouchers/")]
        public async Task<IEnumerable<Voucher>> GetActiveVouchers(string username)
        {
            return await _repo.GetUsersVoucher(username, "Active");
        }

        /// <summary>
        /// Gets the in progress vouchers.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        [HttpGet("GetInProgressVouchers/")]
        public async Task<IEnumerable<Voucher>> GetInProgressVouchers(string username)
        {
            return await _repo.GetUsersVoucher(username, "In Progress");
        }
    }
}