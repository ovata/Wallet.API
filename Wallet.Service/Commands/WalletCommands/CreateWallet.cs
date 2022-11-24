using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data.Entities;

namespace Wallet.Service.Commands.WalletCommands
{
    public class CreateWallet
    {
        public record Command : IRequest<int>
        {
            public UserWallet Wallet { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {

            public Handler()
            {
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return 1;
            }
            
        }
    }
}
