using UnityEngine;
using System.Collections;

public class GroundPlayerController : MonoBehaviour {

    public float moveSpeed;
    public float verticalSpeed;
    private float xSpeed;
    private float ySpeed;
    public float depth;
    private float minDeapth = 0;
    private float maxDeapth = 100;
    private GameObject levelManager;
    private bool testAbility = false;
    //private Animator anim;

    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        //anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Debug.Log("player fixed update");
        //float z = (transform.eulerAngles.z) / 360 * 2 * Mathf.PI;
        //xSpeed = -Mathf.Sin(z);
        //ySpeed = Mathf.Cos(z);
        xSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        ySpeed = Input.GetAxis("Vertical") * moveSpeed;
        //Debug.Log(z);
        //anim.SetFloat("speed", Mathf.Sqrt(Mathf.Pow(xSpeed,2) + Mathf.Pow(ySpeed, 2)));
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
        

        if (Input.GetKey(KeyCode.Space))
        {
            //test ability
            ability();


        }

    }

    public void ability()
    {
        gameObject.GetComponent<PlayerSync>().testAbility();
    }
}
