using MongoDB.Driver;
using REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API.Data.Repositories
{
    public class InitialData
    {
        public InitialData()
        {

        }

        #region Private Methods

        /// <summary>
        /// Initials the vouchers.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<Voucher> InitialVouchers(ApplicationContext context, string username)
        {
            List<Voucher> vouchers = new List<Voucher>();
            Voucher voucher = new Voucher();
            voucher.VoucherId = Guid.NewGuid().ToString().ToUpper().Substring(4, 24);
            voucher.VoucherName = "20 % Discount in Products";
            voucher.Discount = 20;
            voucher.Username = username;
            voucher.State = "In Progress";
            voucher.StateProgress = 98;
            vouchers.Add(voucher);

            Voucher voucher1 = new Voucher();
            voucher1.VoucherId = Guid.NewGuid().ToString().ToUpper().Substring(4, 24);
            voucher1.VoucherName = "50 % Discount in Products";
            voucher1.Discount = 50;
            voucher1.Username = username;
            voucher1.State = "In Progress";
            voucher1.StateProgress = 96;
            vouchers.Add(voucher1);

            await context.Voucher.InsertManyAsync(vouchers);
            return voucher;
        }
        #endregion
    }
}
