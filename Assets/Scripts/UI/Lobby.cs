using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCars.Networking;
using TMPro;


namespace BattleCars.UI
{
    public class Lobby : MonoBehaviour
    {
        public string LobbyName => lobbyNameInput.text;

        private List<LobbyPlayerSlot> leftTeamSlot = new List<LobbyPlayerSlot>();
        private List<LobbyPlayerSlot> rightTeamSlot = new List<LobbyPlayerSlot>();

        [SerializeField]
        private GameObject leftTeamHolder;
        [SerializeField]
        private GameObject rightTeamHolder;
        [SerializeField]
        private TMP_InputField lobbyNameInput;

        private bool assigningToLeft = true;

        private BattleCarsPlayerNet localPlayer;

        public void AssignPlayerToSlot(BattleCarsPlayerNet _player, bool _left, int _slotId)
        {
            // get the correct slot list depending on the left param
            List<LobbyPlayerSlot> slots = _left ? leftTeamSlot : rightTeamSlot;
            //assign the plaer to the relevent slot in the list
            slots[_slotId].AssignPlayer(_player);
        }

        public void OnPlayerConnected(BattleCarsPlayerNet _player)
        {
            bool assigned = false;

            if(_player.isLocalPlayer)
            {
                localPlayer = _player;
            }

            List<LobbyPlayerSlot> slots = assigningToLeft ? leftTeamSlot : rightTeamSlot;

            slots.ForEach(slot =>
            {
                if (assigned)
                {
                    return;
                }
                else if (!slot.IsTaken)
                {

                    //if we havnt already assigned a player to the slot
                    //hasnt been taken, assign the player to this slot and flag
                    //as slot been assigned
                    slot.AssignPlayer(_player);
                    slot.SetSide(assigningToLeft);
                    assigned = true;
                }
            });

            for(int i = 0; i < leftTeamSlot.Count; i++)
            {
                LobbyPlayerSlot slot = leftTeamSlot[i];
                if(slot.IsTaken)
                {
                    localPlayer.AssignPlayerToSlot(slot.IsLeft, i, slot.Player.playerId);
                }
            }

            for (int i = 0; i < rightTeamSlot.Count; i++)
            {
                LobbyPlayerSlot slot = rightTeamSlot[i];
                if (slot.IsTaken)
                {
                    localPlayer.AssignPlayerToSlot(slot.IsLeft, i, slot.Player.playerId);
                }
            }

            assigningToLeft = !assigningToLeft;
        }

        private void Start()
        {
            leftTeamSlot.AddRange(leftTeamHolder.GetComponentsInChildren<LobbyPlayerSlot>());
            rightTeamSlot.AddRange(rightTeamHolder.GetComponentsInChildren<LobbyPlayerSlot>());
        }

    }
}