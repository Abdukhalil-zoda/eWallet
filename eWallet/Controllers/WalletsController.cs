using AutoMapper;
using eWallet.Data.DTO.Wallet;
using eWallet.Repositories;
using eWallet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        public WalletsController(IWalletService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public IWalletService Service { get; }
        public IMapper Mapper { get; }

        [Authorize, HttpPost("add")]
        public async Task<IActionResult> AddWallet([FromBody] string name)
        {
            var userId = Guid.Parse(User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value);
            
            return Ok(await Service.CreateWallet(userId, name));
        }

        [Authorize, HttpGet("balance/{id}")]
        public IActionResult GetBalance(Guid id) =>
            Ok(Service.Balance(id));

        [Authorize, HttpGet("checkAccount")]
        public IActionResult CheckAcc(Guid walletId) =>
            Ok(Mapper.Map<WalletAccountDTO>(Service.CheckAcc(walletId)));
    }
}