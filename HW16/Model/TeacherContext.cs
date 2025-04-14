using Microsoft.EntityFrameworkCore;

namespace HW16.Model
{
    public class TeacherContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public TeacherContext(DbContextOptions<TeacherContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                Teachers.Add(new Teacher
                {
                    Name = "Irina",
                    Surname = "Petrova",
                    Age = 42,
                    Subject = "Mathematics"
                });
                Teachers.Add(new Teacher
                {
                    Name = "Alexey",
                    Surname = "Kuznetsov",
                    Age = 35,
                    Subject = "History"
                });
                Teachers.Add(new Teacher
                {
                    Name = "Olga",
                    Surname = "Smirnova",
                    Age = 29,
                    Subject = "English"
                });
                SaveChanges();
            }
        }
    }
}
