using AutoSendWhatsapp.Models;

namespace AutoSendWhatsapp.Utils
{
    public interface IExcelHelper
    {
        List<PersonContactDto> MapPersonDataFromExcel(IFormFile file);
    }
}
