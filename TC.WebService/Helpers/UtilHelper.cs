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
            if(String.IsNullOrWhiteSpace(domainInput) || domainInput.IndexOf('.') == -1)
            {
                return null;
            }
            UriBuilder myUri = new UriBuilder(domainInput);
            string domain = myUri.Host.Replace("www.", String.Empty);
            return string.IsNullOrEmpty(domain) ? null : domain;

        }
    }
}
