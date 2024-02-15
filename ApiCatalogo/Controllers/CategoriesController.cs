﻿using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<Category>> Get()
        {

           var categories = _repository.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {

                var category = _repository.Get(c => c.CategoryId == id);  
       
                if (category is null)
                {
                    _logger.LogWarning($"Category with id = {id} not found");
                    return NotFound($"Category com o id= {id} not found");
                }

                return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {
            if (category is null)
            {
                _logger.LogWarning($"Invalid Data");
                return BadRequest("Invalid Data");
            }

            var createCategory= _repository.Create(category);

            return new CreatedAtRouteResult("GetCategory", new { id = createCategory.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogWarning($"Invalid Data");
                return BadRequest("Invalid Data");
            }

            var categoryUpdate = _repository.Update(category);

            return Ok(categoryUpdate);
        }

        [HttpDelete]
        public ActionResult<Category> Delete(int id)
        {
            var category = _repository.Get(c => c.CategoryId == id) ;

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found");
                return NotFound("Category not found");
            }

            var categoryDeleted = _repository.Delete(category);
            return Ok(categoryDeleted);
        }
    }
}
