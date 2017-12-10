using CommonDataItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CredentialSignalRGameServer
{
    public static class GameDataObjects
    {
        static public List<PlayerData> RegisteredPlayers = new List<PlayerData>()
            {
                new PlayerData {
                playerID = Guid.NewGuid().ToString(),
                imageName = "Player 1",
                GamerTag ="BAZOOKATOOTH",  Password = "password",
                PlayerName= "NOEL", Coins = 0},

                new PlayerData {
                playerID = Guid.NewGuid().ToString(),
                imageName = "Player 2",
                GamerTag ="WENGU",  Password = "password",
                PlayerName= "TOMMY", Coins = 0},


                new PlayerData {
                playerID = Guid.NewGuid().ToString(),
                imageName = "Player 3",
                GamerTag ="CRUSH",  Password = "password",
                PlayerName= "ROB", Coins = 0},


                new PlayerData {
                playerID = Guid.NewGuid().ToString(),
                imageName = "Player 4",
                GamerTag ="HAWKE",  Password = "password",
                PlayerName= "PETER", Coins = 0},

            };

    }
}