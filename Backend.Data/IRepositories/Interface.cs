using MovieAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI1.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI1.Interface
{
    public interface MovieRepositoryInterface
    {
        //Task <IEnumerable<MovieAPI1.Models.Movies>> GetMovies();
        Task <ActionResult<IEnumerable<Movies>>> GetMovies();
        Task<MovieAPI1.Models.Movies> GetMovies(int Id);
        Task<MovieAPI1.Models.Movies> AddMovie(MovieDTO movie);
        Task<MovieAPI1.Models.Movies> UpdateMovie(int id,MovieDTO movie);
        void DeleteMovie(int Id);
    }
}
