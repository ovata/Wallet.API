using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data.Entities;
using Wallet.Service.Services;

namespace Wallet.Service.Commands.WalletCommands
{
    public class UpdateWallet
    {
        public record Command : IRequest
        {
            public UserWallet UserWallet { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly WalletRepository _walletRepository;

            public Handler(WalletRepository walletRepository)
            {
                _walletRepository = walletRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _walletRepository.Update(request.UserWallet);
                return Unit.Value;
            }
        }
    }
}
