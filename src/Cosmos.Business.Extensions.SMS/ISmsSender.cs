namespace Cosmos.Business.Extensions.SMS {
    public interface ISmsSender {
        void SendMessage();
        void SendCode();
    }
}