using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BadmintonCenter.Presentation.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;
        public string Message { get; set; } = null!;
        public int? Status { get; set; }

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string errMessage, int? statusCode)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Message = errMessage;
            Status = statusCode;
        }
    }

}
