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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chat>()
                .HasOne(p => p.User)
                .WithMany(b => b.Chats)
                .HasForeignKey(b => b.UserId);
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .HasOne(p => p.Chat)
                .WithMany(b => b.Messages)
                .HasForeignKey(b => b.ChatId);
        }
        public DbSet<Message> MessagesDatabase { get; set; }
        public DbSet<Chat> ChatsDatabase { get; set; }
    }
}
