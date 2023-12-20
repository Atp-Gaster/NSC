using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public MainController mc;
    public List<Quest> questList;
    void Start()
    {
        
    }

    public void QuestCollectUp(int questID)
    {
        
        //if (questList[questID].passCollect < questList[questID].currCollect)
            questList[questID].currCollect++;

        //Debug.Log(questList[questID].currCollect);
        //else
        //mc.npcManager.listNPC[questList[questID].npcID].currEventID++;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
