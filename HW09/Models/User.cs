using System.ComponentModel.DataAnnotations;

namespace HW09.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
