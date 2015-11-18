using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WormController : MonoBehaviour {
    public float moveSpeed;
    private float verticalSpeed = 1;
    private float xSpeed;
    private float ySpeed;
    public float depth = 0;   //1 is on the ground, 0 is in shllow underground, -1 is in deep underground
    private float minDeapth = -1;
    private float maxDeapth = 1;
    private GameObject levelManager;
    private bool testAbility = false;
    private float hp = 100;
    private Scrollbar healthBar;
    public bool onground=false;
    public bool isActive;
    //worm body fields
    public float speed = 0.1f;
    public float elastifactor = 0.3f; // To stop it from scrunching up when it stops! Higher value = less scrunchy
    public Transform bod1;
    public Transform bod2;
    public Transform bod3;

    public float[] pos = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f }; // *** SYNC VARIABLE OVER NETWORK **

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

        setupSegPositions();

        isActive = gameObject.GetComponent<WormController>().isActiveAndEnabled;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //transform.Translate (new Vector3 (h * speed, v * speed));

        transform.position = new Vector3((transform.position.x + (h * speed)), (transform.position.y + (v * speed)));

        if (Mathf.Abs(h) > elastifactor || Mathf.Abs(v) > elastifactor)
        {
            pos[6] = pos[4];
            pos[4] = pos[2];
            pos[2] = pos[0];
            //}

            //if (Mathf.Abs(v)>elastifactor) {
            pos[7] = pos[5];
            pos[5] = pos[3];
            pos[3] = pos[1];
        }

        pos[1] = transform.position.y;
        pos[0] = transform.position.x;

        updateWormSegPositions();

        /* if (v != 0.0f) {
			v = v / Mathf.Abs(v);
			Debug.Log (v);
			GetComponent<Rigidbody2D> ().AddForce (transform.up.normalized * speed * v);
		}
		transform.Rotate(0, 0, -h * speed * Time.deltaTime * 90); */

    }

    void setupSegPositions()
    {
        pos[0] = transform.position.x;
        pos[1] = transform.position.y;
        pos[2] = bod1.position.x;
        pos[3] = bod1.position.y;
        pos[4] = bod2.position.x;
        pos[5] = bod2.position.y;
        pos[6] = bod3.position.x;
        pos[7] = bod3.position.y;
    }


    void updateWormSegPositions()
    {
        bod1.position = new Vector3(pos[2], pos[3]);
        bod2.position = new Vector3(pos[4], pos[5]);
        bod3.position = new Vector3(pos[6], pos[7]);
    }

    // Update is called once per frame
    void Update()
    {
        //down
        if (Input.GetButtonDown("Fire1"))
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
        if (Input.GetButtonDown("Fire2"))
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

        //if (Input.GetButtonDown("Jump"))
        //{
        //    //test ability
        //    ability();

        //}



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