using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_Dash : MonoBehaviour
{
    Rigidbody body;
    public float Dash = 400;
    public string GroundLayer = "Ground";
    float graceTimer;
    bool justDash;

    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!body)
        {
            Debug.LogWarning("Rigidbody component is missing from " + this.name);
            return;
        }

        if (graceTimer > 0)
        {
            graceTimer = graceTimer - Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (graceTimer > 0 && !justDash)
            {
                body.AddForce(new Vector3( Dash , 0 , 0 ));
                graceTimer = -1;
                justDash = true;
            }
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(GroundLayer))
        {
            graceTimer = 0.3f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(GroundLayer))
        {
            graceTimer = 0.3f;
            justDash = false;
        }
    }
}
