using AutoSendWhatsapp.Models;
using AutoSendWhatsapp.Services.BackgroundJobs;
using AutoSendWhatsapp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AutoSendWhatsapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private readonly IExcelHelper _excelHelper;
        private readonly IBackgroundJobService _backgroundJobService;
        public MessageController(IExcelHelper excelHelper,IBackgroundJobService backgroundJobService)
        {
            _excelHelper = excelHelper;
            _backgroundJobService = backgroundJobService;
        }

        [HttpPost("/SendMessageBulk")]
        public IActionResult SendMessagesBulk(IFormFile file)
        {
            List<PersonContactDto> datas = _excelHelper.MapPersonDataFromExcel(file);
            _backgroundJobService.SendMessageBackgroundJob(datas);
            return Ok(datas);
        }
    }
}
