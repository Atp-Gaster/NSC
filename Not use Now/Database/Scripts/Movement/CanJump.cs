using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanJump : MonoBehaviour
{
    Rigidbody body;
    public float jumpPower = 400;
    public string GroundLayer = "Ground";
    float graceTimer;
    bool justJumped;

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

        if (Input.GetButtonDown("Jump"))
        {
            if (graceTimer > 0 && !justJumped)
            {
                body.AddForce(new Vector3(0, jumpPower , 0 ));
                graceTimer = -1;
                justJumped = true;
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
            justJumped = false;
        }
    }
}
