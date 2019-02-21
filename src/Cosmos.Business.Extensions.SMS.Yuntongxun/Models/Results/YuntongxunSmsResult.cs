namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Models.Results
{
    public class YuntongxunSmsResult
    {
        public string statusCode { get; set; }

        public string statusMsg { get; set; }

        public TemplateSMS TemplateSMS { get; set; }
    }
}