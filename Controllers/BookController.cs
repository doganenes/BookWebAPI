using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
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

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery]int id)
        {
            try
            {
                Book book = _service.GetById(id);
                return Ok(book);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody]BookUpdateRequestDto dto)
        {
            try
            {
                _service.Update(dto);
                return Ok("Updated successfully!");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]BookAddRequestDto dto)
        {
            _service.Add(dto);
            return Ok("Added successfully!");
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery]int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("Deleted successfully!");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            List<BookResponseDto> results =  _service.GetAllDetails();
            return Ok(results);
        }

        [HttpGet("getdetailsbyid")]
        public IActionResult GetDetailsById([FromQuery]int id)
        {
            try
            {
                BookResponseDto result = _service.GetDetailsById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getbycategoryid")]
        public IActionResult GetByCategoryId([FromQuery]int categoryId)
        {
            List<BookResponseDto> response = _service.GetByCategoryId(categoryId);
            return Ok(response);
        }

        [HttpGet("getbyauthorid")]
        public IActionResult GetByAuthorId([FromQuery]int AuthorId)
        {
            List<BookResponseDto> response = _service.GetByAuthorId(AuthorId);
            return Ok(response);
        }

        [HttpGet("getbypricerange")]
        public IActionResult GetByPriceRangeDetails([FromQuery]double min, [FromQuery] double max)
        {
            List<BookResponseDto> responses = _service.GetByPriceRangeDetails(min, max);
            return Ok(responses);
        }

        [HttpGet("getbytitlecontains")]
        public IActionResult GetByTitleContains([FromQuery]string title)
        {
            List<BookResponseDto> responses = _service.GetByTitleContains(title);
            return Ok(responses);
        }
    }
}
