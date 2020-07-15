using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Hangman.Official.Views
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();
            List<ProcessInfo> players = new List<ProcessInfo>();
            players = GetPlayersInfo();
            listView.ItemsSource = players;



        }

        public class ProcessInfo
        {
            public string Name { get; set; }
            public int Loses { get; set; }
            public int Series { get; set; }
            public int Movies { get; set; }
            public int Songs { get; set; }
            public int Artists { get; set; }
            public int Actors { get; set; }
            public int All { get; set; }


        }

        public class PreProcess
        {
            public string Name { get; set; }
            public string Win_Lose { get; set; }
            public string Category { get; set; }
        }

        //Get statitistics from file
        private List<ProcessInfo> GetPlayersInfo()
        {

            List<PreProcess> processes = new List<PreProcess>();
            List<ProcessInfo> statistics = new List<ProcessInfo>();
            string line;
            using (var reader = File.OpenText("Statisticsy.txt"))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    PreProcess newInfo = new PreProcess();
                    string[] words = line.Split(' ');

                    newInfo.Name = words[0];
                    newInfo.Win_Lose = words[1];
                    newInfo.Category = words[2];
                    processes.Add(newInfo);
                }
            }

            foreach (PreProcess info in processes)
            {
                if (!statistics.Any(i => i.Name == info.Name))

                {
                    ProcessInfo item = new ProcessInfo();
                    item.Name = info.Name;
                    item.Loses = 0;
                    item.Movies = 0;
                    item.Series = 0;
                    item.Songs = 0;
                    item.Artists = 0;
                    item.Actors = 0;
                    item.All = 0;
                    statistics.Add(item);
                }
            }
                foreach (PreProcess info2 in processes)
                {
                    ProcessInfo playerInfo = new ProcessInfo();
                    playerInfo = statistics.Find(i => i.Name == info2.Name);

                    if (info2.Win_Lose == "Lose")
                        playerInfo.Loses++;
                    else
                    {
                        switch (info2.Category)
                        {
                            case "Movies":
                                playerInfo.Movies++;
                                break;
                            case "Series":
                                playerInfo.Series++;
                                break;
                            case "Songs":
                                playerInfo.Songs++;
                                break;
                            case "Artists":
                                playerInfo.Artists++;
                                break;
                            case "Actors":
                                playerInfo.Actors++;
                                break;
                            case "All":
                                playerInfo.All++;
                                break;

                        }
                    }
                }


            return statistics;
        }


        //Go to SignUp page function 
        private void GoHome(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow home = new MainWindow();
            home.Show();
        }

       // Close button function
        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
    }
}

