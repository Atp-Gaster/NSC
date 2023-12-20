using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntityGroup : MonoBehaviour
{
    public enum ActionAfterKillAll
    { 
        None,
        AddStoryProgress
    }

    public ActionAfterKillAll actionAfterKillAll;

    public float area;
    public GameObject entityPrefab;
    
    public int startEntityAmount;
    public bool dontRespawn;
    public float secToRespawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject entity;
        for (int i = 0; i < startEntityAmount; i++)
        {
            Vector3 spawnPos = Random.insideUnitCircle * area;
            spawnPos = new Vector3(spawnPos.x + transform.position.x, transform.position.y, spawnPos.y + transform.position.z);

            entity = Instantiate(entityPrefab, spawnPos, Quaternion.identity);
            entity.transform.SetParent(transform);
            entity.GetComponent<EachEntity>().habitatCenter = transform.position;
            entity.GetComponent<EachEntity>().area = area;
            if (secToRespawn > 0)
            {
                entity.GetComponent<EachEntity>().secToRespawn = secToRespawn;
            }
            //entity.GetComponent<EachEntity>().health = 
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (actionAfterKillAll == ActionAfterKillAll.AddStoryProgress)
        {
            if (transform.childCount <= 0)
            {
                //Debug.Log("gere");
                FindObjectOfType<MainController>().player.storyProgress++;
                Destroy(gameObject);
            }
        }
    }
}
