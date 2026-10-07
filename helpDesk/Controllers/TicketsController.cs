using helpDesk.Dtos;
using helpDesk.Exceptions;
using helpDesk.Services;
using Microsoft.AspNetCore.Mvc;

namespace helpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsServices _services;
        public TicketsController(TicketsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tickets = _services.GetAll();

            return Ok(tickets);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var ticket = _services.GetById(id);

                var response = new ResponseDto<TicketDto>
                {
                    Data = ticket
                };

                return Ok(response);
            }

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }

        }


        [HttpPost]
        public IActionResult Create(CreateTicketDto dto)
        {
            try
            {
                var ticket = _services.Create(dto);

                var response = new ResponseDto<TicketDto>
                {
                    Data = ticket
                };

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTicketDto dto)
        {
            try
            {
                var ticket = _services.Update(id, dto);

                var response = new ResponseDto<TicketDto>
                {
                    Data = ticket
                };

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ticket = _services.Delete(id);

                var response = new ResponseDto<TicketDto>
                {
                    Data = ticket
                };

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }

        }
    }
}