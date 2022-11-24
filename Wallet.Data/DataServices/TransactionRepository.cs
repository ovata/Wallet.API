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
    public class TransactionRepository : GenericDataRepository<Transaction>
    {
        private readonly ApplicationContext _context;

        public TransactionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetByWalletIdAsync(long walletId, DateTime? start, DateTime? end)
        {
            var transactions = _context.Transactions.Where(c => c.WalletId == walletId);
            if (start.HasValue && end.HasValue)
                transactions = transactions.Where(c => c.TransactionDate.Date >= start.Value.Date
                && c.TransactionDate.Date <= end.Value.Date);
            if (start.HasValue && !end.HasValue)
                transactions = transactions.Where(c => c.TransactionDate.Date >= start.Value.Date);

            if (end.HasValue && !start.HasValue)
                transactions = transactions.Where(c => c.TransactionDate.Date <= end.Value.Date);

            return await transactions.ToListAsync();

        }
    }
}
