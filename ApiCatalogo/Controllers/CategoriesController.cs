﻿using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            try
            {
                return _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).ToList(); //carregar os relacionamento
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem with the request");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories.AsNoTracking().ToList();
            if (categories is null)
            {
                return NotFound("Categories not found");
            }

            return categories;
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (category is null)
                {
                    return NotFound($"Category com o id= {id} not found");
                }

                return category;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem with the request");
            }
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {
            if (category is null)
            {
                return BadRequest("Invalid Data");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Invalid Data");
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        public ActionResult<Category> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            Console.WriteLine(category);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }
    }
}
