using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace TC.WebServiceTest.TestUtil
{
    public static class ImageUtil
    {
        public static byte[] LoadImage(string imgName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", imgName);
       
            Image img = Image.FromFile(path);
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            return arr;
        }
    }
}
