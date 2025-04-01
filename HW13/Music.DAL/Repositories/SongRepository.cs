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
    internal class SongRepository : IRepository<Song>
    {
        private readonly MusicContext db;

        public SongRepository(MusicContext db)
        {
            this.db = db;
        }

        public async Task Create(Song item)
        {
            await db.Songs.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Song? song = await db.Songs.FindAsync(id);
            if (song != null)
            {
                db.Songs.Remove(song);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Song?> Get(int id)
        {
            return await db.Songs
                .Include(s => s.Author)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Song?> Get(string title)
        {
            return await db.Songs
                .Include(s => s.Author)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(s => s.Title == title);
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            return await db.Songs
                .Include(s => s.Author)
                .Include(s => s.Genre)
                .ToListAsync();
        }

        public void Update(Song item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
