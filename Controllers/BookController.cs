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
        public class BookController : ControllerBase
        {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
          List<Book> books = _service.GetAll();
            return Ok(books);
        }

        [HttpGet]
        public IActionResult GetById([FromQuery]int id)
        {
            Book book = _service.GetById(id);
            return Ok(book);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]BookUpdateRequestDto dto)
        {
            _service.Update(dto);
            return Ok("Updated successfully!");
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]BookAddRequestDto dto)
        {
            _service.Add(dto);
            return Ok("Added successfully!");
        }

        [HttpPost]
        public IActionResult Delete([FromQuery]int id)
        {
            _service.Delete(id);
            return Ok("Deleted successfully!");
        }
    }
}
