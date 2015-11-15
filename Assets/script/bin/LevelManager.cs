using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class LevelManager : MonoBehaviour {

    public GameObject environmentCamera;
    public Vector3 wormPosition;
    public bool wormOnGround;
    //public float depth;

    public float maxDepth = 100;  // the maximum depth of player
    private  float maxBlurDepth = 20;  // the maximum depth of blur
    private float maxBlurLevel = 5;  // the maximum blur level
    private int hideCameraZ = 1;
    private GameObject worm;


    // Use this for initialization
    void Start () {
        worm = GameObject.FindGameObjectWithTag("Python");
        if (worm != null)
        {
            wormOnGround = worm.GetComponent<WormController>().onground;
            wormPosition = worm.transform.position;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        //this.setBlur(depth);
        if (worm != null)
        {
            wormOnGround = worm.GetComponent<WormController>().onground;
            wormPosition = worm.transform.position;
        }
        else
        {
            worm = GameObject.FindGameObjectWithTag("Python");
        }
    }


    /// <summary>
    /// author: Xingze
    /// set blur value 
    /// </summary>
    /// depth: depth of player
    public void setBlur(float depth) {
        float blurLevel = 0;
        //rand of blurSize is 0-10
        //convert depth to 0 - 10
        //float blurLevel = depth /(maxDepth /maxBlur)/ maxBlur * 10;  
        blurLevel = depth / maxBlurDepth * maxBlurLevel;
        if (depth <= -1) {
            blurLevel = 10;
        } else {
            blurLevel = 0;
        }

        //TODO: change camera to ground when worm on the ground
        //if (environmentCamera.transform.position.z == hideCameraZ) {
        //       environmentCamera.transform.position = new Vector3(environmentCamera.transform.position.x, environmentCamera.transform.position.y, -10);
        //}
        environmentCamera.GetComponent<BlurOptimized>().blurSize = blurLevel;
    }

    
}
