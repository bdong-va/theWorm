using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class LevelManager : MonoBehaviour {

    public GameObject environmentCamera;
    //public float depth;

    public float maxDepth = 100;  // the maximum depth of player
    public float maxBlur = 20;  // the maximum depth of blur

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //this.setBlur(depth);
	}


    /// <summary>
    /// author: Xingze
    /// set blur value 
    /// </summary>
    /// depth: depth of player
    public void setBlur(float depth) {

        //rand of blurSize is 0-10
        //convert depth to 0 - 10
        float blurLevel = depth /(maxDepth /maxBlur) * 10;  
        Debug.Log("blurLevel = " + blurLevel);
        environmentCamera.GetComponent<BlurOptimized>().blurSize = blurLevel;
    }

    
}
