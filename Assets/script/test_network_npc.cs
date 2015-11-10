using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class test_network_npc : NetworkBehaviour {

    [SyncVar]
    private Vector3 syncPos;
    [SerializeField]
    float lerpRate = 5;

    private bool moveDir = true;
	// Use this for initialization
	void Start () {
        if (isServer)
        {   
            InvokeRepeating("testMOve", 1, 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        //syncPos = transform.position;
        CmdProvidePositionToServer(transform.position);

        TransmitPosition();
    }

    void testMOve() {
        float randomNumber = Random.Range(0, 100);
        if (randomNumber<25)
        {   
            GetComponent<Rigidbody2D>().velocity = new Vector2(5, 5);
            moveDir = false;
        }
        else if(randomNumber<50)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-5, -5);
            moveDir = true;
        }
        else if (randomNumber < 75)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(5, -5);
            moveDir = true;
        }
        else if (randomNumber < 100)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 5);
            moveDir = true;
        }
    }

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }


    //only run on clients
    //tell server the position
    [ClientCallback]
    void TransmitPosition()
    {
        transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
    }
}
