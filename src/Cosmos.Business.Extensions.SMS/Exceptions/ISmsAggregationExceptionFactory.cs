namespace Cosmos.Business.Extensions.SMS.Exceptions
{
    public interface ISmsAggregationExceptionFactory
    {
        SmsAggregationException Create();
    }
}