using System;

namespace Cosmos.Business.Extensions.SMS.Core.RestrictContracts
{
    public class MustHaveTemplateCodeAttribute : Attribute
    {
        public string TemplateCode { get; }

        public MustHaveTemplateCodeAttribute(string templateCode)
        {
            if (string.IsNullOrWhiteSpace(templateCode)) throw new ArgumentNullException(nameof(templateCode));
            TemplateCode = templateCode;
        }
    }
}