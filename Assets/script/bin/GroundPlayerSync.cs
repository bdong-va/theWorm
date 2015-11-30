using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GroundPlayerSync : NetworkBehaviour
{

    [SyncVar]
    private Vector3 syncPos; //server will automatically transmit this veriable to all clients when it changes with the SyncVar tag
    [SyncVar]
    private Quaternion syncRotation;
    [SyncVar]
    private float syncSpeed;
    [SyncVar]
    private bool SwitchyActive;

    //[SyncVar]
    //bool ability1 = false;

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 5;
    public Rigidbody2D abilityTest1;

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell server the new position
        TransmitPosition();

        //if not local player, and position changes, then get the new position
        LerpData();

        //if (ability1)
        //{
        //    UseAbility();
        //    ability1 = false;
        //    CmdTestAbility(false);
        //}
    }

    void LerpData() {
        //if not local player, and position changes, then get the new position
        LerpPosition();
        LerpRotation();
        LerpSpeed();
        if (SwitchyActive)
        {
            switchy();
            //CmdProvideSwitchyToServer(false);
        }
    }
    //only set position for the player not local
    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            //set position lerp
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
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
    void LerpSpeed()
    {
        if (!isLocalPlayer)
        {
            //set speed lerp
            gameObject.GetComponent<GroundPlayerBodyController>().currentSpeed = syncSpeed;
        }
    }
    //only set rotation for the player not local
    public void activeSwitchy()
    {
        SwitchyActive = true;
    }

    [ClientCallback]
    void switchy()
    {
        Debug.Log("Switchy!");
        if (!isLocalPlayer)
        {   // find random NPC
            Debug.Log("not LocalPlayer!");
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("EnemyAnim");
            int r = (int)Mathf.Floor(Random.Range(0, npcs.Length));
            // switch position with him.
            Vector3 positionA = new Vector3( npcs[r].transform.position.x, npcs[r].transform.position.y, npcs[r].transform.position.z);
            Quaternion rotatationA =  new Quaternion(npcs[r].transform.rotation.x, npcs[r].transform.rotation.y, npcs[r].transform.rotation.z, npcs[r].transform.rotation.w);
            npcs[r].transform.position = transform.position;
            npcs[r].transform.rotation = transform.rotation;
            transform.position = positionA;
            transform.rotation = rotatationA;
            SwitchyActive = false;
        }
    }


    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
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
    //tell the server of the players speed
    [Command]
    void CmdProvideSpeedToServer(float speed)
    {
        syncSpeed= speed;
    }

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players speed
    [Command]
    void CmdProvideSwitchyToServer(bool Switchy)
    {
        SwitchyActive = Switchy;
    }

    //only run on clients
    //tell server the position
    [ClientCallback]
    void TransmitPosition()
    {
        //only work for local player
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position);
            CmdProvideRotationToServer(myTransform.rotation);
            //set to speed script
            CmdProvideSpeedToServer(gameObject.GetComponent<GroundPlayerController>().currentSpeed);
            CmdProvideSwitchyToServer(SwitchyActive);
        }
    }

    ////only run on clients
    ////tell server the player use the ability
    //[ClientCallback]
    //public void testAbility()
    //{
    //    if (isLocalPlayer)
    //    {
    //        CmdTestAbility(true);
    //    }
    //}

    //[Command]
    //void CmdTestAbility(bool ifUse)
    //{
    //    ability1 = ifUse;
    //}


    //void UseAbility()
    //{

    //    Rigidbody2D testAbilityInstance = Instantiate(abilityTest1, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

    //}
}


