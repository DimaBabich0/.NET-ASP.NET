using Music.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.DAL.Entities;
using Music.DAL.EF;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Music.DAL.Repositories
{
    internal class UserRepository : IRepository<User>
    {
        private MusicContext db;

        public UserRepository(MusicContext db)
        {
            this.db = db;
        }
        public async Task Create(User item)
        {
            await db.Users.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            User? user = await db.Users.FindAsync(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public async Task<User> Get(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<User> Get(string login)
        {
            var users = await db.Users.Where(a => a.Login == login).ToListAsync();
            User? user = users?.FirstOrDefault();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
