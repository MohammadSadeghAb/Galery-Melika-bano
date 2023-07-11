namespace SmsPanel.Models
{
    public class SendInputModel
    {
        public string Token { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}