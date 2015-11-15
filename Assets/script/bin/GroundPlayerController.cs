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

    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    void FixedUpdate()
    {
        //Debug.Log("player fixed update");
        float z = (transform.eulerAngles.z) / 360 * 2 * Mathf.PI;
        xSpeed = -Mathf.Sin(z);
        ySpeed = Mathf.Cos(z);
        //Debug.Log(z);
    }

    // Update is called once per frame
    void Update()
    {
        xSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        ySpeed = Input.GetAxis("Vertical") * moveSpeed;
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
