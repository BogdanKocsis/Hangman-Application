using Hangman.Official.Interfaces;
using Hangman.Official.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;


namespace Hangman.Official.Repositories
{
    public class WordRepository : IWordRepository
    {
        

        public IEnumerable<string> GetRandomSet(int size)
        {
            var words = new List<string>();

            try
            {   // Open the text file using a stream reader.
                String line="";
         
                using (StreamReader sr = new StreamReader("../../Words/" + Globals.category+ ".txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    while ((line = sr.ReadLine()) != null)
                    {
                     
                        words.Add(line.ToLower());
                    }
                
                }
            }
            catch (IOException e)
            {
     
                MessageBox.Show("The file could not be read:" + e.Message);
            }

            Random r = new Random();

            words = words.OrderBy(x => r.Next()).ToArray().ToList();

            return words;
        }
    }
}
