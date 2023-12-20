using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToTarget : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 distancePos; //= new Vector3(0, 24, -40);
    public float zoomPower;
    public static bool isZooming = false;
    Vector3 zoomDistancePos;

    public static bool isFocusing = false;
    public static float secToFocus = 2;
    public static Transform focusTarget;


    //bool isFocusing2 = false;

    public void ClickZoomOut()
    {
        isZooming = !isZooming;
    }

    private void Start()
    {
        distancePos = transform.position - target.position;
    }

    IEnumerator FocusOnTarget (float secToFocus)
    {

        yield return new WaitForSeconds(secToFocus);
        //isFocusing2 = false;
        isFocusing = false;
        focusTarget = null;

    }

    

    void Update()
    {
        if (isZooming) //zooming
        {
            zoomDistancePos = new Vector3(distancePos.x, distancePos.y + (0.6f * zoomPower), distancePos.z - zoomPower);
            Vector3 targetPosition = target.TransformPoint(zoomDistancePos);

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        }
        else if (isFocusing) //focusing
        {
            
            // Define a target position above and behind the target transform
            Vector3 targetPosition = focusTarget.TransformPoint(distancePos);

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            StartCoroutine(FocusOnTarget(secToFocus));

        }
        else //normal
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(distancePos);

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        
    }
}
