using HW09.Controllers;
using Microsoft.EntityFrameworkCore;

namespace HW09.Models
{
    public class GuestBookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public GuestBookContext(DbContextOptions<GuestBookContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                string salt, hash;

                salt = MyCryptography.GetSalt();
                hash = MyCryptography.GenerateHash(salt, "qwerty");
                var user1 = new User { Login = "DimaB", Password = hash, Salt = salt };

                salt = MyCryptography.GetSalt();
                hash = MyCryptography.GenerateHash(salt, "123456");
                var user2 = new User { Login = "Alex", Password = hash, Salt = salt };
                Users?.AddRange(user1, user2);
                SaveChanges();

                Messages?.AddRange( new Message { 
                    Id_User = user1.Id,
                    Text = "Hello!",
                    MessageDate = DateTime.Now.AddMinutes(-2)
                }, new Message {
                    Id_User = user2.Id,
                    Text = "Test",
                    MessageDate = DateTime.Now.AddMinutes(-1)
                });
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.Id_User);
        }
    }
}
