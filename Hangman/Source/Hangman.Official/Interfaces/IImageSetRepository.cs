using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman.Official.Interfaces
{
    //Inteface for getting images 
    public interface IImageSetRepository
    {
   

       IEnumerable<byte[]> GetAll();
        

    }
}
