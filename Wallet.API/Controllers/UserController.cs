using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallet.Data.Entities;
using Wallet.Service.Commands.UserCommands;
using Wallet.Service.Commands.WalletCommands;
using Wallet.Service.DTOs;
using Wallet.Service.Queries;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser.Command request)
        {
            var result = await _mediator.Send(new CreateUser.Command());
            return Ok(result);
        }

        private async Task<int> CreateWallet(User user)
        {
            var wallet = new UserWallet
            {
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Type = user.WalletType,
                Balance = 0
            };
            var result = await _mediator.Send(new CreateWallet.Command());
            if (result == 1)
            {
                user.IsWalletCreated = true;
                await _mediator.Send(new UpdateUser.Command());
            }
            return result;
        }
    }
}
