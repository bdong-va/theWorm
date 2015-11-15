using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    ////unity function when a player joinin the game
    //public override void OnStartLocalPlayer()
    //{
    //    GetComponent<testPlayer>().enabled = true;
    //}

    void Start() {

        if (isLocalPlayer) {

            if (GetComponent<WormController>() != null) {
                GetComponent<WormController>().enabled = true;
            }

            if (GetComponent<GroundPlayerController>() != null) {
                GetComponent<GroundPlayerController>().enabled = true;
            }
                


            string playerName = gameObject.name.ToString();

            //TODO change it to player's real name
            if (playerName.Equals("python(Clone)"))
            {
                Debug.Log("player is worm");
                GameObject[] groundPlayerCameras = GameObject.FindGameObjectsWithTag("GroundPlayerCamera"); 
                
                foreach(GameObject camera in groundPlayerCameras){
                    camera.GetComponent<Camera>().enabled = false;    
                }                
            }
            else {
                Debug.Log("player is ground");
                GameObject[] wormCameras = GameObject.FindGameObjectsWithTag("WormCamera");

                foreach(GameObject camera in wormCameras)
                {
                    camera.GetComponent<Camera>().enabled = false;    
                }
            }
        }

    }
}
