using UnityEngine;
using System.Collections;

public class Enemy_controller_01 : MonoBehaviour {
    private string State;
    private EnemyRandomWalk randomWalkScript; 
    // Use this for initialization
    void Start () {
        randomWalkScript = GetComponent<EnemyRandomWalk>();
        randomWalkScript.Start();
        State = "randomWalk";
        randomWalkScript.TriggerRandomWalk();
    }
	
	// Update is called once per frame
	void Update () {
        if (State == "randomWalk") {
            randomWalkScript.RandomWalkUpdate();
        }
	}
}
