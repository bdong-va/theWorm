using UnityEngine;
using System.Collections;

public class testPython : MonoBehaviour {

	public float speed = 1f;
	public float elastifactor = 0.3f; // To stop it from scrunching up when it stops! Higher value = less scrunchy
	public Transform bod1;
	public Transform bod2;
	public Transform bod3;

	private float[] pos = {0f,0f,0f,0f,0f,0f,0f,0f}; // *** SYNC VARIABLE OVER NETWORK **

	void Start () {
		setupSegPositions ();
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		//transform.Translate (new Vector3 (h * speed, v * speed));

		transform.position = new Vector3((transform.position.x + (h * speed)), (transform.position.y + (v * speed)));

		if (Mathf.Abs(h)>elastifactor) {
			pos[6]=pos[4];
			pos[4]=pos[2];
			pos[2]=pos[0];
			Debug.Log (h.ToString());
		}

		if (Mathf.Abs(v)>elastifactor) {
			pos[7]=pos[5];
			pos[5]=pos[3];
			pos[3]=pos[1];
			Debug.Log (v.ToString());
		}

		pos [1] = transform.position.y;
		pos [0] = transform.position.x;

		updateWormSegPositions ();

		/* if (v != 0.0f) {
			v = v / Mathf.Abs(v);
			Debug.Log (v);
			GetComponent<Rigidbody2D> ().AddForce (transform.up.normalized * speed * v);
		}
		transform.Rotate(0, 0, -h * speed * Time.deltaTime * 90); */

	}

	void setupSegPositions() {
		pos [0] = transform.position.x;
		pos [1] = transform.position.y;
		pos [2] = bod1.position.x;
		pos [3] = bod1.position.y;
		pos [4] = bod2.position.x;
		pos [5] = bod2.position.y;
		pos [6] = bod3.position.x;
		pos [7] = bod3.position.y;
	}

	void updateWormSegPositions () {
		bod1.position = new Vector3 (pos [2], pos [3]);
		bod2.position = new Vector3 (pos [4], pos [5]);
		bod3.position = new Vector3 (pos [6], pos [7]);
	}
}
