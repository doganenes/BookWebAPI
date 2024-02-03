using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repositories;
using BookWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            List<Category> category = _service.GetAll();
            return Ok(category);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery]int id) {
            try
            {
                Category category = _service.GetById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]CategoryAddRequestDto dto)
        {
            _service.Add(dto);
            return Ok("Added successfully!");
        }
    }
}
