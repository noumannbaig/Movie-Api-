﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI1.Models;
using MovieAPI1.DTO;
using MovieAPI1.Interface;
using Backend.Data;
using Microsoft.AspNetCore.Authorization;

namespace MovieAPI1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class MoviesController : ControllerBase
    {
        private readonly MovieAPI1Context _context;
        private readonly RepositoryPatternClass _content;
        //private readonly DummyService _services;
        
        public MoviesController(MovieAPI1Context context)
        {
            _context = context;
            _content=new RepositoryPatternClass (context);
        }
        

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            //return await _context.Movies.ToListAsync();
            return await _content.GetMovies();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(int id)
        {

            return  await _content.GetMovies(id);
            //var movies = await _context.Movies.FindAsync(id);

            //if (movies == null)
            //{
            //    return NotFound();
            //}

            //return movies;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovies(int id, MovieDTO movies)
        {
           
            if (!MoviesExists(id))
            {
                return BadRequest();
            }
            
            //_context.Entry(
              //  new Movies
                //{
                  //  Id = id,
                    //Title=movies.Title,
                    //Genre=movies.Genre,
                    //Updatedat=DateTime.Now,
                //}
           //).State = EntityState.Modified;
          await _content.UpdateMovie(
              id,
              new MovieDTO
              {
                   
                  Title=movies.Title,
                  Genre = movies.Genre
                  
              }
              );
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MoviesExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(MovieDTO movies)
        {
           var result= await _content.AddMovie(movies);
            return CreatedAtAction("GetMovies", new { id = result.Id }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //public void DeleteMovieis(int id)
        //{
        //    _content.DeleteMovie(id);
        //}

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}