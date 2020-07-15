using System.Collections.Generic;

namespace Hangman.Official.Interfaces
{
    //Inteface for words and getting a set of words
    public interface IWordRepository
    {
        IEnumerable<string> GetRandomSet(int size);
    }
}
