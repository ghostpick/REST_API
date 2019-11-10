using REST_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_API.Interfaces
{
    public interface IVoucherRepository : IBaseEntity<Voucher>
    {
        /// <summary>
        /// Gets the users voucher.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        Task<IEnumerable<Voucher>> GetUsersVoucher(string username, string state);
    }
}
