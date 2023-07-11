namespace SmsPanel
{
    public class LoginOutputModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DataModel Data { get; set; }
    }

    public class DataModel
    {
        public string Amount { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
    }
}