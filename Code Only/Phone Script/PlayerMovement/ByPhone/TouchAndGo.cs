using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndGo : MonoBehaviour {
	
	[SerializeField]
	float moveSpeed = 5f;
   // [SerializeField]
    //float DashSpeed = 5f;


    Animator Anim;
    SpriteRenderer Render;
    Rigidbody2D rb;

	Touch touch;
    //Touch DoubleTap;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	void Start ()
    {
		rb = GetComponent<Rigidbody2D> ();
        Anim = this.GetComponent<Animator>();
        Render = this.transform.Find("Render").GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        Vector3 PlayerXYZ = this.transform.localPosition;

        if (isMoving)
			currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
		
		if (Input.touchCount > 0)
        {
			touch = Input.GetTouch (0);
            //DoubleTap = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began)
            {
                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                whereToMove = (touchPosition - transform.position).normalized;
                rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);

                if (touchPosition.x > PlayerXYZ.x)
                {
                    Anim.SetBool("Walk", true);
                    Render.flipX = false;
                }
                else if (touchPosition.x < PlayerXYZ.x)
                {
                    Render.flipX = true;
                    Anim.SetBool("Walk", true);

                }
            }
            //Dash by Two tap
           // else if (DoubleTap.phase == TouchPhase.Began)
           // {
              //  previousDistanceToTouchPos = 0;
              //  currentDistanceToTouchPos = 0;
              //  isMoving = true;
              //  touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
              //  touchPosition.z = 0;
              //  whereToMove = (touchPosition - transform.position).normalized;
              //  rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
              //
              //  if (touchPosition.x > PlayerXYZ.x)
              //  {
              //      Anim.SetBool("Walk", true);
              //      Render.flipX = false;
              //  }
              //  else if (touchPosition.x < PlayerXYZ.x)
              //  {
              //      Render.flipX = true;
              //      Anim.SetBool("Walk", true);

             //   }
            //}


        }

		if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
			isMoving = false;
			rb.velocity = Vector2.zero;
            Anim.SetBool("Walk", false);
        }

       

        if (isMoving)
			previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
	}
}
