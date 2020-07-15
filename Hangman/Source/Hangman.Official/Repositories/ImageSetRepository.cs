using Hangman.Official.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hangman.Official.Repositories
{
    public class ImageSetRepository : IImageSetRepository
    {
        
        private static readonly IList<string> _images = new[]
        {
            "S0", "S1", "Image2", "Image3", "Image4",
            "Image5", "Image6", "Image7", "Image8"
        };


       
     


        public IEnumerable<byte[]> GetAll()
        {
            var imagesetsdata = new List<byte[]>();

            Image[] images = new Image[12];
            images[0].Source= new BitmapImage(new Uri("C:/HangmanWPF-master/Source/Hangman.Official/Images/S0.png"));

            byte[] imageBytes = File.ReadAllBytes("C:/HangmanWPF-master/Source/Hangman.Official/Images/S0.png");

            return imagesetsdata;
        }

        private static void LoadImages(SQLiteDataReader reader, List<List<byte[]>> imagesetsdata)
        {
            while (reader.Read())
            {
                var current = new List<byte[]>();

                foreach (var image in _images)
                {
                    current.Add((byte[])reader[image]);
                }

                imagesetsdata.Add(current);
            }
        }

      


    }
}
