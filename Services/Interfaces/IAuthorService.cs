﻿using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;

namespace BookWebApi.Services.Interfaces
{
    public interface IAuthorService
    {
        void Add(AuthorAddRequestDto dto);
        List<Author> GetAll();
        Author GetById(int id);
    }
}
