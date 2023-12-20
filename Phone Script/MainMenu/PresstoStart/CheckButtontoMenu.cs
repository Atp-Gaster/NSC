using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButtontoMenu : MonoBehaviour
{
    public Transition Starts;
    
    
       
    
    // Update is called once per frame
    void Update()
    {
            GameObject g = GameObject.FindGameObjectWithTag("NextButton");
            if (((Input.touchCount == 1) && ((Input.GetTouch(0).phase == TouchPhase.Began)) || (Input.GetKey(KeyCode.Space))))
            {
                // GameObject.FindGameObjectWithTag("NextButton").GetComponent<Transition>().LoadNextLevel();
                Starts = g.GetComponent<Transition>();
                Starts.Start = true;
                Starts.Sceennumber = 0;

            }
        
               
            if (Input.touchCount > 0) // ตรวจสอบว่ามีการ touch screen หรือ ไม่
            {
                // ทำการ loop จำนวนนิ้วที่ Touch Screen
                for (int figerIndex = 0; figerIndex < Input.touchCount; figerIndex++)
                {
                // GameObject.FindGameObjectWithTag("NextButton").GetComponent<Transition>().LoadNextLevel();
                Starts = g.GetComponent<Transition>();
                Starts.Start = true;
                Starts.Sceennumber = 0;

            }
            }
        
    }
        
}
