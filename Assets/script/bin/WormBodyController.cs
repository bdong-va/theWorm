using UnityEngine;
using System.Collections;

//Only control worm's body
//only active when worm is not local player
public class WormBodyController : MonoBehaviour {
    public float speed = 1f;
    public float elastifactor = 0.3f; // To stop it from scrunching up when it stops! Higher value = less scrunchy
    public Transform bod1;
    public Transform bod2;
    public Transform bod3;

    private float[] pos = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f }; // *** SYNC VARIABLE OVER NETWORK **



    void Start () {
        setupSegPositions();
    }
	
	// Update is called once per frame
	void Update () {

        pos[6] = pos[4];
        pos[4] = pos[2];
        pos[2] = pos[0];
        //}

        //if (Mathf.Abs(v)>elastifactor) {
        pos[7] = pos[5];
        pos[5] = pos[3];
        pos[3] = pos[1];

        pos[1] = transform.position.y;
        pos[0] = transform.position.x;


        updateWormSegPositions();

    }

    void setupSegPositions()
    {
        pos[0] = transform.position.x;
        pos[1] = transform.position.y;
        pos[2] = bod1.position.x;
        pos[3] = bod1.position.y;
        pos[4] = bod2.position.x;
        pos[5] = bod2.position.y;
        pos[6] = bod3.position.x;
        pos[7] = bod3.position.y;
    }

    void updateWormSegPositions()
    {
        bod1.position = new Vector3(pos[2], pos[3]);
        bod2.position = new Vector3(pos[4], pos[5]);
        bod3.position = new Vector3(pos[6], pos[7]);
    }
}
