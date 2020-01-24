using System;


namespace TC.WebService.Helpers
{
    public interface IUtilHelper
    {
        public string GetDomain(string domainInput);
    }
    public class UtilHelper : IUtilHelper
    {
        public string GetDomain(string domainInput)
        {
            Uri myUri = new Uri(domainInput);
            string domain = myUri.Host.Replace("www.", String.Empty);
            return string.IsNullOrEmpty(domain) ? null : domain;

        }
    }
}
