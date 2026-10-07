using helpDesk.Enums;

namespace helpDesk.Dtos
{
    public class CreateTicketDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public int CustomerId { get; set; } 
    }
}