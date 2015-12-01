using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class GroundPlayerController : MonoBehaviour
{

    public float maxSpeed;
    private float xSpeed;
    private float ySpeed;
    public float currentSpeed;
    private float angle;
    private Animator anim;


    //skills cool down
    public List<GroundSkill> skills;
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


        GameObject escape = GameObject.FindGameObjectWithTag("escape");
        Image escapeImage = escape.GetComponent<Image>();
        skills[0].skillIcon = escapeImage;

        GameObject flashBomb = GameObject.FindGameObjectWithTag("flashBomb");
        Image flashBombImage = flashBomb.GetComponent<Image>();
        skills[1].skillIcon = flashBombImage;
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
            Debug.Log("fire1");
            if ((skills[0].currentCoolDown >= skills[0].cooldown))
            {

                //test ability
                runSwitchy();

                //set skill cooldown
                skills[0].currentCoolDown = 0;

            }            
        }

        if (Input.GetButton("Fire2"))
        {
            Debug.Log("fire2");
            if ((skills[1].currentCoolDown >= skills[1].cooldown))
            {

                //test ability
                falshBomb();

                //set skill cooldown
                skills[1].currentCoolDown = 0;

            }
        }

        foreach (GroundSkill s in skills)
        {
            if (s.currentCoolDown < s.cooldown)
            {
                s.currentCoolDown += Time.deltaTime;

                s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
            }
        }
    }

    void runSwitchy()
    {
        GetComponent<GroundPlayerSync>().activeSwitchy();

    }

    void falshBomb()
    {
        GetComponent<GroundPlayerSync>().useFlashBomb();

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


[System.Serializable]
public class GroundSkill
{
    public float cooldown;

    // [HideInInspector]
    public float currentCoolDown;


    [HideInInspector]
    public Image skillIcon;
}