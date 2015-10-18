using UnityEngine;
using System.Collections;

public class Enemy_controller_01 : MonoBehaviour {
    public int maxPanic;
    public float PanicSpeed;
    private string State;
    private EnemyRandomWalk randomWalkScript;
    private EnemyAlert enemyAlertScript;
    private OngroundAlter eye;
    private Transform enemy_anim;
    private Animator anim;
    private bool findPlayer;
    private int panicLevel;
    private GameObject player;
    // Use this for initialization
    void Start () {
        randomWalkScript = GetComponent<EnemyRandomWalk>();
        enemyAlertScript = GetComponent<EnemyAlert>();
        randomWalkScript.Start();
        enemyAlertScript.Start();
        State = "randomWalk";
        randomWalkScript.TriggerRandomWalk();
        panicLevel = 0;
        InvokeRepeating("TryToCalm", 2, 2);
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform t in transform)
        {
            if (t.name == "onground alert")
            {
                eye = t.gameObject.GetComponent<OngroundAlter>();
            }
            else if (t.CompareTag("EnemyAnim"))
            {
                enemy_anim = t;
                anim = t.GetComponent<Animator>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (State == "randomWalk")
        {
            randomWalkScript.RandomWalkUpdate();
        }
        else if (State == "panic") {
            AlertUpdate();
        }
        findPlayer = eye.SeePython();
        if (findPlayer) {
            panicLevel = maxPanic;
        }
        anim.SetInteger("panicLevel", panicLevel);
	}

    public void TriggerAlert() {
        randomWalkScript.stopRandomWalk();
        enemyAlertScript.TriggerAlert();
        Invoke("StartPanic", 1);
    }

    void StartPanic() {
        State = "panic";
        
    }

    void AlertUpdate() {
        float currentAngle = -90 + Mathf.Atan2((transform.position.y - player.transform.position.y), (transform.position.x - player.transform.position.x)) * 180 / Mathf.PI;
        Debug.Log(currentAngle);
        enemy_anim.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        transform.position = transform.position + new Vector3(-PanicSpeed * Mathf.Sin(currentAngle * 2 * Mathf.PI / 360) * Time.deltaTime, PanicSpeed * Mathf.Cos(currentAngle * 2 * Mathf.PI / 360) * Time.deltaTime, 0f);
    }

    public bool IsCalm() {
        if (State == "alerted" || State == "panic")
            return false;
        else
            return true;
    }

    void TryToCalm() {
        if (panicLevel > 0)
        {
            panicLevel = panicLevel - 1;
        }
        else if (panicLevel == 0 ){
            if (State != "randomWalk") {
                State = "randomWalk";
                randomWalkScript.TriggerRandomWalk();
            }
        }
    }
}
