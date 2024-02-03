using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repositories;
using BookWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookWebApi.Services.Concrete;

public class BookService : IBookService
{
    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;

    public BookService(BaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Add(BookAddRequestDto dto)
    {
        Book book = _mapper.Map<Book>(dto);
        _context.Books.Add(book);
        _context.SaveChanges(); 
    }

    public void Delete(int id)
    {
        Book? book = _context.Books.Find(id);
        if(book != null)
        {
            throw new Exception($"Book with id: {id} was not found!");
        }
        _context.Books.Remove(book);
    }

    public List<Book> GetAll()
    {
        List<Book>books = _context.Books.ToList();
        return books;
    }

    public Book GetById(int id)
    {
        Book? book = _context.Books.Find(id);
        if( book != null )
        {
            throw new Exception($"Book with id: {id} was not found!");
        }
        return book;
    }

    public void Update(BookUpdateRequestDto dto)
    {
        Book? book = _context.Books.Find(dto.Id);
        if( book != null )
        {
            throw new Exception($"Book with id: {dto.Id} was not found!");
        }
        Book updatedBook = _mapper.Map<Book>(dto);  
        _context.Books.Update(updatedBook);
        _context.SaveChanges();
    }
}
