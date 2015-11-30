using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
/// <summary>
/// author: Bo
/// set decision tree logic system for enemies, or Civilians.
/// </summary>
public class npcController : NetworkBehaviour{
    public float panicLevelMax;
    public float panicLevelMin;
    public float WalkSpeedMax;
    public float runSpeed;
    public float randomTimeLimit;
    public bool senseWorm;
    public float walkChance;
    public float alertDistance;
    public GameObject bloodStain;

    delegate void MyDelegate();
    MyDelegate enemyAction;

    [SyncVar]
    private Vector3 syncPos;
    [SyncVar]
    private float syncRotation;
    [SyncVar]
    private float syncSpeed;
    [SyncVar]
    private bool death;
    private LevelManager levelManager;
    private Animator anim;
    private float randomTime;
    private float panicLevel;
    private Vector3 wormPosition;
    private float speed;
    private float angle;
    private bool fireworkPlayed;



    // Use this for initialization
    public void Start () {
        randomTime = 0;
        panicLevel = 0;
        anim = GetComponent<Animator>();
        wormPosition = new Vector3(0f, 0f, 0f); 
        speed = 0;
        angle = 0;
        enemyAction = InPanic;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        death = false;
        fireworkPlayed = false;
    }

    // Update is called once per frame
    public void Update () {
        if (isServer)
        {
            checkWorm();
            makeDecision();
            enemyAction();   
        }
        //death check.
        if (death)
        {
            if (!fireworkPlayed)
            {
                Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                Object explosionClone = Instantiate(bloodStain, transform.position, randomRotation);
                fireworkPlayed = true;
                Invoke("destroyThis", 1);
            }
        }
        CmdSyncDataToServer();
        TransmitDataFromServer();

    }
    // is enemy in panic?
    private void InPanic()
    {
        if (panicLevel > panicLevelMin)
        {
            checkWormWhenPanic();
        }
        else
        {
            checkWormWhenNotPanic();
        }

    }

    // seen worm when in panic?
    private void checkWormWhenPanic()
    {
        if (senseWorm)
        {
            updateWormPosition();
            enemyAction = runForYourLife;
            panicLevel = panicLevelMax;
        }
        else
        {
            enemyAction = runForYourLife;
            panicLevel -= Time.deltaTime;
        }
    }
    // seen worm when not in panic?
    private void checkWormWhenNotPanic()
    {
        if (senseWorm)
        {
            updateWormPosition();
            enemyAction = alert;
            panicLevel += Time.deltaTime;
        }
        else
        {
            randomCheck();
        }

    }
    // is random session finished?
    private void randomCheck()
    {
        if (randomTime < randomTimeLimit)
        {
            randomTime += Time.deltaTime;
            enemyAction = executeCasualAction;
        }
        else
        {
            randomizeAction();
            randomTime = 0;
            enemyAction = executeCasualAction;
        }
    }

    // set random data for next random session, 
    private void randomizeAction()
    {
        if (Random.Range(0f, 1f) > walkChance)
        {
            speed = 0;
        }
        else
        {
            speed = Random.Range(WalkSpeedMax / 2, WalkSpeedMax);
            angle = Random.Range(0, 360);
        }
    }
    // check if npc is dead
    private void isDead()
    {
        if (death)
        {
            if (!fireworkPlayed) {
                Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                Object explosionClone = Instantiate(bloodStain, transform.position, randomRotation);
                fireworkPlayed = true;
                transform.position = new Vector3(20, 20, 0);
                Invoke("destroyThis", 1);
            }
            //kill();

        }
        else
        {
            InPanic();
        }
    }
    
    // start the decision make processing.
    private void makeDecision()
    {
        isDead();
    }
    
    private void checkWorm()
    {
        float distance = Vector3.Distance(transform.position, levelManager.wormPosition);
        if (distance < alertDistance && levelManager.wormOnGround)
        {
            this.senseWorm = true;
        }
        else
        {
            this.senseWorm = false;
        }
    }
    //....................................
    //Enemy Action
    //....................................

    // object in panic. frozen, scared, prepared to run for their life in one sec.
    private void alert()
    {
        speed = 0;
        anim.SetFloat("speed", speed);
    }
    
    // object will run away from the last position of worm in his memory
    private void runForYourLife()
    {
        speed = runSpeed;
        angle = -90 + Mathf.Atan2((transform.position.y - wormPosition.y), (transform.position.x - wormPosition.x)) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = transform.position + new Vector3(-speed * Mathf.Sin(angle * 2 * Mathf.PI / 360) * Time.deltaTime, speed * Mathf.Cos(angle * 2 * Mathf.PI / 360) * Time.deltaTime, 0f);
        
        anim.SetFloat("speed", speed);
    }
    
    // worm appeared again, update memory. adjust steps.
    private void updateWormPosition()
    {
        wormPosition = levelManager.wormPosition;
        //a swallow clone.
        wormPosition = new Vector3(wormPosition.x, wormPosition.y, wormPosition.z);
    }
    
    // nothing happened, just idle and random walk.
    private void executeCasualAction()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = transform.position + new Vector3(-speed * Mathf.Sin(angle * 2 * Mathf.PI / 360) * Time.deltaTime, speed * Mathf.Cos(angle * 2 * Mathf.PI / 360) * Time.deltaTime, 0f);
        panicLevel = 0;
        anim.SetFloat("speed", speed);
    }

    // interface for other gameobject
    // kill this npc object.
    public void kill()
    {
        death = true;
    }

    void destroyThis()
    {
        Destroy(gameObject);
    }

    // only run on server
    // get data from client, store in serer.
    [Command]
    void CmdSyncDataToServer()
    {
        syncPos = transform.position;
        syncRotation = angle;
        syncSpeed = speed;
    }

    //only run on clients
    //tell server the position
    [ClientCallback]
    void TransmitDataFromServer()
    {
        //transform.position = syncPos;
        transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime);
        angle = syncRotation;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        speed = syncSpeed;
        anim.SetFloat("speed", speed);
    }
}
