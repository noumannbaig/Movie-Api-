using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Data;
using MovieAPI1.Models;
using Backend.Application.DataTransferObjects.DTO;
namespace Backend.Data.IRepositories
{
    public interface TicketInterface
    {
        Task<ActionResult<IEnumerable<Ticket>>> GetTickets();
        Task<MovieAPI1.Models.Ticket> GetTickets(int Id);
        Task<MovieAPI1.Models.Ticket> CreateTicket(TicketDTO movie);
        Task<MovieAPI1.Models.Ticket> UpdateTicket(int id, TicketDTO movie);
        void DeleteTicket(int Id);
    }
}
