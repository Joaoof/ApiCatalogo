﻿using ApiCatalogo.Context;
using ApiCatalogo.Interfaces;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base (context) 
        {

        }
    }
}
