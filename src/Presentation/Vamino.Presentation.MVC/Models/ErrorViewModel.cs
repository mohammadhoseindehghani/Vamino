namespace Vamino.Presentation.MVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; } 
        public string[]? ValidationErrors { get; set; } 
    }
}
