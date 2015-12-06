using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {

    [SyncVar] private Vector3 syncPos; //server will automatically transmit this veriable to all clients when it changes with the SyncVar tag
    [SyncVar] private Quaternion syncRotation;
    [SyncVar]
    bool ability1 = false;

    //worm's depth information , do not need to sync back to client
    [SyncVar]
    public float syncDepth = 0;
    [SyncVar]
    public bool syncOnground;

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 5;
    public GameObject abilityTest1;

    [SyncVar]
    public bool isWormWin = false;
    [SyncVar]
    public bool isGroundPlayerWin = false;


    // Update is called once per frame
    void FixedUpdate () {
        //tell server the new position
        TransmitPosition();

        //if not local player, and position changes, then get the new position
        LerpPosition();
        LerpRotation();
        LerpOnGround();
        //LerpDepth();

        //if (!isServer)
        //{
        if (ability1)
            {
                LerpAbility1();
            }

        if (isWormWin) {
            LerpWromWin();            
        }

        if (isGroundPlayerWin) {
            LerpGroundWin();
        }

        
        //}
    }


    void updateLevelManager(bool result) {
        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager.GetComponent<LevelManager>().isWormWin(result);
    }
    //only set position for the player not local
    void LerpPosition() {
        if (!isLocalPlayer) {
            //set position lerp
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
            levelManager.GetComponent<LevelManager>().wormPosition = syncPos;
            levelManager.GetComponent<LevelManager>().depth = syncDepth;
        }
    }

    //only set rotation for the player not local
    void LerpRotation()
    {
        if (!isLocalPlayer)
        {
            //set position lerp
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }


    //only set rotation for the player not local
    void LerpDepth()
    {
        if (!isLocalPlayer)
        {
            GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
            levelManager.GetComponent<LevelManager>().depth = syncDepth;
        }
    }

    //only call ability when the player is not local 
    void LerpAbility1()
    {
        if (!isLocalPlayer)
        {
            //set position lerp
            UseAbility();
            CmdTestAbility(false);
        }
    }

    void LerpOnGround() {

        if (!isLocalPlayer)
        {
            GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
            levelManager.GetComponent<LevelManager>().wormOnGround = syncOnground;
        }
    }

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;

        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager.GetComponent<LevelManager>().wormPosition = pos;

    }

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvideRotationToServer(Quaternion rotation)
    {
        syncRotation = rotation;
    }


    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvideDepthToServer(float depth)
    {
        syncDepth = depth;
        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager.GetComponent<LevelManager>().depth = depth;
    }

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvideOnGroundToServer(bool onground)
    {
        syncOnground = onground;
        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager.GetComponent<LevelManager>().wormOnGround= onground;
    }


    //only run on clients
    //tell server the position
    [ClientCallback]
    void TransmitPosition() {
        //only work for local player
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position);
            CmdProvideRotationToServer(myTransform.rotation);
            CmdProvideDepthToServer(gameObject.transform.Find("head").gameObject.GetComponent<WormController>().depth);
            Debug.Log("depth = " + gameObject.transform.Find("head").gameObject.GetComponent<WormController>().depth);
            CmdProvideOnGroundToServer(gameObject.transform.Find("head").gameObject.GetComponent<WormController>().onground);
        }
    }

    //only run on clients
    //tell server the player use the ability

   //work flow:
   //player1 use ability, then tell server, set ability = true
   //server get the notice, in update, if ability = true, then call lerpAbility let player2 use the ability
   //player2 use the ability, tell server, set ability = false on server
   //server sync the ability to all clients
    [ClientCallback]
    public  void testAbility() {
        if (isLocalPlayer)
        //{
            //CmdTestAbility(true);
        //}
        UseAbility();
        CmdTestAbility(true);
    }

    [Command]
    void CmdTestAbility(bool ifUse)
    {
        ability1 = ifUse;
    }

   [ClientCallback]
    void UseAbility()
    {
            //Debug.Log("test use ability");
            Vector3 GroundPlayerPosition = GameObject.FindGameObjectWithTag("GroundPlayer").transform.position;
            Vector3 GroundPlayerPositionWithOffset = GroundPlayerPosition + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0f); 
            Object testAbilityInstance = Instantiate(abilityTest1, GroundPlayerPositionWithOffset, Quaternion.Euler(new Vector3(0, 0, 0)));
            ability1 = false;
    }


    //////////////////////////////////////////

    //Game OVER

    //////////////////////////////////////////

    void LerpWromWin()
    {
        if (!isLocalPlayer)
        {
            //set position lerp
            //myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            gameOver(true);
            CmdWormWin(false);
            updateLevelManager(true);
        }
    }

    void LerpGroundWin()
    {
        if (!isLocalPlayer)
        {
            //set position lerp
            //myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            gameOver(false);
            CmdGroundWin(false);
            updateLevelManager(false);
        }
    }


    //[ClientCallback]
    //public void wormWin()
    //{
    //    if (isLocalPlayer)

    //        gameOver(true);
    //        CmdWormWin(true);
    //}

    //[ClientCallback]
    //public void groundWin()
    //{
    //    if (isLocalPlayer)

    //        gameOver(false);
    //        CmdGroundWin(true);
    //}

    [Command]
    void CmdWormWin(bool isWin)
    {
        isWormWin = isWin;
    }

    [Command]
    void CmdGroundWin(bool isWin)
    {
        isGroundPlayerWin = isWin;
    }



    [ClientCallback]
    public void setWormWin(bool isWormWin)
    {
        if (isLocalPlayer)
            //{
            //CmdTestAbility(true);
            //}
            gameOver(isWormWin);
            if (isWormWin)
            {

                CmdWormWin(true);
                updateLevelManager(true);
        }
            else
            {

                CmdGroundWin(true);
                updateLevelManager(false);
        }
    }


    //end game
    ///<param name="isWormWin">
    /// ture = worm win
    /// false = ground player win
    /// </param>
    [ClientCallback]
    public void gameOver(bool isWin)
    {


        if (isWin)
        {
            Debug.Log("worm win");
            //Rigidbody2D testAbilityInstance = Instantiate(abilityTest1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            //isWormWin = true;
        }
        else
        {
            Debug.Log("ground win");
            //Rigidbody2D testAbilityInstance = Instantiate(abilityTest1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            //isGroundPlayerWin = true;
        }

        isWormWin = false;
        isGroundPlayerWin = false;
    }
}


