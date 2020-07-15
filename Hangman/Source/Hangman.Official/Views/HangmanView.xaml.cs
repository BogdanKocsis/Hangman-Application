using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Hangman.Official.Models;


namespace Hangman.Official.Views
{
    public partial class HangmanView : UserControl
    {
        public HangmanView()
        {
            InitializeComponent();
            
            textblockUserName.Text = "Player: " + Player.UserName;

            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(Player.ImagePath, UriKind.RelativeOrAbsolute);
            bimage.EndInit();
            

            ProfilePicture.Source = bimage;
          
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }

        //If a key is pressed.. Search for the element (Letter button) with the same character bound to it and if clickable (IsHitTestVisible), execute its command
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
     
            for (int i = 0; i < LettersPanel.Items.Count; i++)
            {

                var obj = LettersPanel.ItemContainerGenerator.ContainerFromIndex(i);
                int childrencount = VisualTreeHelper.GetChildrenCount(obj);

                for (int x = 0; x < childrencount; x++)
                {
                    var o = VisualTreeHelper.GetChild(obj, x);
                    Button b = o as Button;

                    if (b.IsHitTestVisible)
                    {
                        char para = (char)b.CommandParameter;

                        if (para.ToString().ToUpper() == e.Key.ToString().ToUpper())
                        {
                            b.Command.Execute(b.CommandParameter);
                            return;
                        }
                    }
                }
            }
        }

        // Save the settings of the game when the save game button is pressed
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string path = Player.UserName + ".txt";

            if (File.Exists(path))
            {
                string text = Player.UserName + "\n" + Globals.category + "\n" + Globals.timer + "\n" + Globals.wins + "\n" + Globals.tries + "\n" + Globals.wordMasked + "\n" + Globals.wordToGuess + "\n" + Globals.wrongLetters;

                System.IO.File.WriteAllText(path, text);

            }
            else
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(Player.UserName);
                    sw.WriteLine(Globals.category);
                    sw.WriteLine(Globals.timer);
                    sw.WriteLine(Globals.wins);
                    sw.WriteLine(Globals.tries);
                    sw.WriteLine(Globals.wordMasked);
                    sw.WriteLine(Globals.wordToGuess);
                    sw.Write(Globals.wrongLetters);
                }
            }
        }
    }
}
