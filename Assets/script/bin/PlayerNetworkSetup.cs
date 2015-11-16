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

            if (gameObject.transform.Find("head") != null)
            {
                if (gameObject.transform.Find("head").gameObject.GetComponent<WormController>() != null)
                {
                    gameObject.transform.Find("head").gameObject.GetComponent<WormController>().enabled = true;
                    gameObject.transform.Find("head").gameObject.GetComponent<WormBodyController>().enabled = false;
                }
            }

            if (GetComponent<GroundPlayerController>() != null) {
                GetComponent<GroundPlayerController>().enabled = true;
                GameObject worm = GameObject.FindGameObjectWithTag("Worm");
                worm.transform.Find("head").gameObject.GetComponent<WormBodyController>().enabled = true;
            }
                


            string playerName = gameObject.name.ToString();

            //TODO change it to player's real name
            if (playerName.Equals("python_temp(Clone)"))
            {
                Debug.Log("player is worm");
                GameObject groundPlayerCamera = GameObject.FindGameObjectWithTag("GroundPlayerCamera");
                groundPlayerCamera.GetComponent<Camera>().enabled = false;

                GameObject groundPlayerUICamera = GameObject.FindGameObjectWithTag("GroundPlayerUICamera");
                groundPlayerUICamera.GetComponent<Camera>().enabled = false;

            }
            else {
                Debug.Log("player is ground");
                GameObject wormCamera = GameObject.FindGameObjectWithTag("WormCamera");
                wormCamera.GetComponent<Camera>().enabled = false;

                GameObject wormUICamera = GameObject.FindGameObjectWithTag("WormUICamera");
                wormUICamera.GetComponent<Camera>().enabled = false;

            }
        }

    }
}
