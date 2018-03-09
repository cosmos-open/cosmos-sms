namespace Cosmos.Business.Extensions.SMS.TianyiWuxian.Exceptions {
    public class TianyiWuxianSmsException : TianyiWuxianException {
        public TianyiWuxianSmsException(string message) : base(message, 1L, 401) { }
    }
}