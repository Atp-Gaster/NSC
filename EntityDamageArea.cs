using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageArea : MonoBehaviour
{
    
    public bool inDamageArea = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inDamageArea = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inDamageArea = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        
    }
}
