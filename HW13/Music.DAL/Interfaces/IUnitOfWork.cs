using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Music.DAL.Entities;


namespace Music.DAL.Interfaces
{
    internal interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Admin> Admins { get; }
        IRepository<Author> Authors { get; }
        IRepository<Song> Songs { get; }
        IRepository<Genre> Genres { get; }
        Task Save();
    }
}
