using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;

namespace BookWebApi.Services.Interfaces;

public interface ICategoryService
{
    List<Category> GetAll();
    Category GetById(int id);
    void Add(CategoryAddRequestDto category);

}