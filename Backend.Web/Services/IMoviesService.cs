using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Data;
using MovieAPI1.Models;
using MovieAPI1.DTO;

namespace Backend.Web.Services
{
    public interface IMoviesService
    {
        
            //Task <IEnumerable<MovieAPI1.Models.Movies>> GetMovies();
            Task<ActionResult<IEnumerable<Movies>>> GetMovies();
            Task<MovieAPI1.Models.Movies> GetMovies(int Id);
            Task<MovieAPI1.Models.Movies> AddMovie(MovieDTO movie);
            Task<MovieAPI1.Models.Movies> UpdateMovie(int id, MovieDTO movie);
            void DeleteMovie(int Id);
        }
    }

