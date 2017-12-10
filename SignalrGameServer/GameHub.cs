using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using CommonDataItems;
using CredentialSignalRGameServer;

namespace SignalrGameServer
{

    public class GameHub : Hub
    {

        #region Game hub variables
        // Use static to protect Data across dofferent hub invocations
        public static Queue<PlayerData> RegisteredPlayers = new Queue<PlayerData>( new PlayerData[]
        {
            new PlayerData { GamerTag = "BAZOOKA", imageName = "", playerID = Guid.NewGuid().ToString(), Coins = 0 },
            new PlayerData { GamerTag = "WENGU", imageName = "", playerID = Guid.NewGuid().ToString(), Coins = 0 },
            new PlayerData { GamerTag = "CRUSH", imageName = "", playerID = Guid.NewGuid().ToString(), Coins = 0 },
            new PlayerData { GamerTag = "HAWKE", imageName = "", playerID = Guid.NewGuid().ToString(), Coins = 0 },
        });

        public static List<PlayerData> Players = new List<PlayerData>();

        public static Stack<string> characters = new Stack<string>(
                    new string[] { "Player 4", "Player 3", "Player 2", "Player 1" });

        #endregion

        public void Hello()
        {
            Clients.All.hello();
        }


        public PlayerData checkCredentials(string name, string password)
        {
            return GameDataObjects.RegisteredPlayers
                                  .FirstOrDefault(p => p.PlayerName == name.ToLower()
                                   && p.Password == password.ToLower());


        }

        public PlayerData Join()
        {
            // Check and if the charcters
            if (characters.Count > 0)
            {
                // pop name
                string character = characters.Pop();
                // if there is a registered player
                if (RegisteredPlayers.Count > 0)
                {
                    PlayerData newPlayer = RegisteredPlayers.Dequeue();
                    newPlayer.imageName = character;
                    newPlayer.playerPosition = new Position { X = new Random().Next(700),
                                                            Y = new Random().Next(500) };
                    // Tell all the other clients that this player has Joined
                    Clients.Others.Joined(newPlayer);
                    // Tell this client about all the other current 
                    Clients.Caller.CurrentPlayers(Players);
                    // Finaly add the new player on teh server
                    Players.Add(newPlayer);
                    return newPlayer;
                }
               
                
            }
            return null;
        }

        
        public void Moved(string playerID, Position newPosition)
        {
            // Update the collection with the new player position is the player exists
            PlayerData found = Players.FirstOrDefault(p => p.playerID == playerID);

            if (found != null)
            {
                // Update the server player position
                found.playerPosition = newPosition;
                // Tell all the other clients this player has moved
                Clients.Others.OtherMove(playerID,newPosition);
            }
        }

    }
}