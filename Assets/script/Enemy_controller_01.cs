using UnityEngine;
using System.Collections;

public class Enemy_controller_01 : MonoBehaviour {
    public string State;
    private EnemyRandomWalk randomWalkScript;
    private EnemyAlert enemyAlertScript;
    // Use this for initialization
    void Start () {
        randomWalkScript = GetComponent<EnemyRandomWalk>();
        enemyAlertScript = GetComponent<EnemyAlert>();
        randomWalkScript.Start();
        enemyAlertScript.Start();
        State = "randomWalk";
        randomWalkScript.TriggerRandomWalk();
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButtonDown("Jump")) {
        //    TriggerAlert();
        //}

        if (State == "randomWalk")
        {
            randomWalkScript.RandomWalkUpdate();
        }
        else if (State == "Alerted") {
            AlertUpdate();
        }
	}

    public void TriggerAlert() {
        randomWalkScript.stopRandomWalk();
        enemyAlertScript.TriggerAlert();
        Invoke("StartAlertSituation",1);
    }

    void StartAlertSituation() {
        State = "Alerted";
    }

    void AlertUpdate() {
        
    }
}
