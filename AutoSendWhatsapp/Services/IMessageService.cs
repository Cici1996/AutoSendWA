using AutoSendWhatsapp.Models;

namespace AutoSendWhatsapp.Services
{
    public interface IMessageService
    {
        Task<bool> SendMessageWAAsync(SendMessageWaRequest data);
    }
}
