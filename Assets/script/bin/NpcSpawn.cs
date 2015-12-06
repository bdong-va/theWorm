using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NpcSpawn : NetworkBehaviour
{
    [SerializeField] GameObject NPC;
    [SerializeField] float NpcNumber;
    [SerializeField] float Width;
    [SerializeField] float Height;


    private ArrayList positionList = new ArrayList();

    //void Start()
    //{

    //}

    public override void OnStartServer()
    {
        for (int i = 0; i < NpcNumber; i++)
        {
            Vector3 position = new Vector3(Random.Range(-Width / 2, Width / 2), Random.Range(-Height / 2, Height / 2), 0f);
            positionList.Add(position);
        }
       
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

   public void spawnNPC(Vector3 position , Quaternion rotation)
    {
        GameObject npc = GameObject.Instantiate(NPC, position, rotation) as GameObject;
        NetworkServer.Spawn(npc);
    }
}
