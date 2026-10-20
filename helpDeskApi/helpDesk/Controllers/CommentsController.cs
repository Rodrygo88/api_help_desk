using helpDesk.Dtos;
using helpDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace helpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsServices _services;

        public CommentsController(CommentsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = _services.GetAll();

            var response = new ResponseDto<List<CommentDto>>
            {
                Data = comments
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var comment = _services.GetById(id);

            var response = new ResponseDto<CommentDto>
            {
                Data = comment
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(CreateCommentDto dto)
        {
            var comment = _services.Create(dto);

            var response = new ResponseDto<CommentDto>
            {
                Data = comment
            };

            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCommentDto dto)
        {
            var comment = _services.Update(id, dto);

            var response = new ResponseDto<CommentDto>
            {
                Data = comment
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comment = _services.Delete(id);

            var response = new ResponseDto<CommentDto>
            {
                Data = comment
            };

            return Ok(response);
        }

    }
}
