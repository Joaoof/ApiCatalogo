﻿using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context; // instância 

        public ProductsController(AppDbContext context)
        {
            _context = context;
        } // instância do context injetada no controlador

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.ToList(); // retorna uma lista de produtos
            if (products is null)
            {
                return NotFound("Products not found");
            }
            return products;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id); // localizando o primeiro elemento encontrado
            if (product is null)
            {
                return NotFound("Product not found");
            }
            return product;
        }
    }
}
