using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Hangman.Official.Models;
using Hangman.Official.Views;

namespace Hangman.Official.Views
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {

            InitializeComponent();

            DeletePlayer.IsEnabled = false;
            PlayGame.IsEnabled = false;

            Player.players = new ObservableCollection<Player>();

            Player.players = SaveXml.ReadPlayers();
            if (Player.players.Count > 0)
                ListViewProducts.ItemsSource = Player.players;


        }

        //minimeze function
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;

        }
        //close app function
        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //go to pregame page
        private void OpenGame(object sender, RoutedEventArgs e)
        {
            PreGame game = new PreGame();

            this.Visibility = Visibility.Hidden;
            game.Show();

        }

        //get user name function

        private void GetUserName(object sender, RoutedEventArgs e)
        {

            DeletePlayer.IsEnabled = true;
            PlayGame.IsEnabled = true;
            Player item = (Player)(sender as Button).DataContext;
            Player.UserName = item.Name;
            Player.ImagePath = item.Image;

        }

        //open sign up form to register new player
        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            SignUp signupForm = new SignUp();
            signupForm.Show();

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        //show student info
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
        //delete player function
        private void DeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            SaveXml.DeleteData(Player.UserName);

        }
    }

}
