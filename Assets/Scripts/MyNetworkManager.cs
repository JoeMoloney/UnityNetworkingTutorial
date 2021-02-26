using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    //Callbacks
    //Methods Associated With Connecting/Disconnecting To/From Server
    public override void OnStartServer()
    {
        Debug.Log("Server Has Started");
    }

    public override void OnStopServer()
    {
        Debug.Log("Server Has Stopped");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connected To Server");
    }

    public override void OnClientDisconnect(NetworkConnection connection)
    {
        Debug.Log("Disconnected From Server");
    }
}
