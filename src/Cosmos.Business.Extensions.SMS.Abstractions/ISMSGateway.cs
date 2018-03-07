using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.Business.Extensions.SMS.Abstractions {
    public interface ISMSGateway {
        string GetName();
        void Send(string to, ISMSMessage message, IAccountSettings accountSettings);
        void Send(List<string> to, ISMSMessage message, IAccountSettings accountSettings);
        Task SendAsync(string to, ISMSMessage message, IAccountSettings accountSettings);
        Task SendAsync(List<string> to, ISMSMessage message, IAccountSettings accountSettings);
    }
}