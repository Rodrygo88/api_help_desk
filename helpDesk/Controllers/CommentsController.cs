using helpDesk.Dtos;
using helpDesk.Exceptions;
using helpDesk.Services;
using Microsoft.AspNetCore.Mvc;

namespace helpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                var comment = _services.GetById(id);

                var response = new ResponseDto<CommentDto>
                {
                    Data = comment
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
        public IActionResult Create(CreateCommentDto dto)
        {
            try
            {
                var comment = _services.Create(dto);

                var response = new ResponseDto<CommentDto>
                {
                    Data = comment
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCommentDto dto)
        {
            try
            {
                var comment = _services.Update(id, dto);

                var response = new ResponseDto<CommentDto>
                {
                    Data = comment
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
                var comment = _services.Delete(id);

                var response = new ResponseDto<CommentDto>
                {
                    Data = comment
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