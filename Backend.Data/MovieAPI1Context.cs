using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI1.Models;
using Backend.Application.IdentityAuth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Backend.Data
{
    public class MovieAPI1Context : IdentityDbContext<ApplicationUser>
    {
        private readonly MovieAPI1Context DbContext;
        public MovieAPI1Context (DbContextOptions<MovieAPI1Context> options)
            : base(options)
        {

        }

        public DbSet<MovieAPI1.Models.Movies> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieAPI1.Models.Movies>()
                .HasData(
                new Movies
                {
                    Id = 1,
                    Title = "Titanic",
                    Genre = "Thriller",
                    ReleaseDate = DateTime.Now,
                    Updatedat = DateTime.Now,
                   

                }
                ) ;
            modelBuilder.Entity<Movies>()
       .HasMany(c => c.Tickets)
       .WithOne(e => e.Movie);

            


        }

        public DbSet<MovieAPI1.Models.Ticket> Ticket { get; set; }

    }
}
