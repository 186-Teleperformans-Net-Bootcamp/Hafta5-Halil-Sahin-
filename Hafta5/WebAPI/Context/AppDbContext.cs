using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Context
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<FriendShipEntity> Friendships { get; set; }


    }
}
