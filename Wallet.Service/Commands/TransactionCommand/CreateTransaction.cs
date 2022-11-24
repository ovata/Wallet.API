using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Wallet.Data.Entities;

namespace Wallet.Service.Commands.TransactionCommand
{
    public class CreateTransaction
    {
        public record Command : IRequest
        {
            public Data.Entities.Transaction Transaction { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {

            public Handler()
            {
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}
