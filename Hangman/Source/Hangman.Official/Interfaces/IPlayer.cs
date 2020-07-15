using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Official.Models;

namespace Hangman.Official.Interfaces
{
    //Player Interface
    public interface IPlayer
    {

        IEnumerable<Player> GetPlayers();
        void Save(IEnumerable<Player> players);
  
    }
}
