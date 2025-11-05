using Microsoft.EntityFrameworkCore;
using New_Game_MVC.Models;

namespace New_Game_MVC
{
   

        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<GameResult> GameResults { get; set; }
        }
    }

