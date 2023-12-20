using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySight : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<EachEntity>().curTarget = other.gameObject;
            transform.parent.GetComponent<EachEntity>().isTargeting = true;
            //Debug.Log("seen");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<EachEntity>().curTarget = null;
            transform.parent.GetComponent<EachEntity>().isTargeting = false;
            //Debug.Log("seen");
        }
    }
}
