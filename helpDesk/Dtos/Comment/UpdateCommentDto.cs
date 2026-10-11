using System.ComponentModel.DataAnnotations;

namespace helpDesk.Dtos
{
    public class UpdateCommentDto
    {
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Content { get; set; }  = string.Empty;
    }
}