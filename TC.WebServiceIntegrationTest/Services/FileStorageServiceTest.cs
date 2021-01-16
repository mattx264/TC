using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TC.WebService.Services.Files;
using TC.WebServiceTest.TestUtil;
using Xunit;

namespace TC.WebServiceTest.Services
{
    public class FileStorageServiceTest
    {
        //FIX THIS TEST !!!!!!!
        //[Fact]
        //public async System.Threading.Tasks.Task StoreImageTestAsync()
        //{
        //    // THIS TEST IS ONLY WORKING IF YOU HAVE SETUP AZURE STORAGE LOCAL
        //    var _configurationRoot = new Mock<IConfigurationRoot>();
          
        //    _configurationRoot.SetupGet(x => x["Azure:StorageConnectionString"]).Returns("UseDevelopmentStorage=true");
        //    var fileStorageService = new FileStorageService(_configurationRoot.Object);
        //    var bytes = ImageUtil.LoadImage("test-img-1.jpeg");

        //    var filePath = await fileStorageService.StoreFileAsync("test.jpg", bytes);
        //    Assert.NotNull(filePath);
         
        //}

    }
}
