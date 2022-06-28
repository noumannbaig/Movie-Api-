using Backend.Application.DataTransferObjects.DTO;
using Backend.Data.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Repositories
{
    public class TicketRepository : TicketInterface
    {
        private readonly MovieAPI1Context appDbContext;

        public TicketRepository(MovieAPI1Context appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Ticket> CreateTicket(TicketDTO ticket)
        {

            var movie = await appDbContext.Movies.FirstOrDefaultAsync(e => e.Id == ticket.MovieId);
            var result = await appDbContext.Ticket.AddAsync(
                new Ticket
                {
                    Price = ticket.Price,
                    WatchDate = DateTime.Now,
                    MoviesId = movie.Id,
                    
                }
                );
            await appDbContext.SaveChangesAsync();
            return result.Entity;
            //throw new NotImplementedException();
        }

        public void DeleteTicket(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await appDbContext.Ticket.Include(e => e.Movie).ToArrayAsync();

            //throw new NotImplementedException();
        }

        public async Task<Ticket> GetTickets(int Id)
        {
            var ticket= await appDbContext.Ticket.Include(e => e.Movie).Where(e => e.Id == Id).FirstOrDefaultAsync();
            if (ticket == null)
            {
                return NotFoundResult();
            }
            return ticket;
        }

        private Ticket NotFoundResult()
        {
            throw new NotImplementedException("404 Not Found");
        }

        public Task<Ticket> UpdateTicket(int id, TicketDTO movie)
        {
            throw new NotImplementedException();
        }
    }
}
