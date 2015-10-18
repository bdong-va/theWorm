using UnityEngine;
using System.Collections;

public class indicatorController : MonoBehaviour {

    public float emptyOffset;
    public float fullOffset;
    private GameObject player;
    private float depth;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        depth = player.gameObject.GetComponent<testPlayer>().depth;
        foreach (Transform t in transform)
        {
            if (t.name == "core")
            {
                t.transform.position = new Vector3(t.transform.position.x, fullOffset - (depth/100) * (fullOffset - emptyOffset), t.transform.position.z);
            }
        }
    }
}
