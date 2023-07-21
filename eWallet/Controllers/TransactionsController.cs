using AutoMapper;
using eWallet.Data.DTO.Transaction;
using eWallet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        public TransactionsController(ITransactionService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public ITransactionService Service { get; }
        public IMapper Mapper { get; }

        [Authorize, HttpPost("new")]
        public async Task<IActionResult> New(NewTransactionDTO dto)=>
            Ok(await Service.NewTransaction(dto.WalletId, dto.Amount));

        [Authorize, HttpGet("monthly")]
        public IActionResult Get(Guid walletId) 
        {
            var transactions = Service.LastMonthTransaction(walletId);
            return Ok(Mapper.Map<List<TransactionDTO>>(transactions));
        }
        
    }
}
