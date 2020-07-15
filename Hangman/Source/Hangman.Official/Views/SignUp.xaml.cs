using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.Win32;
using Hangman.Official.Models;


namespace Hangman.Official.Views
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        String ImageLocation = "";
        
        public SignUp()
        {
            InitializeComponent();
     

        }
        // Close form function
        private void CloseForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //upload image 
        private void UploadImage(object sender, RoutedEventArgs e)
        {
            

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";

                Nullable<bool> dialoOk = dialog.ShowDialog();
                if (dialoOk == true)
                {
                    ImageLocation = dialog.FileName; 

                }

                ImageProfile.Source = new BitmapImage(new Uri(ImageLocation));

               
                

            }
            catch (Exception)
            {

            }
        }
        //Save form function
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            Player player = new Player();
            player.Name = name.Text;
            player.Image = ImageLocation;
            Player.players.Add(player);
            SaveXml.SaveData(Player.players, "xmlfile.xml");
            
            this.Close();
        }
    }
}
