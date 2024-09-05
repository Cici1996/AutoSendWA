using AutoSendWhatsapp.Models;

namespace AutoSendWhatsapp.Services.BackgroundJobs
{
    public interface IBackgroundJobService
    {
        void SendMessageBackgroundJob(List<PersonContactDto> datas);
    }
}
