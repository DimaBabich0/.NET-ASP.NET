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
    internal class AdminRepository : IRepository<Admin>
    {
        private readonly MusicContext db;

        public AdminRepository(MusicContext db)
        {
            this.db = db;
        }

        public async Task Create(Admin item)
        {
            await db.Admins.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Admin? admin = await db.Admins.FindAsync(id);
            if (admin != null)
            {
                db.Admins.Remove(admin);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Admin?> Get(int id)
        {
            return await db.Admins.FindAsync(id);
        }

        public async Task<Admin?> Get(string login)
        {
            return await db.Admins.FirstOrDefaultAsync(a => a.Login == login);
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await db.Admins.ToListAsync();
        }

        public void Update(Admin item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
