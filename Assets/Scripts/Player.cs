using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    //=====Notes=====//
    //[Callbacks] 
    //Hook into an event in a game
    //E.g. Client Joins/Leaves Server, Objects Spawned/Destroyed, Server Starts/Stops

    //[Command] + [ClientRpc] + [TargetRpc] (Remote Procedure Calls) 
    //Call functions in code across a network
    //Command : Client calls method on a Server
    //RPC : Server calls method on a Client
    
    //[Message] 
    //Low Level Networking for use with optimizing performance & complex data structures
    //
    //===Giving Authority===//
    //Give Object Authority To Client
    //NetworkServer.AddPlayerForConnection(connectionToClient, playerObject);
    //
    //Give Object Authority To Client When Object Is Spawned
    //NetworkServer.Spawn(spawnedObject, connectionToClient);
    //
    //Give Authority To Client
    //NetworkIdentity.AssignClientAuthority(connectionToClient);
    //[NetworkIdentity] object we're handing authority to
    //
    //Remove Authority Of Object From Client
    //NetworkIdentity.RemoveClientAuthority();
    //[NetworkIdentity] object we're removing authority from
    //
    //Can't remove authority from player object, so replace object instead
    //NetworkServer.ReplacePlayerForConnection(clientConnection, playerObject);
    //
    //Attributes
    //[Client] functions that can only run on clients
    //[Server] functions that can only run on servers
    //====================//

    [SyncVar(hook = nameof(OnHolaCountChanged))] //Variables to sync from Server To Client
    int holaCount = 0; //Hola count on server
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = UnityEngine.Input.GetAxis("Horizontal");
            float moveVertical = UnityEngine.Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update()
    {
        HandleMovement();
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X)) //Client command to run on server
        {
            Debug.Log("Sending Hola To The Server");
            Hola();
        }

        //If player goes above Y Axis of 50
        // if(isServer && transform.position.y > 50)
        // {
        //     TooHigh();
        // }
    }

    [Command] //Client command run on server
    void Hola()
    {
        Debug.Log("Received Hola Command From Client");
        holaCount += 1;
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Received Hola From Server");
    }

    [ClientRpc] //Runs on all clients connected
    void TooHigh()
    {
        Debug.Log("Too High");
    }

    void OnHolaCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"Old Count Holas: {oldCount}, New Count Holas: {newCount}");
    }
}
