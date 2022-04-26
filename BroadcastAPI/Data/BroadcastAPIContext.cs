#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BroadcastAPI.Models;

namespace BroadcastAPI.Data
{
    public class BroadcastAPIContext : DbContext
    {
        public BroadcastAPIContext (DbContextOptions<BroadcastAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BroadcastAPI.Models.Message> Message { get; set; }
    }
}
