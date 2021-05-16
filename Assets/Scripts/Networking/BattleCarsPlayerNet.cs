using UnityEngine;
using Mirror;
using BattleCars.Networking;
using BattleCars.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace BattleCars
{
    [RequireComponent(typeof(PlayerMotor))]
    public class BattleCarsPlayerNet : NetworkBehaviour
    {
        //syncVar allow variable to be shared over the network
        //must be changed on the server
        //can only be on Network behaviour
        [SyncVar]
        public byte playerId;
        [SyncVar]
        //username is public to allow it to sink
        public string username = "";

        private Lobby lobby;

        private bool hasJoinedLobby = false;


        private void Start()
        {
            if (isLocalPlayer)
            {
                PlayerMotor playerMotor = gameObject.GetComponent<PlayerMotor>();
                playerMotor.Setup();
            }
        }

        public void SetUsername(string _name)
        {
            if(isLocalPlayer)
            {
                CmdSetUsername(_name);
            }
        }

        public void AssignPlayerToSlot(bool _left, int _slotId, byte _playerId)
        {
            if(isLocalPlayer)
            {
                CmdAssignPlayerToLobbySlot(_left, _slotId, _playerId);
            }
        }

        #region Commands
        [Command]
        public void CmdSetUsername(string _name) => username = _name;
        [Command]
        public void CmdAssignPlayerToLobbySlot(bool _left, int _slotId, byte _playerId) => RpcAssignPlayerToLobbySlot(_left, _slotId, _playerId);
        #endregion

        #region RPCs
        [ClientRpc]
        public void RpcAssignPlayerToLobbySlot(bool _left, int _slotId, byte _playerId)
        {
            if(BattleCarsNetworkManager.Inastance.IsHost)
            {
                return;
            }

            StartCoroutine(AssignPlayerToLobbySlotDelay(BattleCarsNetworkManager.Inastance.GetPlayerForId(_playerId), _left, _slotId));
        }
        #endregion

        #region Coroutines
        private IEnumerator AssignPlayerToLobbySlotDelay(BattleCarsPlayerNet _player, bool _left, int _slotId)
        {
            Lobby lobby = FindObjectOfType<Lobby>();
            while (lobby == null)
            {
                yield return null;

                lobby = FindObjectOfType<Lobby>();
            }

            lobby.AssignPlayerToSlot(_player, _left, _slotId);
        }
        #endregion

        private void Update()
        { 
            if (BattleCarsNetworkManager.Inastance.IsHost)
            {
                if (lobby == null && !hasJoinedLobby)
                {
                    lobby = FindObjectOfType<Lobby>();
                }

                if (lobby != null && !hasJoinedLobby)
                {
                    hasJoinedLobby = true;
                    lobby.OnPlayerConnected(this);
                }
            }
        }

        public override void OnStartClient()
        {
            BattleCarsNetworkManager.Inastance.AddPlayer(this);
        }

        //runs only when the object is connected ts the local player
        public override void OnStartLocalPlayer()
        {
            SceneManager.LoadSceneAsync("InGameMenus", LoadSceneMode.Additive);
        }

        //runs when the client is disconnected from the server
        public override void OnStopClient()
        {
            BattleCarsNetworkManager.Inastance.RemovePlayer(playerId);
        }


    }
}