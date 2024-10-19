using System.ComponentModel.DataAnnotations;

namespace ToolsToWorkers.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }

        public User() { }

        public User(string surname, string name, string patronymic, string role, string status, string login, string hashedPassword)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Role = role;
            Status = status;
            Login = login;
            HashedPassword = hashedPassword;
        }
    }
}
