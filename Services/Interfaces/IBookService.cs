using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;

namespace BookWebApi.Services.Interfaces;

public interface IBookService
{
    List<Book> GetAll();
    Book GetById(int id);
    void Update(BookUpdateRequestDto dto);
    void Add(BookAddRequestDto dto);
    void Delete(int id);
}
