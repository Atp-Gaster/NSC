using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public MainController mc;
    public List<NPC> npcs;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TalkwithNPC(int npcID)
    {
        //Debug.Log("in");
        DialogueEvent curEvent = npcs[npcID].dialogueEvent[npcs[npcID].currEventID];
        Quest curQuest;

        Debug.Log(mc.player.storyProgress + " " + curEvent.requiredStoryProgress);

        if (mc.player.storyProgress == curEvent.requiredStoryProgress)
        {
            
            if (curEvent.questID == -1) //not a quest
            {
                mc.TriggerDialog(curEvent, npcs[0]);
                //npcs[npcID].currEventID++;
            }
            else //is a quest
            {
                curQuest = mc.questManager.questList[curEvent.questID];
                if (curQuest.status == 0) //not yet in this quest
                {
                    curQuest.status++;
                    npcs[npcID].currEventID++;
                    mc.TriggerDialog(curEvent, npcs[0]);
                }
                else if (curQuest.status == 1 && curQuest.currCollect >= curQuest.passCollect) // complete quest
                {
                    
                    npcs[npcID].currEventID++;
                    curEvent = npcs[npcID].dialogueEvent[npcs[npcID].currEventID];
                    mc.TriggerDialog(curEvent, npcs[0]);
                    curQuest.status++;
                    npcs[npcID].currEventID++;
                }
                else if (curQuest.status == 1) //quest in progress
                {
                    
                    mc.TriggerDialog(curEvent, npcs[0]);
                    npcs[npcID].currEventID++;
                    //listNPC[npcID].currEventID++;
                }
            }

        }
        else //if (mc.player.storyProgress > curEvent.requiredStoryProgress )
        {
            mc.TriggerDialog(npcs[npcID].dialogueEvent[npcs[npcID].currEventID - 1], npcs[0]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
