using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HW09.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int Id_User { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Message must not exceed 30 characters")]
        public string Text { get; set; }

        [Required]
        public DateTime MessageDate { get; set; }
    }
}
