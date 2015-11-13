using UnityEngine;
using System.Collections;

public class testPython : MonoBehaviour {

	public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (v != 0.0f) {
			v = v / Mathf.Abs(v);
			Debug.Log (v);
			GetComponent<Rigidbody2D> ().AddForce (transform.up.normalized * speed * v);
		}
		transform.Rotate(0, 0, -h * speed * Time.deltaTime * 90);
	}
}
