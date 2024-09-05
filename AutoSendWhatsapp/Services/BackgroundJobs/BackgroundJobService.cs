using AutoSendWhatsapp.Models;
using AutoSendWhatsapp.Utils;
using Hangfire;

namespace AutoSendWhatsapp.Services.BackgroundJobs
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<BackgroundJobService> _logger;

        public BackgroundJobService(IMessageService messageService, ILogger<BackgroundJobService> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        public async Task SendMessageBulk(List<PersonContactDto> datas)
        {
            foreach (var data in datas) {
                if(string.IsNullOrEmpty(data.Message) || string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.PhoneNumber)) continue;
                var dataToSend = new SendMessageWaRequest
                {
                    ChatId = $"{ExcelHelper.ConvertPhoneNumberId(data.PhoneNumber)}",
                    Text = data.Message
                };
                var isSuccess = await _messageService.SendMessageWAAsync(dataToSend);
                if (isSuccess) {
                    _logger.LogInformation($"already Sent for '{data?.Name} ({data?.PhoneNumber})'");
                }
                else
                {
                    _logger.LogInformation($"sent failed for '{data?.Name} ({data?.PhoneNumber})'");
                }
                await Task.Delay(6000);
            }
        }

        public void SendMessageBackgroundJob(List<PersonContactDto> datas)
        {
            BackgroundJob.Enqueue(() => SendMessageBulk(datas));
        }
    }
}
