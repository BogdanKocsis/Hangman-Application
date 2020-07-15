using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Hangman.Official.Models;
using System.Collections.ObjectModel;
using System.Xml;

namespace Hangman.Official
{
    class SaveXml
    {

        public static void SaveData(ObservableCollection<Player> players, string filename)
        {
            XmlSerializer writer = new XmlSerializer(typeof(ObservableCollection<Player>)); ;
            FileStream file = File.OpenWrite(filename);
            writer.Serialize(file, players);
            file.Close();

        }

        public static ObservableCollection<Player> ReadPlayers()
        {
            ObservableCollection<Player> players = new ObservableCollection<Player>();
            XmlSerializer reader = new XmlSerializer(typeof(ObservableCollection<Player>));
            StreamReader file = new StreamReader("xmlfile.xml");
            players = (ObservableCollection<Player>)reader.Deserialize(file);
            file.Close();
            return players;
        }



        public static void DeleteData(string playerName)
        {

            int index = 0;
            for (var i = 0; i < Player.players.Count; i++)
                if (Player.players[i].Name == playerName)
                    index = i;

            Player.players.Remove(Player.players[index]);

            System.IO.File.WriteAllText("xmlfile.xml", string.Empty);
            SaveData(Player.players, "xmlfile.xml");


            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines("Statisticsy.txt").Where(l => !l.StartsWith(Player.UserName));

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete("Statisticsy.txt");
            File.Move(tempFile, "Statisticsy.txt");

            if (File.Exists(Player.UserName + ".txt"))
                File.Delete(Player.UserName + ".txt");

        }

        

        
    }
}
