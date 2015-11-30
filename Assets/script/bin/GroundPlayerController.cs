using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GroundPlayerController : MonoBehaviour
{

    public float maxSpeed;
    private float xSpeed;
    private float ySpeed;
    public float currentSpeed;
    private float angle;
    private Animator anim;
    //[SyncVar]
    //private Vector3 syncPos;
    //[SyncVar]
    //private float syncRotation;
    //[SyncVar]
    //private float syncSpeed;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        angle = 0;
    }

    void FixedUpdate()
    {
        // update the player speed
        //if (isServer)
        //{
            xSpeed = Input.GetAxis("Horizontal") * maxSpeed;
            ySpeed = Input.GetAxis("Vertical") * maxSpeed;
            currentSpeed = Mathf.Sqrt(Mathf.Pow(xSpeed, 2) + Mathf.Pow(ySpeed, 2));
            anim.SetFloat("speed", currentSpeed);
            angle = (Mathf.Atan2(ySpeed, xSpeed) * 180 / Mathf.PI) - 90;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // position control
        transform.position = transform.position + new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);
        if (xSpeed != 0 || ySpeed != 0)
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        // ability control
        if (Input.GetButton("Fire1"))
        {
            runSwitchy();
        }
    }

    void runSwitchy()
    {
        GetComponent<GroundPlayerSync>().activeSwitchy();

    }
    //[Command]
    //void CmdSyncDataToServer()
    //{
    //    syncPos = transform.position;
    //    syncRotation = angle;
    //    syncSpeed = currentSpeed;
    //}
    ////only run on clients
    ////tell server the position
    //[ClientCallback]
    //void TransmitDataFromServer()
    //{
    //    transform.position = syncPos;
    //    angle = syncRotation;
    //    transform.rotation = Quaternion.Euler(0f, 0f, angle);
    //    currentSpeed = syncSpeed;
    //    anim.SetFloat("speed", currentSpeed);
    //}

}
