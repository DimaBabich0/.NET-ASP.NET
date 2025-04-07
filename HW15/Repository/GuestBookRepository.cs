using HW09.Models;
using Microsoft.EntityFrameworkCore;

namespace HW09.Repository
{
    public class GuestBookRepository : IRepository
    {
        private readonly GuestBookContext _context;

        public GuestBookRepository(GuestBookContext context)
        {
            _context = context;
        }

        public async Task Create(User item)
        {
            await _context.Users.AddAsync(item);
        }

        public async Task Create(Message item)
        {
            await _context.Messages.AddAsync(item);
        }

        public async Task<List<Message>> GetMessageList()
        {
            return await _context.Messages
                .Include(m => m.User)
                .OrderByDescending(m => m.MessageDate)
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersList(string login)
        {
            return await _context.Users
                .Where(u => u.Login == login)
                .ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
