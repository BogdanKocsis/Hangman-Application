using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Official.Models
{
    public static class Globals
    {

        public static String category="All";

        public static int timer ;
        public static int wins;
        public static int tries;
        public static string wordMasked;
        public static string wordToGuess;
        public static string wrongLetters;
        public static bool openGame=false;

    }
}
