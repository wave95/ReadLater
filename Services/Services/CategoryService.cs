using AutoMapper;
using Data;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CategoryService : ReadLaterService, ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        
        private readonly ILogger<CategoryService> _logger;

        IMapper autoMapper;


        public CategoryService(ReadLaterDataContext readLaterDataContext, IHttpContextAccessor httpContextAccessor, ILogger<CategoryService> logger) 
            :base(httpContextAccessor)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _logger = logger;

            var autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryModel, Category>().ReverseMap();
            });
            autoMapper = autoMapperConfiguration.CreateMapper();

        }

        public CategoryModel CreateCategory(CategoryModel categoryModel)
        {
            try
            {
                var category = autoMapper.Map<Category>(categoryModel);
                category.UserId = GetAuthenticatedUserUid();

                _ReadLaterDataContext.Add(category);
                _ReadLaterDataContext.SaveChanges();

                return autoMapper.Map<CategoryModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public void UpdateCategory(CategoryModel categoryModel)
        {
            try
            {
                var category = autoMapper.Map<Category>(categoryModel);

                if (category != null && category.UserId.Equals(category))
                {

                    _ReadLaterDataContext.Update(category);
                    _ReadLaterDataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }

        }

        public List<CategoryModel> GetCategories()
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();
                var categories = autoMapper.Map<List<CategoryModel>>(_ReadLaterDataContext.Categories.Where(c => c.UserId.Equals(userUid)).ToList());

                if (categories == null)
                    return new List<CategoryModel>();
                else
                    return categories;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public CategoryModel GetCategory(int Id)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();
                var category = _ReadLaterDataContext.Categories.Where(c => c.ID == Id  && c.UserId.Equals(userUid)).FirstOrDefault();

                return autoMapper.Map<CategoryModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public CategoryModel GetCategory(string Name)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();
                var category = _ReadLaterDataContext.Categories.Where(c => c.Name == Name && c.UserId.Equals(userUid)).FirstOrDefault();

                return autoMapper.Map<CategoryModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public void DeleteCategory(int Id)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();
                var category = _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();

                if (category != null && category.UserId.Equals(userUid))
                {
                    _ReadLaterDataContext.Categories.Remove(category);
                    _ReadLaterDataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        
    }
}
