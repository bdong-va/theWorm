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

    //[SerializeField]
    //GameObject NPC;

    Vector3 position1 = new Vector3(0, 0, 0);
    Vector3 position2 = new Vector3(10, 0, 0);
    Vector3 position3 = new Vector3(20, 0, 0);
    Vector3 position4 = new Vector3(-10, 0, 0);
    Vector3 position5 = new Vector3(-20, 0, 0);

    private ArrayList positionList = new ArrayList();
    
    //positionList[0] = position1;
    private bool isWorm = true;
    // in the Network Manager component, you must put your player prefabs 
    // in the Spawn Info -> Registered Spawnable Prefabs section 
    public short playerPrefabIndex;

    //void Start()
    //{
    //    positionList.Add(position1);
    //    positionList.Add(position2);
    //    positionList.Add(position3);
    //    positionList.Add(position4);
    //    positionList.Add(position5);
    //}

    //public override void OnStartServer()
    //{
    //    foreach (Vector3 v in positionList) {
    //        spawnNPC(v);
    //    }
    //    for (int i = 0; i < positionList.Count; i++) {
    //        spawnNPC(positionList.)
    //    }
    //}

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
            var player1 = (GameObject)GameObject.Instantiate(character1, new Vector3(Random.Range(-15,15), Random.Range(-9,9),0f), Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player1, playerControllerId);
            isWorm = false;
        }
        else
        {
            //playerPrefab = character2;
            var player2 = (GameObject)GameObject.Instantiate(character2, new Vector3(Random.Range(-15, 15), Random.Range(-9, 9), 0f), Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player2, playerControllerId);

        }


        //MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        //msg.controllerID = playerControllerId;
        //NetworkServer.SendToClient(conn.connectionId, MsgTypes.PlayerPrefab, msg);
        //MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        //msg.controllerID = playerControllerId;
        //NetworkServer.SendToClient(conn.connectionId, player1, msg);
    }


    //void spawnNPC(Vector3 position)
    //{
    //    GameObject npc = GameObject.Instantiate(NPC, position,Quaternion.identity) as GameObject;
    //    NetworkServer.Spawn(npc);
    //}
}


