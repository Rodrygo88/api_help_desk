using helpDesk.Enums;

namespace helpDesk.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Ticket> CustomerTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets {get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments {get; set; } = new List<Comment>();
    }
}