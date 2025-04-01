using Microsoft.EntityFrameworkCore;
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
    internal class GenreRepository : IRepository<Genre>
    {
        private readonly MusicContext db;

        public GenreRepository(MusicContext db)
        {
            this.db = db;
        }

        public async Task Create(Genre item)
        {
            await db.Genres.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Genre? genre = await db.Genres.FindAsync(id);
            if (genre != null)
            {
                db.Genres.Remove(genre);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Genre?> Get(int id)
        {
            return await db.Genres.Include(g => g.Songs).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre?> Get(string title)
        {
            return await db.Genres.Include(g => g.Songs).FirstOrDefaultAsync(g => g.Title == title);
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await db.Genres.Include(g => g.Songs).ToListAsync();
        }

        public void Update(Genre item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
