using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data.Entities;
using static Wallet.Service.Commands.UserCommands.UpdateUser;

namespace Wallet.Service.Commands.UserCommands
{
    public class UpdateUser
    { 

        public record Command : IRequest<Unit>
        {
            public User User { get; set; }
        }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {

        public Handler()
        {
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {

        }
    }
}
