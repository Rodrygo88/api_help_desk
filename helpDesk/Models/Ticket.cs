using helpDesk.Enums;

namespace helpDesk.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        

        // Quem pediu o chamado
        public int CustomerId { get; set; } 
        public User CustomerUser { get; set; } = null!;

        // Quem aceitou o chamado
        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; } 

        public ICollection<Comment> Comments {get; set; } = new List<Comment>();
 
        
    }
}