using Hangman.Official.Commands;
using Hangman.Official.Enums;
using Hangman.Official.Models;
using Hangman.Official.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;



namespace Hangman.Official.ViewModels
{

 
    public class HangmanGameViewModel : BaseViewModel
    {


        private int Tries = 8;
        private int counter = 0;
        private string _Wins;
        private int timerStep = 1000;

        private int _TimeLeft;
        int timerMax = 61;
        private Timer _CountdownTimer;

        private String _Timer;

        public String Timer
        {
            get { return _Timer; }
            set { SetProperty(ref _Timer, value); }

        }


        public String Wins
        {
            get { return _Wins; }
            set { SetProperty(ref _Wins, value); }

        }


        private Stack<string> _cachedWords = new Stack<string>();
        public Stack<string> CachedWords
        {
            get
            {
                if (_cachedWords.Count < 1)
                {
                    PopulateCache();
                }

                return _cachedWords;
            }
        }

        private void PopulateCache()
        {
            foreach (var word in RepositoryContainer.Words.GetRandomSet(50))
            {
                _cachedWords.Push(word.ToUpper()); //get 50 random words from file
            }
        }

        private HangmanRoundManager RoundManager { get; set; } = new HangmanRoundManager();
        private ImageSetEnumerator _imageSetProgresser = new ImageSetEnumerator();

        public ObservableCollection<LetterViewModel> LettersCollection { get; set; }

        private string _maskedWord;
        public string MaskedWord
        {
            get { return _maskedWord; }
            set
            {
                _maskedWord = value;
                NotifyPropertyChanged(this, nameof(MaskedWord));
            }
        }

        private BitmapSource _progressImage;
        public BitmapSource ProgressImage
        {
            get
            {
                return _progressImage;
            }
            set
            {
                _progressImage = value;
                NotifyPropertyChanged(this, nameof(ProgressImage));
            }
        }

        public ICommand GuessLetterCommand { get; set; }
        public ICommand NewRoundCommand { get; set; }


        public HangmanGameViewModel()
        {
            GuessLetterCommand = new ActionCommand<char>(GuessLetter);
            NewRoundCommand = new ActionCommand(StartNewRound);

            if (Globals.openGame == true)
            {
                Tries = Globals.tries;
                _Wins = "Wins: " + Globals.wins;
                counter = Globals.wins;

            }
            else
                _Wins = "Wins: " + counter;


            InitializeLettersCollection();
            StartNewRound();


        }


        // when you guess or not a letter function
        private void GuessLetter(char character)
        {
            if (RoundManager.MakeGuess(character))
            {
                LettersCollection.Single((x) => x.Letter == character).UpdateState(LetterState.Correct);

                UpdateMaskedWord(character);
            }
            else
            {
                LettersCollection.Single((x) => x.Letter == character).UpdateState(LetterState.Wrong);
                Globals.wrongLetters+=character.ToString();
        

                SetNextImageInSet();
            }

            CheckWinOrLoss();
        }

        // New round function
        private void StartNewRound()
        {
            InitializeProgressImages();

            if (Globals.openGame == true)
            {
                StartTimer(Globals.timer);

            }
            else
                StartTimer(timerMax);
            if (Globals.openGame == true)
            {
                foreach (var lettervm in LettersCollection)
                {
                    if (Globals.wrongLetters.Contains(lettervm.Letter))
                    { 
                        lettervm.UpdateState(LetterState.Wrong);
                        SetNextImageInSet();
                    }
                    else
                    if (Globals.wordMasked.Contains(lettervm.Letter))
                        lettervm.UpdateState(LetterState.Correct);

                    else
                        lettervm.UpdateState(LetterState.NoGuess);
                }

            }
            else
                foreach (var lettervm in LettersCollection)
                {

                    lettervm.UpdateState(LetterState.NoGuess);
                }

            if (Globals.openGame == true)
                RoundManager.Start(Globals.wordToGuess, Tries);
            else
            { 
                RoundManager.Start(CachedWords.Pop(), Tries);
                Globals.wordToGuess = RoundManager.WordToGuess;
            }



            Globals.wrongLetters = string.Empty;

         
            InitializeMaskedWord();
           
        }

        // initialize letters
        private void InitializeLettersCollection()
        {
            LettersCollection = new ObservableCollection<LetterViewModel>();

            var letters = Application.Current.FindResource("Letters");

            foreach (var letter in letters as string[])
            {
                LettersCollection.Add(new LetterViewModel(char.Parse(letter)));
            }
        }

        // initialize images
        private void InitializeProgressImages()
        {
            _imageSetProgresser.Reset();

            _imageSetProgresser.InitializeCollection(ImageDataTransformHelper.CreateImageCollectionFromData(SettingsContainer.HangmanOptions.SelectedImageSetData));


            SetNextImageInSet();
        }

        // generate masked word
        private void InitializeMaskedWord()
        {
            if (Globals.openGame == true)
            {
              
                MaskedWord = Globals.wordMasked;

            }
            else
            {
                
                var sb = new StringBuilder();
                if (RoundManager.WordToGuess.Contains(" "))
                {

                    var index = RoundManager.WordToGuess.Split(' ');

                    for (int i = 0; i < index.Length; i++)
                    {
                        for (int j = 0; j < index[i].Length; j++)
                        {
                            sb.Append("-");
                        }
                        sb.Append(" ");
                    }

                }

                else
                {
                    for (int i = 0; i < RoundManager.WordToGuess.Length; i++)
                    {

                        sb.Append("-");
                    }
                }
                MaskedWord = sb.ToString();
            }
        }

        // Update masked word
        private void UpdateMaskedWord(char chartoinsert)
        {
            var sb = new StringBuilder(MaskedWord);

            foreach (var index in FindAllIndexesOf(RoundManager.WordToGuess, (chartoinsert)))
            {


                sb.Remove(index, 1);
                sb.Insert(index, chartoinsert.ToString());
            }

            MaskedWord = sb.ToString();
            Globals.wordMasked = MaskedWord;


        }


        // move next image when you get a wrong letter
        private void SetNextImageInSet()
        {
            _imageSetProgresser.MoveNext();
            ProgressImage = _imageSetProgresser.Current as BitmapSource;
        }

        private void CheckWinOrLoss()
        {
            if (CheckWinCondition())
            {
                OnRoundWon();
            }

            if (CheckLoseCondition())
            {
                OnRoundLost();
            }
        }

        //Win condition
        private bool CheckWinCondition()
        {


            if (RoundManager.WordToGuess.Contains(" "))
            {
                if (MaskedWord.Remove(MaskedWord.Length - 1) == RoundManager.WordToGuess)
                {
                    return true;
                }

                return false;
            }
            else
            {
                if (MaskedWord == RoundManager.WordToGuess)
                {
                    return true;
                }

                return false;

            }
        }

        // Lose condition
        private bool CheckLoseCondition()
        {
            if (RoundManager.TriesLeft < 1)
            {
                return true;
            }

            return false;
        }

        //When you win
        private void OnRoundWon()
        {
            if (Globals.openGame == true)
            {
                Tries = 8;
                Globals.openGame = false;
            }

            counter++;
            Wins = "Wins: " + counter;
            Globals.wins = counter;
            if (counter == 5)
            {
                Dialog dialog = new Dialog();
                dialog.Show();
               
                string createText = Player.UserName + " Win " + Globals.category + Environment.NewLine;

                counter = 0;
                Wins = "Wins: " + counter;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("Statisticsy.txt", true))
                {
                    file.Write(createText);
                }
               
            }



            _CountdownTimer.Stop();
            _CountdownTimer.Enabled = false;

            StartNewRound();
        }

        // When you lost
        private void OnRoundLost()
        {
            if (Globals.openGame == true)
            {
                Tries = 8;
                Globals.openGame = false;
            }

        

            MessageBox.Show("Round lost :(");
            Wins = "Wins: " + 0;
         
            string createText = Player.UserName + " Lose " + Globals.category + Environment.NewLine;

            counter = 0;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Statisticsy.txt", true))
            {
                file.Write(createText);
            }


            _CountdownTimer.Stop();
            _CountdownTimer.Enabled = false;


            StartNewRound();
        }

   

        #region Helpers

        private int[] FindAllIndexesOf(string str, char character)
        {
            var res = new List<int>();

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == character)
                {
                    res.Add(i);
                }
            }

            return res.ToArray();
        }

        #endregion

        // Time settings
        public void StartTimer(int timer)
        {
            _TimeLeft = timer;

           

            _CountdownTimer = new Timer(timerStep);
            _CountdownTimer.Elapsed += OnTimerTick;

            _CountdownTimer.Enabled = true;
        }

        private void OnTimerTick(Object source, ElapsedEventArgs e)
        {
            if (_TimeLeft > 0)
            {
                _TimeLeft--;
                Timer = "TIME LEFT: " + _TimeLeft;
                Globals.timer = _TimeLeft;
            }
            else

            {
                
                _CountdownTimer.Stop();
                _CountdownTimer.Enabled = false;
                OnRoundLost();


            }




        }

    }
}
