using UnityEngine;
using System.Collections;

public class WormUICameraBudFixer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //There is a bug
        //After build, the WormUICamera doesn't
        //To fix it, change any field in the Camera, then change it back
        //I don't know why, no solution on Google
        //DO NOT touch these code unless you know how to fix this bug
        GameObject cameraObject = GameObject.FindGameObjectWithTag("WormUICamera");
        Camera camera = cameraObject.GetComponent<Camera>();
        camera.depth = -2;
        camera.depth = -1;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
