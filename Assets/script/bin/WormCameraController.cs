using UnityEngine;
using System.Collections;

public class WormCameraController : MonoBehaviour {
    private GameObject playerObject;
    private GameObject playerHead;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        //let camera follow python
        playerObject = GameObject.FindGameObjectWithTag("Worm");
        playerHead = playerObject.transform.Find("head").gameObject;
        if (playerHead != null)
        {
            //bool playAlive = player.GetComponent<PlayerController>().playAlive;
            //if (playAlive)
            //{

                transform.position = new Vector3(playerHead.transform.position.x, playerHead.transform.position.y, playerHead.transform.position.z - 100);
            //}
        }
    }
}
