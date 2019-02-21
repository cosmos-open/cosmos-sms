using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events;

namespace Cosmos.Business.Extensions.SMS
{
    public interface ISmsSender
    {
        void SendMessage(string phoneNumber, string message, SmsSendEventMoreInfo moreInfo = null);
        void SendMessage(List<string> phoneNumbers, string message, SmsSendEventMoreInfo moreInfo = null);
        void SendCode(string phoneNumber, string code, SmsSendEventMoreInfo moreInfo = null);
        void SendCode(List<string> phoneNumbers, string code, SmsSendEventMoreInfo moreInfo = null);
        void SendTemplateMessage(string phoneNumber, string templateCode, SmsSendEventMoreInfo moreInfo = null);
        void SendTemplateMessage(List<string> phoneNumbers, string templateCode, SmsSendEventMoreInfo moreInfo = null);
    }
}