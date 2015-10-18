using UnityEngine;
using System.Collections;

public class testPlayer : MonoBehaviour {
    public float moveSpeed;
    public float verticalSpeed;
    private float xSpeed;
    private float ySpeed;
    public float depth;
    private float minDeapth = 0;
    private float maxDeapth = 100;
    private GameObject levelManager;
    // Use this for initialization
    void Start () {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    void FixedUpdate()
    {
        //Debug.Log("player fixed update");
        float z = (transform.eulerAngles.z ) / 360 * 2 * Mathf.PI;
        xSpeed = -Mathf.Sin(z);
        ySpeed = Mathf.Cos(z);
        //Debug.Log(z);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(0, 0, Time.deltaTime * 180);
        }


        if (Input.GetKey(KeyCode.Q))
        {
            if (depth > minDeapth)
            {
                depth -= verticalSpeed;

                //set blur
                levelManager.GetComponent<LevelManager>().setBlur(this.depth);
            }

        }


        if (Input.GetKey(KeyCode.E))
        {

            if (depth < maxDeapth)
            {
                depth += verticalSpeed;
                //set blur
                levelManager.GetComponent<LevelManager>().setBlur(this.depth);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -Time.deltaTime * 180);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log(xSpeed+ySpeed);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * xSpeed, moveSpeed * ySpeed);


        }

        if (Input.GetKey(KeyCode.S))
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed * xSpeed, -moveSpeed * ySpeed);

        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            //anim.SetBool("Moving", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
