using helpDesk.Dtos;
using helpDesk.Exceptions;
using helpDesk.Services;
using Microsoft.AspNetCore.Mvc;

namespace helpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var response = new ResponseDto<List<UserDto>>
                {
                    Data = users
                };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _services.GetById(id);

                var response = new ResponseDto<UserDto>
                {
                    Data = user
                };

                return Ok(response);
            }

            catch (NotFoundException ex)
            {
                return NotFound(
                    new ResponseDto
                    {
                        Errors = [ex.Message]
                    }
                );
            }

            catch (Exception)
            {
                return StatusCode(500,
                    new ResponseDto
                    {
                        Errors = ["Erro interno no servidor."]
                    }
                );
            }

        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            try
            {
                var user = _services.Create(dto);

                var response = new ResponseDto<UserDto>
                {
                    Data = user
                };

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500,
                    new ResponseDto
                    {
                        Errors = ["Erro interno no servidor."]
                    }
                );
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDto dto)
        {
            try
            {
                var user = _services.Update(id, dto);

                var response = new ResponseDto<UserDto>
                {
                    Data = user
                };

                return Ok(response);
            }

            catch (NotFoundException ex)
            {
                return NotFound(
                    new ResponseDto
                    {
                        Errors = [ex.Message]
                    }
                );
            }

            catch (Exception)
            {
                return StatusCode(500,
                    new ResponseDto
                    {
                        Errors = ["Erro interno no servidor."]
                    }
                );
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _services.Delete(id);

                var response = new ResponseDto<UserDto>
                {
                    Data = user
                };

                return Ok(response);
            }

            catch (NotFoundException ex)
            {
                return NotFound(
                    new ResponseDto
                    {
                        Errors = [ex.Message]
                    }
                );
            }

            catch (Exception)
            {
                return StatusCode(500,
                    new ResponseDto
                    {
                        Errors = ["Erro interno no servidor."]
                    }
                );
            }


        }
    }
}