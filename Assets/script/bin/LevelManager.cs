using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject wormColorBackground;
    public Text groundGameOverText;
    public Text wormGameOverText;

    public GameObject environmentCamera;
    //[SyncVar]
    public Vector3 wormPosition;
    //[SyncVar]
    public bool wormOnGround;
    //[SyncVar]
    public float depth;
    //public float depth;

    public float maxDepth = 100;  // the maximum depth of player
    private  float maxBlurDepth = 20;  // the maximum depth of blur
    private float maxBlurLevel = 5;  // the maximum blur level
    private int hideCameraZ = 1;
    private GameObject worm;
    private GameObject wormBackground;

    // Use this for initialization
    void Start () {
        wormBackground = Instantiate(wormColorBackground, new Vector3(0f,0f,0f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

    }

    // Update is called once per frame
    void Update () {
        setWormLayer();
        changeWormBackgroundColor();
    }

    void changeWormBackgroundColor()
    {
        if (depth == -1) {
            wormBackground.GetComponent<SpriteRenderer>().color =Color.Lerp(wormBackground.GetComponent<SpriteRenderer>().color, new Color(27f/255, 37f/255, 26f/255),Time.deltaTime);
        }
        else if (depth == 0)
        {
            wormBackground.GetComponent<SpriteRenderer>().color = Color.Lerp(wormBackground.GetComponent<SpriteRenderer>().color, new Color(72f/255, 98f/255, 69f/255), Time.deltaTime);
        }
        else
        {
            wormBackground.GetComponent<SpriteRenderer>().color = Color.Lerp(wormBackground.GetComponent<SpriteRenderer>().color, new Color(91f/255, 94f/255, 144f/255), Time.deltaTime);
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

    //show text
    //reest players and npc

    public void isWormWin(bool isWormWin) {
        Debug.Log("set worm win");
        this.showGameOver(isWormWin);
        Invoke("resetAll", 5);
        //yield return new WaitForSeconds(5);
        //this.();

    }

    private void resetAll() {
        this.resetNPC();
        this.resetPlayer();
        this.resetText();

    }
    
    //show game over text
    private void showGameOver(bool isWormWin) {
        if (isWormWin)
        {
            wormGameOverText.text = "You Win! \n Game will be rest after 10seconds";
            groundGameOverText.text = "You Lost! \n Game will be rest after 10seconds";
        }
        else if (!isWormWin) {
            groundGameOverText.text = "You Win! \n Game will be rest after 10seconds";
            wormGameOverText.text = "You Lost! \n Game will be rest after 10seconds";
        }
    }

    private void resetPlayer() {

        //reset worm's position and status
        GameObject worm= GameObject.FindGameObjectWithTag("Worm");
        GameObject wormHead = worm.transform.Find("head").gameObject;
        wormHead.transform.position = new Vector3(0,0,0);
        wormHead.GetComponent<WormController>().reset();


        //reset ground player's position and status
        //TODO call ji feng bu function
    }

    //reset npc
    private void resetNPC() {

        //reset npc
        GameObject NPCSpawn = GameObject.FindGameObjectWithTag("NPCSpawn");
        NPCSpawn.GetComponent<NpcSpawn>().spawnAllNPC();
        
    }

    //reset game over text
    private void resetText() {
        wormGameOverText.text = "";
        groundGameOverText.text = "";
    }

    private void setWormLayer() {
        GameObject worm = GameObject.FindGameObjectWithTag("Worm");
        Transform head = worm.transform.Find("head");
        

        if (depth < 1)
        {
            head.gameObject.layer = 14;
            foreach (Transform child in head)
            {
                if (null == child)
                {
                    continue;
                }
                child.gameObject.layer = 14;
            }

            //worm.layer = 14;
            //worm.GetComponent<Renderer>().enabled = false;
        }
        else {
            //worm.layer = 13;

            head.gameObject.layer = 13;
            foreach (Transform child in head)
            {
                if (null == child)
                {
                    continue;
                }
                child.gameObject.layer = 13;
            }
        }
        
    }
}
