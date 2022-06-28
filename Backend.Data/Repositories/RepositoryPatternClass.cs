using MovieAPI1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI1.DTO;
using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using System;

namespace MovieAPI1.Interface
{
    public class RepositoryPatternClass : MovieRepositoryInterface
    {
        private readonly MovieAPI1Context appDbContext;

        public RepositoryPatternClass(MovieAPI1Context appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Movies> AddMovie(MovieDTO movie)
        {
            var result = await appDbContext.Movies.AddAsync(
                new Movies
                {
                    Title=movie.Title,
                    Genre=movie.Genre,
                    ReleaseDate=System.DateTime.Now,
                    Updatedat=System.DateTime.Now,

                }
                );
            await appDbContext.SaveChangesAsync();
            return result.Entity;
           // throw new System.NotImplementedException();
        }

       

        public async void DeleteMovie(int Id)
        {
            //var movies = await  appDbContext.Movies.FindAsync(Id);
            var result = await appDbContext.Movies
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result == null)
            {
                 NotFoundResult();
            }
            
            else 
            {
                appDbContext.Movies.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
            //throw new System.NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            return await appDbContext.Movies.Include(e => e.Tickets).ToArrayAsync();
            //throw new system.notimplementedexception();
        }

        public async Task<Movies> GetMovies(int Id)
        {
            var movies = await appDbContext.Movies.FindAsync(Id);

            if (movies == null)
            {
                return NotFoundResult();
            }

            return await appDbContext.Movies
                .FirstOrDefaultAsync(e => e.Id == Id);
           // throw new System.NotImplementedException();
        }

        private Movies NotFoundResult()
        {
            throw new NotImplementedException("Not Found");
        }

        public async Task<Movies> UpdateMovie(int id,MovieDTO movie)
        {
            var result = await appDbContext.Movies
                .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.Title = movie.Title;
                result.Genre = movie.Genre;
                result.Updatedat = System.DateTime.Now;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            else
            {
                return BadRequestResult();
            }
            //return null;
           // throw new System.NotImplementedException();
        }

        private Movies BadRequestResult()
        {
            throw new NotImplementedException();
        }
    }
}
