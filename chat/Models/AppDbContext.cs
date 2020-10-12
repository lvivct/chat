using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chat.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserChat>().HasKey(sc => new { sc.ChatId, sc.UserId });

            modelBuilder.Entity<AppUserChat>()
                .HasOne<Chat>(sc => sc.Chat)
                .WithMany(s => s.AppUsersChats)
                .HasForeignKey(sc => sc.ChatId);

            modelBuilder.Entity<AppUserChat>()
                .HasOne<AppUser>(sc => sc.User)
                .WithMany(s => s.AppUsersChats)
                .HasForeignKey(sc => sc.UserId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .HasOne(p => p.Chat)
                .WithMany(b => b.Messages)
                .HasForeignKey(b => b.ChatId);
        }
        public DbSet<Message> MessagesDatabase { get; set; }
        public DbSet<Chat> ChatsDatabase { get; set; }
        public DbSet<AppUserChat> ChatsUsersDatabase { get; set; }

    }
}
