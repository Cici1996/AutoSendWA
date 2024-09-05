using AutoSendWhatsapp.Constants;
using AutoSendWhatsapp.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AutoSendWhatsapp.Utils
{
    public class ExcelHelper : IExcelHelper
    {
        private readonly IConfiguration _configuration;
        public ExcelHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<PersonContactDto> MapPersonDataFromExcel(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            IWorkbook workbook = new XSSFWorkbook(stream);
            var sheet = workbook.GetSheetAt(0);
            var listDatas = new List<PersonContactDto>();
            for (var row = 1; row <= sheet.LastRowNum; row++)
            {
                var sheetRow = sheet.GetRow(row);
                if (sheetRow == null) continue;

                var name = sheetRow.GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                var phoneNumber = sheetRow.GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                string messageText = _configuration[GlobalConstants.MessageText]?.ToString() ?? string.Empty;
                string namePerson = name?.ToString() ?? string.Empty;
                messageText = messageText.Replace("{{name}}", Uri.EscapeDataString(namePerson));

                listDatas.Add(new PersonContactDto
                {
                    Name = namePerson,
                    PhoneNumber = phoneNumber?.ToString() ?? string.Empty,
                    Message = messageText
                });
            }

            return listDatas;
        }

        public static string ConvertPhoneNumberId(string input)
        {
            if (input.StartsWith("+62"))
                return "62" + input.Substring(3);
            else if (input.StartsWith("0"))
                return "62" + input.Substring(1);
            return input;
        }
    }
}
