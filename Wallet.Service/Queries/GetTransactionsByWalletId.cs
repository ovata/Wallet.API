using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data.Entities;
using Wallet.Service.Services;

namespace Wallet.Service.Queries
{
    public class GetTransactionsByWalletId
    {
        public record Query : IRequest<IEnumerable<Transaction>>
        {
            public long WalletId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Transaction>>
        {
            private readonly TransactionRepository _transactionRepository;

            public Handler(TransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<IEnumerable<Transaction>> Handle(Query request, CancellationToken cancellationToken) => await _transactionRepository.GetByWalletIdAsync(request.WalletId, request.StartDate, request.EndDate);
        }
    }
}
