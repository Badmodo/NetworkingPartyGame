using System;
using System.Net;
using BattleCars.UI;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.Events;

/*
	Discovery Guide: https://mirror-networking.com/docs/Guides/NetworkDiscovery.html
    Documentation: https://mirror-networking.com/docs/Components/NetworkDiscovery.html
    API Reference: https://mirror-networking.com/docs/api/Mirror.Discovery.NetworkDiscovery.html
*/

namespace BattleCars.Networking
{
    [SerializeField]

    public class DiscoveryRequest : NetworkMessage
    {
        //name of the game
        public string gameName;
    }

    //reieved by the client and converted
    public class DiscoveryResponse : NetworkMessage
    {
        //the server that sent the message
        //this is a property so that it is not serizlied but the client
        public IPEndPoint EndPoint { get; set; }
        //uri points to the spacific part of the address (url)
        public Uri uri;
        public long serverID;
        //name of the game
        public string gameName;
    }

    public class ServerFoundEvent : UnityEvent<DiscoveryResponse> { };


    public class BattlecarsNetworkDiscovery : NetworkDiscoveryBase<DiscoveryRequest, DiscoveryResponse>
    {
        #region Server

        public long ServerID { get; private set; }
        [Tooltip("Transport to beadvertised during discovery")]
        public Transport transport;
        [Tooltip("Invoked when a server is found")]
        public ServerFoundEvent onServerFound = new ServerFoundEvent();

        private Lobby lobby;

        public override void Start()
        {
            ServerID = RandomLong();

            if(transport == null)
            {
                transport = Transport.activeTransport;
            }

            base.Start();
        }

        private void Update()
        {
            if(lobby == null)
            {
                lobby = FindObjectOfType<Lobby>();
            }
        }

        /// <summary>
        /// Process the request from a client
        /// </summary>
        /// <remarks>
        /// Override if you wish to provide more information to the clients
        /// such as the name of the host player
        /// </remarks>
        /// <param name="request">Request coming from client</param>
        /// <param name="endpoint">Address of the client that sent the request</param>
        /// <returns>A message containing information about this server</returns>
        protected override DiscoveryResponse ProcessRequest(DiscoveryRequest _request, IPEndPoint _endpoint)
        {
            try
            {
                return new DiscoveryResponse()
                {
                    serverID = ServerID,
                    uri = transport.ServerUri(),
                    gameName = lobby.LobbyName
                };
            }
            catch(NotImplementedException _e)
            {
                Debug.LogError($"Transport {transport} does not support network discovery");
                throw;
            }
        }

        #endregion

        #region Client

        /// <summary>
        /// Create a message that will be broadcasted on the network to discover servers
        /// </summary>
        /// <remarks>
        /// Override if you wish to include additional data in the discovery message
        /// such as desired game mode, language, difficulty, etc... </remarks>
        /// <returns>An instance of ServerRequest with data to be broadcasted</returns>
        protected override DiscoveryRequest GetRequest()
        {
            return new DiscoveryRequest();
        }

        /// <summary>
        /// Process the answer from a server
        /// </summary>
        /// <remarks>
        /// A client receives a reply from a server, this method processes the
        /// reply and raises an event
        /// </remarks>
        /// <param name="response">Response that came from the server</param>
        /// <param name="endpoint">Address of the server that replied</param>
        protected override void ProcessResponse(DiscoveryResponse _response, IPEndPoint _endpoint) 
        {
            //I dont fully understand this code, but I know its something we need to do
            _response.EndPoint = _endpoint;
            UriBuilder realUri = new UriBuilder(_response.uri)
            {
                Host = _response.EndPoint.Address.ToString()
            };

            _response.uri = realUri.Uri;

            onServerFound.Invoke(_response);
        }

        #endregion
    }
}