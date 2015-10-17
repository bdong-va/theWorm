using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

    private bool findPlayer = false;
    private GameObject player;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        //int thisZ = this.transform.position.z;
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        // If it hits an enemy...
        if (col.tag == "Player")
        {
            Debug.Log("found player");
            findPlayer = true;
            //this.die();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        // If it hits an enemy...
        if (col.tag == "Player")
        {
            Debug.Log("player left");
            findPlayer = false;
            //this.die();
        }
    }
}
