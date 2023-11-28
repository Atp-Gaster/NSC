using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	Rigidbody body;
	public float power =10;
	public float maxSpeed = 15;

	void Start ()
	{
		body = this.GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		if(!body)
		{
			Debug.LogWarning("Rigidbody component is missing from "+this.name);
			return;
		}

		body.maxAngularVelocity = maxSpeed;
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 direction = Vector3.Normalize(body.velocity);
		direction = new Vector3(direction.x,0,direction.z);
		if(direction == Vector3.zero)direction = new Vector3(0,0,1);

		if(horizontal != 0)
		{
			//body.AddForce(Vector3.right*horizontal*speed);
			body.AddTorque(-Vector3.forward*horizontal*power);
		}
		if(vertical != 0)
		{
			//body.AddForce(Vector3.forward*vertical*speed);
			body.AddTorque(Vector3.right*vertical*power);
		}
	}
}
