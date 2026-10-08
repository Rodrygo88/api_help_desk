using System.ComponentModel.DataAnnotations;
using helpDesk.Enums;

namespace helpDesk.Dtos
{
    public class UpdateTicketDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public TicketStatus Status { get; set; }

        [Required]
        public TicketPriority Priority { get; set; }
    }
}