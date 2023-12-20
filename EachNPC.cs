using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EachNPC : MonoBehaviour
{
    public int NPCId;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);


            /*


            GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
            NPCManager npcManager = GameObject.FindObjectOfType<NPCManager>();

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            screenPos = new Vector3(screenPos.x, screenPos.y + 370, screenPos.z);

            a = Instantiate(Balloon, screenPos, Quaternion.identity);
            a.transform.SetParent(canvas.transform);
            a.GetComponent<Button>().onClick.AddListener(delegate 
            { 
                npcManager.TalkwithNPC(NPCId);
                Destroy(a);
            });
            */
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            //if (a != null)
            //    Destroy(a);
        }
        

    }
}
