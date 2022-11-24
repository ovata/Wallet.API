using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data;
using Wallet.Data.Entities;

namespace Wallet.Service.Queries
{
    public class GetUserByEmail
    {
        public record Query : IRequest<User>
        {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly ApplicationContext _context;

            public Handler(ApplicationContext context)
            {
                _context = context;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Email, cancellationToken);
                return null;
            }
        }
    }
}
