namespace Domain.Entities
{
    public class ErrorResponse
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public int? ErrorCode { get; set; }
    }
}
