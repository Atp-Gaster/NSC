using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeingRangeSystem : MonoBehaviour
{
    public KamodBehaviour behaviour;
    // Start is called before the first frame update
    void Start()
    {
        behaviour.aiPath.canSearch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //behaviour.aiPath.canSearch = true;
            behaviour.Seeing();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //behaviour.aiPath.canSearch = false;
            behaviour.UnSeeing();
        }
    }
}
