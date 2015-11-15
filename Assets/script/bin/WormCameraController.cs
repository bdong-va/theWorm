using UnityEngine;
using System.Collections;

public class WormCameraController : MonoBehaviour {
    private GameObject player;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        //let camera follow python
        player = GameObject.FindGameObjectWithTag("Worm");
        if (player != null)
        {
            //bool playAlive = player.GetComponent<PlayerController>().playAlive;
            //if (playAlive)
            //{
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 100);
            //}
        }
    }
}
