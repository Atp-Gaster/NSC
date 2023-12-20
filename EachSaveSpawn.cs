using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachSaveSpawn : MonoBehaviour
{
    public int SaveSpawnID;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);


        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }
}
