using AutoSendWhatsapp.Constants;
using AutoSendWhatsapp.Models;
using System.Net;

namespace AutoSendWhatsapp.Services
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        public MessageService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(GlobalConstants.WAHAClient);
        }

        public async Task<bool> SendMessageWAAsync(SendMessageWaRequest data)
        {
            var datas = await _httpClient.PostAsJsonAsync("api/sendText", data);
            if(datas.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;

        }
    }
}
