using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.WebService.Helpers;
using TC.WebService.Models;
using Xunit;

namespace TC.WebServiceIntegrationTest.Helpers
{
    public class SerializationTests
    {
        [Fact]
        public void ToByteArray_Can()
        {
            var szwagierModels = new List<SzwagierModel>()
            {
                new SzwagierModel()
                {
                    ConnectionId=Guid.NewGuid().ToString(),
                    Location="test location Mars",
                    Name="my name",
                    SzwagierType =SzwagierType.SzwagierBrowserExtension,
                    UserId = Guid.NewGuid().ToString()
                }
            };
            var exceptions = Record.Exception(() => szwagierModels.ToByteArray());
            Assert.Null(exceptions);
        }
    }
}
