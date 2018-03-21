namespace Cosmos.Business.Extensions.SMS.Exceptions {
    public class SmsAggregationExceptionFactory: ISmsAggregationExceptionFactory {
        public SmsAggregationException Create() {
            return new SmsAggregationException();
        }
    }
}