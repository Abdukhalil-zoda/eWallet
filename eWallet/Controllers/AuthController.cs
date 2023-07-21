using AutoMapper;
using eWallet.Data.DTO.Auth;
using eWallet.Data.Models;
using eWallet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService service,
                              IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public IAuthService Service { get; }
        public IMapper Mapper { get; }

        [HttpPost("login")]
        public IActionResult Auth(LoginDTO dto) =>
            Ok(Service.Login(dto.UserName, dto.Password));
        [HttpPost("register")]
        public async Task<IActionResult> NewUser(NewUserDTO dto)
        {
            await Service.AddUser(Mapper.Map<User>(dto));
            return Ok();
        }
    }
}
