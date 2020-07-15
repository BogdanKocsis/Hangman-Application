using Hangman.Official.Interfaces;

namespace Hangman.Official.Models
{
    public static class SettingsContainer
    {
        private static IHangmanImagesSettings _hangmanOptions;
        public static IHangmanImagesSettings HangmanOptions
        {
            get
            {
                if (_hangmanOptions == null)
                {
                    HangmanOptions = new HangmanOptionsSettings();
                  
                }

                return _hangmanOptions;
            }
            set
            {
                _hangmanOptions = value;
            }
        }



        /// <summary>
        /// Saves all settings
        /// </summary>
    
    }
}
