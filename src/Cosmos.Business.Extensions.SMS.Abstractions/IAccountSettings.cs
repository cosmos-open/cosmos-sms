namespace Cosmos.Business.Extensions.SMS.Abstractions {
    public interface IAccountSettings {
        string User { get; set; }
        string Key { get; set; }
    }
}