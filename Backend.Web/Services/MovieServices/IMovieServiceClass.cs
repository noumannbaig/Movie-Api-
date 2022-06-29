using Microsoft.AspNetCore.Mvc;
using MovieAPI1.DTO;
using MovieAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI1.Interface;
using Backend.Data;
using System.Linq;

namespace Backend.Web.Services.MovieServices
{
    public class IMovieServiceClass : IMoviesService
    {
        private readonly MovieAPI1Context _context;
        private readonly RepositoryPatternClass _content;
        public IMovieServiceClass(MovieAPI1Context context)
        {
            _context = context;
            _content = new RepositoryPatternClass(context);
        }
        public Task<Movies> AddMovie(MovieDTO movie)
        {
            
            if(MovieExist(movie.Title))
            {
                return null;
            }
            return _content.AddMovie(movie);
        }

        public void DeleteMovie(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            throw new System.NotImplementedException();
        }

        public Task<Movies> GetMovies(int Id)
        {
            throw new System.NotImplementedException();
        }

        public bool MovieExist(string Title)
        {
            return _context.Movies.Any(e => e.Title == Title);

        }

        public bool MovieExists(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movies> UpdateMovie(int id, MovieDTO movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
