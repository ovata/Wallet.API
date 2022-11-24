using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data;
using Wallet.Data.Entities;
using Wallet.Service.DTOs;
using Wallet.Service.Queries;

namespace Wallet.Service.Commands.UserCommands
{
    public static class CreateUser
    {
        public record Command : IRequest<Response>
        {
            [DataType(DataType.EmailAddress), Required(ErrorMessage = "Email is Required")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Password is required")]
            [StringLength(255, ErrorMessage = "Must not be less than 8 characters", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Passwords does not meet required complexity")]
            public string Password { get; set; }
            [Compare(("Password"))]
            public string ConfirmPassword { get; set; }
        }

        public record Response
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime RegistrationDate { get; set; }
            public bool IsWalletCreated { get; set; }
            public WalletType WalletType { get; set; }
            
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ApplicationContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public Handler(ApplicationContext context, UserManager<User> userManager, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if(_context.Users.Any(x => x.Email == request.Email))
                {
                    return new Response();
                }
                var user = _mapper.Map<User>(request);

                return new Response
                {
                    FirstName = user.FirstName,
                    IsWalletCreated = user.IsWalletCreated,
                    LastName = user.LastName,
                    RegistrationDate = DateTime.Now,
                    WalletType = WalletType.USD,
                };
            }
        }
    }
}
