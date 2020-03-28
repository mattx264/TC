using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace TC.WebService.Helpers
{
    public class FileHelper
    {
        public void Save()
        {

        }
        public void SaveImageAndCreateThumbnail(IFormFile ProductImage)
        {
            // OVER HERE WE CAN DEFIINDE PIPE
            Stream stream = ProductImage.OpenReadStream();

            Image newImage = GetReducedImage(32, 32, stream);
            newImage.Save("path+filename");

        }

        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                Image image = Image.FromStream(resourceImage);
                Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
