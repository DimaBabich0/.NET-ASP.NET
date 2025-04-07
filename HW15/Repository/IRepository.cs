using HW09.Models;

namespace HW09.Repository
{
    public interface IRepository
    {
        Task<List<Message>> GetMessageList();
        Task<List<User>> GetUsersList();
        Task<List<User>> GetUsersList(string login);
        Task Create(User item);
        Task Create(Message item);
        Task Save();
    }
}
