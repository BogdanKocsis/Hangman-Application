using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hangman.Official.Models
{
    public static class ImageDataTransformHelper
    {

        public static List<ImageSource> CreateImageCollectionFromData(IEnumerable<byte[]> dataset)
        {
            var imagearray = new List<ImageSource>();

           
            
        //    IList<string> _imagesNames = new[]
        //{
        //    "HangmanState_0","S0", "S1", "S3", "S4", "S5",
        //    "S6", "S7", "Swin"
        //};
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\..\Images\";
            string filter = "*.png";
            string[] files = Directory.GetFiles(folder, filter);

            foreach (var data in files)
            {


                byte[] imageBytes = File.ReadAllBytes(data);
                imagearray.Add(ConvertDataToImageSource(imageBytes));
            }

            return imagearray;
        }

        public static ImageSource ConvertDataToImageSource(byte[] imagedata)
        {
            return (ImageSource)new ImageSourceConverter().ConvertFrom(imagedata);
        }

     
    }
}
