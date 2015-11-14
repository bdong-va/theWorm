using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {

    [SyncVar] private Vector3 syncPos; //server will automatically transmit this veriable to all clients when it changes with the SyncVar tag
    [SyncVar] private Quaternion syncRotation;
    [SyncVar] bool ability1 = false;
    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 5;
    public Rigidbody2D abilityTest1;

    // Update is called once per frame
    void FixedUpdate () {
        //tell server the new position
        TransmitPosition();

        //if not local player, and position changes, then get the new position
        LerpPosition();
        LerpRotation();

        if (ability1)
        {
            UseAbility();
            ability1 = false;
            CmdTestAbility(false);
        }
    }

    //only set position for the player not local
    void LerpPosition() {
        if (!isLocalPlayer) {
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

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
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

    //only run on clients
    //tell server the position
    [ClientCallback]
    void TransmitPosition() {
        //only work for local player
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position);
            CmdProvideRotationToServer(myTransform.rotation);
        }
    }

    //only run on clients
    //tell server the player use the ability
    [ClientCallback]
    public  void testAbility() {
        if (isLocalPlayer)
        {
            CmdTestAbility(true);
        }
    }

    [Command]
    void CmdTestAbility(bool ifUse)
    {
        ability1 = ifUse;
    }


    void UseAbility()
    {

            Rigidbody2D testAbilityInstance = Instantiate(abilityTest1, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

        }
}


