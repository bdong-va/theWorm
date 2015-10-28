using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    //unity function when a player joinin the game
    public override void OnStartLocalPlayer()
    {
        GetComponent<testPlayer>().enabled = true;
    }
}
