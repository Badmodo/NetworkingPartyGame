using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Battlecars.Networking;
using Mirror.Discovery;
using UnityEngine.Serialization;

namespace BattleCars.UI
{

    public class ConnectionMenu : MonoBehaviour
    {
        [SerializeField]
        private Button hostButton;
        [SerializeField]
        private Button connectButton;
        [SerializeField]
        private TextMeshProUGUI gameName;
        [SerializeField]
        private TextMeshProUGUI playerCount;
        [SerializeField]
        private TextMeshProUGUI hostName;
        [SerializeField] 
        private TMP_InputField playerNameInput;
        [SerializeField] 
        private TMP_InputField gameNameInput;
        [SerializeField]
        private BattlecarsNetworkManager networkManager;
        [SerializeField]
        private DiscoveredGame gameTemplate;
        [SerializeField]
        private Transform foundGamesHolder;

        [SerializeField]
        private Text ipText;

        private NetworkDiscovery networkDiscovery;

        private Dictionary<IPAddress, DiscoveredGame> discoveredGames = new Dictionary<IPAddress, DiscoveredGame>();


        void Start()
        {
            hostButton.onClick.AddListener(() => networkManager.StartHost());
            connectButton.onClick.AddListener(OnClickConnect);

            
            networkManager.discovery.onServerFound.AddListener(OnDetectServer);
            networkManager.discovery.StartDiscovery();
           // networkDiscovery.StartDiscovery();
        }

        private void OnClickConnect()
        {
            networkManager.networkAddress = ipText.text;
            networkManager.StartClient();
        }

        private void OnDetectServer(DiscoveryResponse _response)
        {
            if (!discoveredGames.ContainsKey(_response.EndPoint.Address))
            {
                DiscoveredGame game = Instantiate(gameTemplate, foundGamesHolder);
                game.gameObject.SetActive(true);
                game.Setup(_response, networkManager);
                discoveredGames.Add(_response.EndPoint.Address, game);
            }
            else
            {
                DiscoveredGame game = discoveredGames[_response.EndPoint.Address];
                if(game.GameName != _response.gameName)
                {
                    game.UpdateResponse(_response);
                }
            }
        }

        private void NamingPlayer() => playerNameInput.interactable = true;
        
        

    }
}