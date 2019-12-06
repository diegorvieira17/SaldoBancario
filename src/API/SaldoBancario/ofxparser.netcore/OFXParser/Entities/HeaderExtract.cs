using System;

namespace OFXParser.Entities
{
    public class HeaderExtract
    {
        public string Status { get; set; }
        public string Language { get; set; }
        public DateTime ServerDate { get; set; }
        public string BankName { get; set; }

        public HeaderExtract()
        {
        }

        public HeaderExtract(string status, string language, DateTime serverDate, string bankName)
        {
            Status = status;
            Language = language;
            ServerDate = serverDate;
            BankName = bankName;
        }



    }
}
