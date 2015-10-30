
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NpcSyncPosition : NetworkBehaviour
{

    [SyncVar]
    private Vector3 syncPos; //server will automatically transmit this veriable to all clients when it changes with the SyncVar tag


    private Transform myTransform;
    [SerializeField]
    float lerpRate = 5;

    void Start() {
        myTransform = this.transform;

    }

        // Update is called once per frame
        void FixedUpdate()
    {
        //tell server the new position
        TransmitPosition();

        //if not local player, and position changes, then get the new position
        LerpPosition();
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

    //only run on server
    //DO NOT remove the 'Cmd' of the function name
    //tell the server of the players position
    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
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
        }
    }
}
