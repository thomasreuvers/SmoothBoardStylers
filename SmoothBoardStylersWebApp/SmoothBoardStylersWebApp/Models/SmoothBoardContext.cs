using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmoothBoardStylersWebApp.Models
{
    public class SmoothBoardContext : DbContext
    {
        public SmoothBoardContext(DbContextOptions<SmoothBoardContext> options)
            : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<SubscriberModel> Subscribers { get; set; }
        public DbSet<SmoothBoardModel> SmoothBoards { get; set; }
    }
}
