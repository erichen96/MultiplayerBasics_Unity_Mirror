using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    // public override void OnClientConnect(NetworkConnection conn)
    // {
    //     base.OnClientConnect(conn);

    //     Debug.Log("I connected to a server!");
    // }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        
        Color displayColor = new Color(
            Random.Range(01,1f),
            Random.Range(0f,1f),
            Random.Range(0f,1f));
        player.SetDisplayName($"Player {numPlayers}");
        player.SetDisplayColor(displayColor);
        Debug.Log("New Player has Joined: Total Players " + numPlayers);
    }

}
