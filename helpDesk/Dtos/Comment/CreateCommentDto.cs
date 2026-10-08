using System.ComponentModel.DataAnnotations;

namespace helpDesk.Dtos
{
    public class CreateCommentDto
    {
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Content { get; set; }  = string.Empty;

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TicketId { get; set; }
    }
}