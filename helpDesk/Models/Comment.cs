namespace helpDesk.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Ticket Ticket { get; set; } = null!;
        public int TicketId { get; set; }
        public User User { get; set; }= null!;
        public int UserId { get; set; }

    }
}