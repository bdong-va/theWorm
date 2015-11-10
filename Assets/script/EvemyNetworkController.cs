using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
/// <summary>
/// author: Bo
/// set decision tree logic system for enemies, or Civilians.
/// </summary>
public class EvemyNetworkController : NetworkBehaviour
{
    public float panicLevelMax;
    public float panicLevelMin;
    public float WalkSpeedMax;
    public float runSpeed;
    public float randomTimeLimit;
    public bool sensePython;
    public float walkChance;


    delegate void MyDelegate();
    MyDelegate enemyAction;

    private Animator anim;
    private float randomTime;
    private float panicLevel;
    private Vector3 pythonPosition;
    private float speed;
    private float angle;

    // Use this for initialization
    public void Start()
    {
        randomTime = 0;
        panicLevel = 0;
        anim = GetComponent<Animator>();
        pythonPosition = new Vector3(0f, 0f, 0f);
        speed = 0;
        angle = Random.Range(0, 360);

        if (isServer)
        {
            enemyAction = InPanic;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (isServer)
        {
            makeDecision();
            enemyAction();
        }
    }
    // is enemy in panic?
    private void InPanic()
    {
        if (panicLevel > panicLevelMin)
        {
            checkPythonWhenPanic();
        }
        else
        {
            checkPythonWhenNotPanic();
        }

    }

    // seen Python when in panic?
    private void checkPythonWhenPanic()
    {
        if (sensePython)
        {
            updatePythonPosition();
            enemyAction = runForYourLife;
            panicLevel = panicLevelMax;
        }
        else
        {
            enemyAction = runForYourLife;
            panicLevel -= Time.deltaTime;
        }
    }
    // seen python when not in panic?
    private void checkPythonWhenNotPanic()
    {
        if (sensePython)
        {
            updatePythonPosition();
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
    // start the decision make processing.
    private void makeDecision()
    {
        InPanic();
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

    // object will run away from the last position of python in his memory
    private void runForYourLife()
    {
        speed = runSpeed;
        angle = -90 + Mathf.Atan2((transform.position.y - pythonPosition.y), (transform.position.x - pythonPosition.x)) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = transform.position + new Vector3(-speed * Mathf.Sin(angle * 2 * Mathf.PI / 360) * Time.deltaTime, speed * Mathf.Cos(angle * 2 * Mathf.PI / 360) * Time.deltaTime, 0f);

        anim.SetFloat("speed", speed);
    }

    // pythong appeared again, update memory. adjust steps.
    private void updatePythonPosition()
    {
        Debug.Log("Update Position!");
        pythonPosition = GameObject.FindGameObjectWithTag("Python").transform.position;
        //a swallow clone.
        pythonPosition = new Vector3(pythonPosition.x, pythonPosition.y, pythonPosition.z);
    }

    // nothing happened, just idle and random walk.
    private void executeCasualAction()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = transform.position + new Vector3(-speed * Mathf.Sin(angle * 2 * Mathf.PI / 360) * Time.deltaTime, speed * Mathf.Cos(angle * 2 * Mathf.PI / 360) * Time.deltaTime, 0f);
        panicLevel = 0;
        anim.SetFloat("speed", speed);
    }

}
