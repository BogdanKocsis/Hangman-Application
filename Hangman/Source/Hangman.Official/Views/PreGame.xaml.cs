using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hangman.Official.Models;
using Hangman.Official.ViewModels;


namespace Hangman.Official.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PreGame : Window
    {
      
        public PreGame()
        {
            InitializeComponent();
      
        
        }
        //Minimize function
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;

        }
        //Close app function
        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        //Show student info
        private void Show_System_Info(object sender, RoutedEventArgs e)
        {


            String author = "Author: Kocsis Bogdan";
            String group = "Group: 10LF382";
            String specialization = "Specialization: Informatica Aplicata";


            AboutDialogBox.Inlines.Clear();
            AboutDialogBox.Inlines.Add(author + "\n" + group + "\n" + specialization + "\n" + "GitHub: ");
            Hyperlink hyperlink = new Hyperlink()
            {
                NavigateUri = new Uri("https://github.com/BogdanKocsis")
            };

            hyperlink.Inlines.Add("https://github.com/BogdanKocsis");
            hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            AboutDialogBox.Inlines.Add(hyperlink);


        }
        //go to sign up page
        private void GoHome(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow home = new MainWindow();
            home.Show();
        }

        // combobox with categories load
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> data = new List<string>();
            data.Add("All");
            data.Add("Series");
            data.Add("Movies");
            data.Add("Songs");
            data.Add("Artists");
            data.Add("Actors");
          

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;

           
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
                var selectedItem = sender as ComboBox;
                Globals.category = selectedItem.SelectedItem as String;
            
        }

        // go to statistics page
        private void Statistics_Button_Click(object sender, RoutedEventArgs e)
        {
            Statistics page = new Statistics();
            page.Show();

        }

        // load from file the settings from last game saved
        private void Button_OpenGame(object sender, RoutedEventArgs e)
        {
        
            if (File.Exists(Player.UserName + ".txt"))
            {
                Globals.openGame = true;
                string[] lines = System.IO.File.ReadAllLines(Player.UserName + ".txt");
                Globals.category = lines[1];
                Globals.timer = Int32.Parse(lines[2]);
                Globals.wins = Int32.Parse(lines[3]);
                Globals.tries = Int32.Parse(lines[4]);
                Globals.wordMasked = lines[5];
                Globals.wordToGuess = lines[6];
                Globals.wrongLetters = lines[7];
            }
        }
    }
}
