using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;



public class Network_Custom : NetworkManager{

    [SerializeField]
    Vector3 playerSpawnPos = new Vector3(0, 0, 0);
    [SerializeField]
    GameObject character1;
    [SerializeField]
    GameObject character2;

    private bool isWorm = true;
    // in the Network Manager component, you must put your player prefabs 
    // in the Spawn Info -> Registered Spawnable Prefabs section 
    public short playerPrefabIndex;

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        // always remember to register prefabs before spawning them.

        ClientScene.RegisterPrefab(character1);
        ClientScene.RegisterPrefab(character2);
        Debug.Log("Connect to a new game");

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    // Instantiate whichever character the player chose and was assigned to chosenCharacter
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (isWorm)
        {
           // playerPrefab = character1;
            var player1 = (GameObject)GameObject.Instantiate(character1, playerSpawnPos, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player1, playerControllerId);
            isWorm = false;
        }
        else
        {
            //playerPrefab = character2;
            var player2 = (GameObject)GameObject.Instantiate(character2, playerSpawnPos, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player2, playerControllerId);

        }

        //MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        //msg.controllerID = playerControllerId;
        //NetworkServer.SendToClient(conn.connectionId, MsgTypes.PlayerPrefab, msg);
        //MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        //msg.controllerID = playerControllerId;
        //NetworkServer.SendToClient(conn.connectionId, player1, msg);
    }
}


