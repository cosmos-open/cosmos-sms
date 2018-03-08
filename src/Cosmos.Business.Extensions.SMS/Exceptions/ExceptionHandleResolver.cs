using System;

namespace Cosmos.Business.Extensions.SMS.Exceptions {
    public static class ExceptionHandleResolver {
        private static Action<Exception> _exceptionHandle { get; set; }

        public static Action<Exception> ResolveHandler() => _exceptionHandle;

        public static void SetHandler(Action<Exception> handle) => _exceptionHandle += handle;

        public static void Reset() => _exceptionHandle = null;
    }
}