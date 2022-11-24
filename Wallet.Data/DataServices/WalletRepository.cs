using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data;
using Wallet.Data.Entities;
using Wallet.Data.Services;

namespace Wallet.Service.Services
{
    public class WalletRepository : GenericDataRepository<UserWallet>
    {
        private readonly ApplicationContext _context;

        public WalletRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserWallet> GetWalletByUserId(string userId)
        {
            return await _context.Wallets.Where(c => c.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
