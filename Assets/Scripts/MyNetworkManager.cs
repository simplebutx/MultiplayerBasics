using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("I connected to server");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) //conn = ����
    {
        base.OnServerAddPlayer(conn);
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>(); //conn.identity �� player ���ο� �ִ� NetworkIdentity
        
        player.SetDisplayName($"Player {numPlayers}");
        Debug.Log($"There are now {numPlayers} players");
    }
}
