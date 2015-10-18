using UnityEngine;
using System.Collections;

public class Enemy_controller_01 : MonoBehaviour {
    public int maxPanic;
    private string State;
    private EnemyRandomWalk randomWalkScript;
    private EnemyAlert enemyAlertScript;
    private OngroundAlter eye;
    private bool findPlayer;
    private int panicLevel;
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
        foreach (Transform t in transform)
        {
            if (t.name == "onground alert")
            {
                eye = t.gameObject.GetComponent<OngroundAlter>();
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
