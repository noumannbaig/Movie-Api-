using Microsoft.AspNetCore.Mvc;
using MovieAPI1.DTO;
using MovieAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI1.Interface;
using Backend.Data;
using System.Linq;
using Backend.Application.Wrappers;
using System;

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
            var result= _content.AddMovie(movie);
            return result;
        }

        public void DeleteMovie(int Id)
        {
             _content.DeleteMovie(Id);
            throw new System.NotImplementedException();
        }

        public async Task<Response<dynamic>> GetMovies()
        {
            dynamic result = await _content.GetMovies();
            if(result == null)
            {
                return NotFoundResult();
            }

            return new Response<dynamic>(true,result, "Records sent succesfully");

        }

        private Response<dynamic> NotFoundResult()
        {
            throw new NotImplementedException("Not Found");
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
