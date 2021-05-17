using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System.Linq;

namespace BattleCars.Networking
{
    public class BattleCarsNetworkManager : NetworkManager
    {
        //a reference to the battlecars version of the network manager
        public static BattleCarsNetworkManager Inastance => singleton as BattleCarsNetworkManager;

        //wheather or not this networkmanager is the host
        public bool IsHost { get; private set; } = false;

        public BattlecarsNetworkDiscovery discovery;

        //runs only when connecting to an online scene as a host
        public override void OnStartHost()
        {
            IsHost = true;

            discovery.AdvertiseServer();
        }

        private Dictionary<byte, BattleCarsPlayerNet> players = new Dictionary<byte, BattleCarsPlayerNet>();

        //attempts to return player correstponding to the passed id
        //if not player found, return null
        public BattleCarsPlayerNet GetPlayerForId(byte _playerId)
        {
            BattleCarsPlayerNet player;
            players.TryGetValue(_playerId, out player);
            return player;
        }

        public override void OnServerAddPlayer(NetworkConnection _connection)
        {
            //give us the next spawn position depending on the spawn mode
            Transform spawnPos = GetStartPosition();

            //spawn a player and try to use the spawnpos
            GameObject playerObj = spawnPos != null
                ? Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation)
                : Instantiate(playerPrefab);

            //Assign the id adn add them to the server based on the connection
            AssignPlayerId(playerObj);
            //associates connection to player object
            NetworkServer.AddPlayerForConnection(_connection, playerObj);
        }

        //removes the player with the coresponding ID
        public void RemovePlayer(byte _id)
        {
            //if the player is presant in the dictionacry
            if(players.ContainsKey(_id))
            {
                players.Remove(_id);
            }
        }

        public void AddPlayer(BattleCarsPlayerNet _player)
        {
            if(!players.ContainsKey(_player.playerId))
            {
                players.Add(_player.playerId, _player);
            }
        }

        //loop through dictoinary and find a free key ID
        protected void AssignPlayerId(GameObject _playerObj)
        {
            byte id = 0;

            //generate a list sorted by key values
            List<byte> playerIds = players.Keys.OrderBy(x => x).ToList();

            foreach(byte key in playerIds)
            {
                if(id == key)
                {
                    id++;
                }

                //get the playernet comenant from the gameobject and assign its player ID
                BattleCarsPlayerNet player = _playerObj.GetComponent<BattleCarsPlayerNet>();
                player.playerId = id;
                players.Add(id, player);
            }
        }
    }
}
