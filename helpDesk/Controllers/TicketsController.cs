using helpDesk.Dtos;
using helpDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace helpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

            var response = new ResponseDto<List<TicketDto>>
            {
                Data = tickets
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _services.GetById(id);

            var response = new ResponseDto<TicketDto>
            {
                Data = ticket
            };

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(CreateTicketDto dto)
        {
            var ticket = _services.Create(dto);

            var response = new ResponseDto<TicketDto>
            {
                Data = ticket
            };

            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTicketDto dto)
        {
            var ticket = _services.Update(id, dto);

            var response = new ResponseDto<TicketDto>
            {
                Data = ticket
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _services.Delete(id);

            var response = new ResponseDto<TicketDto>
            {
                Data = ticket
            };

            return Ok(response);
        }
    }
}
