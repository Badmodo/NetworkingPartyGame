using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BattleCars.Networking;

namespace BattleCars.UI
{
    public class LobbyPlayerSlot : MonoBehaviour
    {

        public bool IsTaken => player != null;
        public BattleCarsPlayerNet Player => player;
        public bool IsLeft { get; private set; } = false;

        [SerializeField] 
        TextMeshProUGUI nameDisplay;
        [SerializeField]
        private Button playerButton;

        private BattleCarsPlayerNet player= null;


        public void AssignPlayer(BattleCarsPlayerNet _player)
        {
            player = _player;
        }

        public void SetSide(bool _left) => IsLeft = _left;

        void Update()
        {
            //if he slot is empty then set the button shouldnt be active
            playerButton.interactable = IsTaken;
            //if the player is set, dsiplay their name, otherwise display awaiting player...
            nameDisplay.text = IsTaken ? GetPlayerName() : "Awaiting Player...";
        }

        private string GetPlayerName()
        {
            return string.IsNullOrEmpty(player.username) ? $"player {player.playerId + 1}" : player.username;
        }
    }
}