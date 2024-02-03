using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repositories;
using BookWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Services.Concrete
{
    public class AuthorService : IAuthorService
    {
        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;
        public void Add(AuthorAddRequestDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public List<Author> GetAll()
        {
            List<Author> Authors = _context.Authors.ToList();
            return Authors;

        }

        public Author GetById(int id)
        {
            Author? author = _context.Authors.Find(id);
            if(author == null)
            {
                throw new Exception($"Author which have id : {id} not found!");
            }
            return author;
        }

        
    }
}
