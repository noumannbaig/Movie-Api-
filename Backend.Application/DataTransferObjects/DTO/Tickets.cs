using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.DTO
{
    public class TicketDTO
    {
        public int MovieId { get; set; }

        public int Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime WatchDate { get; set; }
    }
}
