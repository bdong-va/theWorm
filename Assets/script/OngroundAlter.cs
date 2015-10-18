using UnityEngine;
using System.Collections;

public class OngroundAlter : MonoBehaviour
{

    private bool findPlayer = false;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float playerDepth = player.GetComponent<testPlayer>().depth;
        //if find player and player is on the ground
        // alert
        if (findPlayer && playerDepth == 0) {
            //alert
            this.transform.root.gameObject.GetComponent<Enemy_controller_01>().TriggerAlert();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // If it hits the player
        if (col.tag == "Player")
        {
            findPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        // If it hits the player
        if (col.tag == "Player")
        {
            findPlayer = false;
        }
    }
}
