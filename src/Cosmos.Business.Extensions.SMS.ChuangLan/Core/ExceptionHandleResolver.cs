using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core
{
    public static class ExceptionHandleResolver
    {
        private static Action<Exception> _exceptionHandle { get; set; }

        public static Action<Exception> ResolveHandler() => _exceptionHandle;

        public static void SetHandler(Action<Exception> handle) => _exceptionHandle += handle;

        public static void Reset() => _exceptionHandle = null;
    }
}
