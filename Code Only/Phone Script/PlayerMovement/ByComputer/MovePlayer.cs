using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    Rigidbody2D Rigidbody;
    Touch touch;
    Vector3 touchPosition, whereToMove;
    bool isMoving = false;

    [SerializeField] float moveSpeed = 5f;//Change Speed
    
	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	void Start () {
        Rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	void Update ()
    {

		if (isMoving)
			currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
		
		if (Input.touchCount > 0)
        {
			touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began)
            {
				previousDistanceToTouchPos = 0;
				currentDistanceToTouchPos = 0;
				isMoving = true;
				touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
				touchPosition.z = 0;
				whereToMove = (touchPosition - transform.position).normalized;
                Rigidbody.velocity = new Vector2 (whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
			}
		}

		if (currentDistanceToTouchPos > previousDistanceToTouchPos) {
			isMoving = false;
            Rigidbody.velocity = Vector2.zero;
		}

		if (isMoving)
			previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
	}
}
