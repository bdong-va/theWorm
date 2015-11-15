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
            if (playerName.Equals("python(Clone)"))
            {
                Debug.Log("player is python");
                GameObject groundPlayerCamera = GameObject.FindGameObjectWithTag("GroundPlayerCamera"); 
                groundPlayerCamera.GetComponent<Camera>().enabled = false;
               


            }
            else {
                Debug.Log("player is ground");
                GameObject pythonCamera = GameObject.FindGameObjectWithTag("PythonCamera");
                pythonCamera.GetComponent<Camera>().enabled = false;
            }
        }

    }
}
