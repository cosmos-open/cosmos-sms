namespace Cosmos.Business.Extensions.SMS.Luosimao.Exceptions {
    public class LuosimaoSmsException : LuosimaoException {
        public LuosimaoSmsException(string message) : base(message, 1L, 401) { }
    }
}