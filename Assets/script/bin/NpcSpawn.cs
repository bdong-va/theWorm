using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NpcSpawn : NetworkBehaviour
{
    [SerializeField] GameObject NPC;

    Vector3 position1 = new Vector3(0, 0, 0);
    Vector3 position2 = new Vector3(1, 0, 0);
    Vector3 position3 = new Vector3(2, 0, 0);
    Vector3 position4 = new Vector3(-1, 0, 0);
    Vector3 position5 = new Vector3(-2, 0, 0);

    private ArrayList positionList = new ArrayList();

    //void Start()
    //{

    //}

    public override void OnStartServer()
    {
        positionList.Add(position1);
        positionList.Add(position2);
        positionList.Add(position3);
        positionList.Add(position4);
        positionList.Add(position5);

        Debug.Log("test");
        spawnAllNPC();
        //for (int i = 0; i < positionList.Count; i++) {
        //    spawnNPC(positionList.)
        //}
    }

    public void spawnAllNPC() {


        if (isServer)
        {
            //remove all npcs
            GameObject[] npcList = GameObject.FindGameObjectsWithTag("EnemyAnim");

            foreach (GameObject npc in npcList)
            {
                Destroy(npc);
            }


            foreach (Vector3 v in positionList)
            {
                spawnNPC(v);
            }
        }
    }


    void spawnNPC(Vector3 position)
    {
        GameObject npc = GameObject.Instantiate(NPC, position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(npc);
    }
}
