using Hangman.Official.Interfaces;
using Hangman.Official.Repositories;

namespace Hangman.Official.Models
{
    public static class RepositoryContainer
    {
        public static IWordRepository Words { get; }
        public static IImageSetRepository ImageSets { get; }


        static RepositoryContainer()
        {
            Words = new WordRepository(); //getting words
            ImageSets = new ImageSetRepository(); //getting images
           
        }
    }
}
