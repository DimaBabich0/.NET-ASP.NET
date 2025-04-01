using Music.DAL.EF;
using Music.DAL.Entities;
using Music.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DAL.Repositories
{
    internal class EFUnitOfWork : IUnitOfWork
    {
        private MusicContext db;
        private UserRepository userRepository;
        private AdminRepository adminRepository;
        private AuthorRepository authorRepository;
        private GenreRepository genreRepository;
        private SongRepository songRepository;
        public EFUnitOfWork(MusicContext _context) 
        {
            db = _context;
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Admin> Admins
        {
            get
            {
                if (adminRepository == null)
                    adminRepository = new AdminRepository(db);
                return adminRepository;
            }
        }

        public IRepository<Author> Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                return authorRepository;
            }
        }

        public IRepository<Song> Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
