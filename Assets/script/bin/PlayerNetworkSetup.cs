using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {


    void Start() {

        if (isLocalPlayer) {
            //check if player is worm or gound player
            //then enable or disable the controller script
            if (gameObject.transform.Find("head") != null)
            {
                if (gameObject.transform.Find("head").gameObject.GetComponent<WormController>() != null)
                {   
                    //set worm's controller
                    gameObject.transform.Find("head").gameObject.GetComponent<WormController>().enabled = true;
                    gameObject.transform.Find("head").gameObject.GetComponent<WormBodyController>().enabled = false;

                }
            }

            if (GetComponent<GroundPlayerController>() != null) {
                GetComponent<GroundPlayerController>().enabled = true;
                GetComponent<GroundPlayerBodyController>().enabled = false;

            }
                

            //check if player is worm or ground player
            //then enable or disable the camera
            //TODO: merge this code with above
            string playerName = gameObject.name.ToString();
            if (playerName.Equals("worm(Clone)"))
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
