using UnityEngine;
using System.Collections;

public class indicatorController : MonoBehaviour {
    public float depth;
    public float emptyOffset;
    public float fullOffset;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (Transform t in transform)
        {
            if (t.name == "Core")
            {
                t.transform.position = new Vector3(t.transform.position.x, emptyOffset + (depth/100) * (fullOffset - emptyOffset), t.transform.position.z);
            }
        }
    }
}
