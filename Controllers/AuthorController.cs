using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorController(IAuthorService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AuthorAddRequestDto dto)
        {
            _service.Add(dto);
            return Ok(dto);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            List<Author> authors = _service.GetAll();
            return Ok(authors);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] int id)
        {
            try
            {
                Author author = _service.GetById(id);
                return Ok(author);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}
