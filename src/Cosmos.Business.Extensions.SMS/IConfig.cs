namespace Cosmos.Business.Extensions.SMS
{
    public interface IConfig { }

    public interface IConfig<TAccount> : IConfig
    {
        TAccount Account { get; set; }
    }
}