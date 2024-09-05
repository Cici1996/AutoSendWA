using AutoSendWhatsapp.Constants;

namespace AutoSendWhatsapp.Models
{
    public class SendMessageWaRequest
    {
        public string ChatId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Session { get; set; } = GlobalConstants.Default.ToLower();
    }
}
