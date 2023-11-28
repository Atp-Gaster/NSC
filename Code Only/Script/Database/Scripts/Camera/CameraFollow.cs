using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public bool useSmoothFollow;
	public float smoothFollow;
	Transform self;

	void Start()
	{
		self = this.transform;
	}

	void FixedUpdate ()
	{
		if(target == null)
		{
			Debug.LogWarning("target is missing from "+this.name);
			return;
		}
		if(useSmoothFollow)
		{
			Vector3 desirePosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.Slerp(self.position,desirePosition,smoothFollow*Time.deltaTime);
			self.position = smoothedPosition;

			var lookRot = Quaternion.LookRotation(target.position - self.position,Vector3.up);
			self.rotation = Quaternion.Lerp(self.rotation,lookRot,5*Time.deltaTime);
		}
		else
		{
			self.position = target.position + offset;
			self.LookAt(target);
		}
	}
}
