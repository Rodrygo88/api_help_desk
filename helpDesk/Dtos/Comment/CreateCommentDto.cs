namespace helpDesk.Dtos
{
    public class CreateCommentDto
    {
        public string Content { get; set; }  = string.Empty;
        public int UserId { get; set; }
        public int TicketId { get; set; }
    }
}