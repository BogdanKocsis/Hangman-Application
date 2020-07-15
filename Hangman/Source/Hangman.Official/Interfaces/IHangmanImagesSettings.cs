using System.Collections.Generic;
using Hangman.Official.Enums;

namespace Hangman.Official.Interfaces
{
    //Inteface for Images Settings
    public interface IHangmanImagesSettings
    {
       
        List<byte[]> SelectedImageSetData { get; set; }
    }
}