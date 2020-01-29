using TC.WebService.Helpers;
using Xunit;

namespace TC.WebServiceTest.Helpers
{
    public class UtilHelperTest
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("google", null)]        
        [InlineData("google.com", "google.com")]
        [InlineData("http://google.com", "google.com")]
        [InlineData("http://www.google.com", "google.com")]
        [InlineData("https://www.google.com/search?Q4dUDCAg&uact=5", "google.com")]
        public void GetDomainTest1(string domainInput, string result)
        {
            var util = new UtilHelper();
            var domain = util.GetDomain(domainInput);
            Assert.Equal(domain, result);
        }
    }
}
