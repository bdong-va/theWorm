using UnityEngine;
using System.Collections;

public class depthIndicator : MonoBehaviour {
    private Animator anim;
    private GameObject levelManager;
   
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    // Update is called once per frame
    void Update () {
        anim.SetFloat("depth",levelManager.GetComponent<LevelManager>().depth);
	}
}
