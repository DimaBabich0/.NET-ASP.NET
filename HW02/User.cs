namespace HW02
{
    public static class Config
    {
        public static string FilePath = "users.txt";
    }

    struct User
    {
        public string Name { get; }
        public string Login { get; }
        public string Password { get; }

        public User(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return $"{Name},{Login},{Password}";
        }

        public string ShowInfo()
        {
            return $"Name: {Name}; Login: {Login}; Password: {Password}";
        }

        public static User FromString(string data)
        {
            string[] parts = data.Split(',');
            if (parts.Length == 3)
            {
                return new User(parts[0], parts[1], parts[2]);
            }
            else
            {
                throw new Exception("Error in user data format");
            }
        }
    }
}
