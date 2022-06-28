using Backend.Data.Entities.Test;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Data
{
    public partial class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions options) : base(options)
        {
        
        }
        public virtual DbSet<Test> Test { get; set; }

    }
}
