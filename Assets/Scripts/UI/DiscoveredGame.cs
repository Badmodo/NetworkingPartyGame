using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BattleCars.Networking;

namespace BattleCars.UI
{
    [RequireComponent(typeof(Button))]
    public class DiscoveredGame : MonoBehaviour
    {
        public string GameName => response.gameName;

        [SerializeField]
        private TextMeshProUGUI ipDisplay;

        private BattleCarsNetworkManager networkManager;
        private DiscoveryResponse response;

        public void Setup(DiscoveryResponse _response, BattleCarsNetworkManager _manager)
        {
            ipDisplay.text = _response.EndPoint.Address.ToString();
            networkManager = _manager;

            
            Button button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(JoinGame);
        }

        public void UpdateResponse(DiscoveryResponse _response)
        {
            response = _response;
            ipDisplay.text = $"<b>{response.gameName}</b>\n{ response.EndPoint.Address}";
        }

        private void JoinGame()
        {
            networkManager.networkAddress = ipDisplay.text.Trim((char)8203);
            networkManager.StartClient();
        }
    }
}