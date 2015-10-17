using UnityEngine;
using System.Collections;

public class EnemyAlert : MonoBehaviour {

	private GameObject enemy_alert;
	private Animator alert_anim;
	// Use this for initialization
	public void Start()
	{
		foreach (Transform child in transform)
		{
			if (child.CompareTag("EnemyAlert"))
			{
				enemy_alert = child.gameObject;
			}
		}
		if (enemy_alert == null)
		{
			enemy_alert = this.gameObject;
		}
		alert_anim = enemy_alert.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
	
	public void TriggerAlert()
	{
		alert_anim.SetTrigger("alert");
	}
}
