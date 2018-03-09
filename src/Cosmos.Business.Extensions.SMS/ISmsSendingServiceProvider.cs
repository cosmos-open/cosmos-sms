namespace Cosmos.Business.Extensions.SMS {
    public interface ISmsSendingServiceProvider {
        ISmsSender GetSmsSender();
    }
}