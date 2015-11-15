using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WormController : MonoBehaviour {

    public float moveSpeed;
    private float verticalSpeed = 1;
    private float xSpeed;
    private float ySpeed;
    private float depth = 0;   //1 is on the ground, 0 is in shllow underground, -1 is in deep underground
    private float minDeapth = -1;
    private float maxDeapth = 1;
    private GameObject levelManager;
    private bool testAbility = false;
    private float hp = 100;
    private Scrollbar healthBar;
    public bool onground=false;

    //skills cool down
    public List<Skill> skills;

    //private bool upCooldown;
    //private bool downCooldown;

    // Use this for initialization
    void Start()
    {

        GameObject healthBarObject = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = healthBarObject.GetComponent<Scrollbar>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");

        GameObject skillDown= GameObject.FindGameObjectWithTag("skill_down");
        Image downImage = skillDown.GetComponent<Image>();
        skills[0].skillIcon = downImage;

        GameObject skillUp = GameObject.FindGameObjectWithTag("skill_up");
        Image upImage = skillUp.GetComponent<Image>();
        skills[1].skillIcon = upImage;
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
        //down
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //if not cool down and not in min deapth
            if ((skills[0].currentCoolDown >= skills[0].cooldown) && (depth > minDeapth)) {

                depth -= verticalSpeed;                    
                //set blur
                levelManager.GetComponent<LevelManager>().setBlur(this.depth);

                //set skill cooldown
                skills[0].currentCoolDown = 0;

            }


            //if on the ground, lost health
            if (depth < 1)
            {
                onground = false;
                CancelInvoke("hpLostByTime");
            }
        }

        //up
        if (Input.GetKeyDown(KeyCode.E))
        {

        //if not cool down and not < maxdeapth
            if ((skills[1].currentCoolDown >= skills[1].cooldown) && (depth < maxDeapth))
            {
                depth += verticalSpeed;
                //set blur
                levelManager.GetComponent<LevelManager>().setBlur(this.depth);


                //set skill cooldown
                skills[1].currentCoolDown = 0;
            }

            //if on the ground, lost health
            //only set when worm is under ground
            if (depth == 1 && !onground)
            {
                onground = true;
                InvokeRepeating("hpLostByTime", 1, 1);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(0, 0, Time.deltaTime * 180);
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


        if (Input.GetKey(KeyCode.Space))
        {
            //test ability
            ability();

        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            //anim.SetBool("Moving", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }


        foreach (Skill s in skills) {
            if (s.currentCoolDown < s.cooldown) {
                s.currentCoolDown += Time.deltaTime;
                s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
            }
        }

    }

    public void ability()
    {
        gameObject.GetComponent<PlayerSync>().testAbility();
    }

    //lose hp and set health bar
    public void loseHP(float damage) {
        hp = hp - damage;
        if (hp < 0) {
            hp = 0;
        }

        //set health bar value
        healthBar.size = hp / 100f;
    }

    //worm's hp lose along with time
    private void hpLostByTime() {
        this.loseHP(1);
    }
}


[System.Serializable]
public class Skill {
    public float cooldown;

    [HideInInspector]
    public float currentCoolDown;
    [HideInInspector]
    public Image skillIcon;
}