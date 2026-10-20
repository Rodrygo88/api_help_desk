using helpDesk.Data;
using helpDesk.Dtos;
using helpDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace helpDesk.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;
        public AccountController(TokenService tokenService, AppDbContext appDbContext)
        {
            _tokenService = tokenService;
            _context = appDbContext;
        }

        [HttpPost("api/accounts/login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto loginDto)
        {
            var user =  _context.Users.AsNoTracking()
                .FirstOrDefault(x => x.Email == loginDto.Email);

            if (user == null)
            {
                return StatusCode(401, "Email inválido");
            }

            bool valido = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!valido)
            {
                return StatusCode(401, "Senha inválida");
            }

            try
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(token);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}
