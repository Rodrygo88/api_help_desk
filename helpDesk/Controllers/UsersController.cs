using helpDesk.Dtos;
using helpDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace helpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _services;
        public UsersController(UsersServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _services.GetAll();

            return Ok(new ResponseDto<List<UserDto>>
            {
                Data = users
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _services.GetById(id);

            return Ok(new ResponseDto<UserDto>
            {
                Data = user
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(CreateUserDto dto)
        {
            var user = _services.Create(dto);

            return StatusCode(201, new ResponseDto<UserDto>
            {
                Data = user
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDto dto)
        {
            var user = _services.Update(id, dto);

            return Ok(new ResponseDto<UserDto>
            {
                Data = user
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _services.Delete(id);

            return Ok(new ResponseDto<UserDto>
            {
                Data = user
            });
        }
    }
}
