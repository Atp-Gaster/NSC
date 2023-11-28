using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	public Transform target;

	void Update()
	{
		if(target==null)
		{
			Debug.LogWarning("target is missing from "+this.name);
			return;
		}

		Quaternion lookAtRotation = Quaternion.LookRotation(target.position - transform.position,Vector3.up);

//		Vector3 hackTargetPos = target.position;
//		hackTargetPos.y = transform.position.y;
//		Quaternion lookAtRotation = Quaternion.LookRotation(hackTargetPos - transform.position);

		transform.rotation = lookAtRotation;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;

		Gizmos.DrawWireSphere(target.position,2f);

//		Vector3 hackTargetPos = target.position;
//		hackTargetPos.y = transform.position.y;
//		Gizmos.DrawWireSphere(hackTargetPos,.5f);
	}
}
