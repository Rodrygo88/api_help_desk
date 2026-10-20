namespace helpDesk.Dtos
{
    public class ResponseDto
    {
        public List<string> Errors { get; set; } = [];
    }

    public class ResponseDto<T> : ResponseDto
    {
        public T? Data { get; set; }
    }
}