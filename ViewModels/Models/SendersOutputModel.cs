using System.Collections.Generic;

namespace SmsPanel.Models
{
    public class SendersOutputModel
    {
        public int Status { get; set; }
        public IList<SenderModel> Senders { get; set; }
    }

    public class SenderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TariffModel Tariffs { get; set; }
        public int? Number { get; set; }
    }

    public class TariffModel
    {
        public double Normal { get; set; }
        public double Special { get; set; }
    }
}