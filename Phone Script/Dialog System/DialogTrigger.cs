using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public PlayerBehavior player;
    public bool Done = false;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")//For Demo
        {
            //TriggerDialog();
        }
    }
    /*
    public void TriggerDialog(DialogueEvent dialogueEvent, NPC npc = null)
    {
        FindObjectOfType<DialogManager>().StartDialog(dialogueEvent, npc);
        
        if(Done == false)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialogueEvent, npc);
            
            Done = true;
        }

        
    }
    */


}
