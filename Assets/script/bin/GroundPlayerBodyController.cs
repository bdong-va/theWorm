using UnityEngine;
using System.Collections;

public class GroundPlayerBodyController : MonoBehaviour {

    public float maxSpeed;
    private float xSpeed;
    private float ySpeed;
    public float currentSpeed;
    private float angle;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        angle = 0;
    }

    void FixedUpdate()
    {

        anim.SetFloat("speed", currentSpeed);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
