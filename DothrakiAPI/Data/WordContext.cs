using DothrakiAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DothrakiAPI.Data
{
    public class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options)
            : base(options)
        {
            // Make sure database is created when using the db context
            Database.EnsureCreated();
        }
        public DbSet<Word> Words { get; set; }
    }
}
