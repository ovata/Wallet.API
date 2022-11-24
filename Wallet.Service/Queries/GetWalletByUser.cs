using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wallet.Data;
using Wallet.Data.Entities;
using Wallet.Service.Services;

namespace Wallet.Service.Queries
{
    public static class GetWalletByUser
    {
        public record Query : IRequest<Response>
        {
            public string UserId { get; set; }
        }

        public record Response
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime RegistrationDate { get; set; }
            public bool IsWalletCreated { get; set; }
            public WalletType WalletType { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ApplicationContext _context;
            private readonly UserManager<User> _userManager;

            public Handler(ApplicationContext context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.)
            }
        }
    }
}
