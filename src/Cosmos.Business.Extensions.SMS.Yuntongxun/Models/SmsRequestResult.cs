namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Models
{
    public enum SmsRequestResult
    {
        Success = 0,
        EmptyMainAccountId = 172006,
        EmptyMainAccountToken = 172007,
        EmptySubAccountId = 172008,
        EmptySubAccountToken = 172009,
        EmptyVoipAccount = 1720010,
        EmptyVoipPassword = 172011,
        WrongPhoneNumber = 172002,
        InvalidRequestFormat = 112616,
        ServerError = 172003
    }
}