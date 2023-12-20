using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGWeapon : MonoBehaviour
{
    float speed = 10;
    float atk = 400;
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            transform.parent.parent.parent.GetComponent<AgentEnvironment>().playerHealth -= atk;
            //transform.parent.parent.GetComponent<GreatGrandpaAgent>().AddReward(atk / 20);
        }
        Destroy(gameObject);
    }

    void FixedUpdate () {
        if (transform.GetComponent<SpriteRenderer>().flipX)
            transform.localPosition += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        else
            transform.localPosition += new Vector3(speed * Time.fixedDeltaTime * -1, 0, 0);
    }
}
