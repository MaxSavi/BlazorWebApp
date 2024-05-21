using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorWebApp.Models;

namespace BlazorWebApp.Data
{
    public class BlazorWebAppContext : DbContext
    {
        public BlazorWebAppContext (DbContextOptions<BlazorWebAppContext> options)
            : base(options)
        {
        }
        public DbSet<BlazorWebApp.Models.UserModel> UserModel { get; set; } = default!;
        public DbSet<BlazorWebApp.Models.BookModel> BookModel { get; set; } = default!;
        
    }
}
