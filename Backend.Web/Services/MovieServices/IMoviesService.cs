using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Data;
using MovieAPI1.Models;
using MovieAPI1.DTO;

namespace Backend.Web.Services.MovieServices
{
    public interface IMoviesService
    {

        //Task <IEnumerable<MovieAPI1.Models.Movies>> GetMovies();
        Task<ActionResult<IEnumerable<Movies>>> GetMovies();
        Task<Movies> GetMovies(int Id);
        Task<Movies> AddMovie(MovieDTO movie);
        Task<Movies> UpdateMovie(int id, MovieDTO movie);
        void DeleteMovie(int Id);
        bool MovieExist(string Title);
        bool MovieExists(int Id); 
    }
}

