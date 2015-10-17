using UnityEngine;
using System.Collections;

public class EnemyRandomWalk : MonoBehaviour {
    public float maxSpeed;
    public float timeGapMax;
    public float timeGapMin;
    public float walkChance;

    private Animator anim;
    private float CurrentSpeed;
    private float angle;
    private bool isWalking;
    private GameObject enemy_anim;

	// Use this for initialization
	public void Start () {
        isWalking = false;
        CurrentSpeed = maxSpeed;
        angle = 0f;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemyAnim")) {
                enemy_anim = child.gameObject;
            }
        }
        if (enemy_anim == null) {
            enemy_anim = this.gameObject;
        }
        anim = enemy_anim.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
   }

	public void RandomWalkUpdate(){
		if (isWalking)
		{
			CurrentSpeed = maxSpeed;
		}
		else {
			CurrentSpeed = 0;
		}
		transform.position = transform.position + new Vector3(-CurrentSpeed * Mathf.Sin(angle * 2 * Mathf.PI / 360) * Time.deltaTime, CurrentSpeed * Mathf.Cos(angle * 2 * Mathf.PI / 360)* Time.deltaTime, 0f);
		anim.SetFloat("speed", CurrentSpeed);
	}

    public void TriggerRandomWalk() {
			// check if this round need to walk
			if (Random.value < walkChance) {
				isWalking = true;
				angle = Random.Range (0f, 180f);
                enemy_anim.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            } else {
				isWalking = false;
			}
        Invoke("TriggerRandomWalk", Random.Range(timeGapMin,timeGapMax));
    }
	void stopRandomWalk(){
        CancelInvoke("TriggerRandomWalk");
	}

}
