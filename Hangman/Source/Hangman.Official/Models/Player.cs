using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Official.Commands;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Hangman.Official.Models
{

    public class Player : ObservableObject
    {
        
        public static ObservableCollection<Player> players { get; set; }
     
        public string Name { get; set; }

        public string Image { get; set; }

        public static string UserName = "";
        public static string ImagePath = "";
        public  string textdata { get; set; }

       


      public Player()
        {

        }
    }


}
