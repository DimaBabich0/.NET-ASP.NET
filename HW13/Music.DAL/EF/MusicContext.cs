using Microsoft.EntityFrameworkCore;
using Music.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DAL.EF
{
    internal class MusicContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
