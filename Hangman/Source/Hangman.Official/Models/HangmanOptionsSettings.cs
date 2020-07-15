using System;
using System.Collections.Generic;
using Hangman.Official.Enums;
using Hangman.Official.Interfaces;

namespace Hangman.Official.Models
{
    public class HangmanOptionsSettings : IHangmanImagesSettings
    {
      
       
        public List<byte[]> SelectedImageSetData { get; set; }

    

       

        private List<byte[]> GetImagesFromSettings()
        {
            return Properties.Settings.Default.SelectedImageSet;
        }
    }
}
