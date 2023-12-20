using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DisplayWorldPos : MonoBehaviour 
{
    public Vector3 worldPos;
    public Vector3 localPos;

    void Update() 
	{
        worldPos = this.transform.position;
        worldPos.x = Mathf.Round(worldPos.x * 100) / 100;
        worldPos.y = Mathf.Round(worldPos.y * 100) / 100;
        worldPos.z = Mathf.Round(worldPos.z * 100) / 100;
        this.gameObject.name = "WorldPos : " + worldPos.x + "," + worldPos.y + "," + worldPos.z;

        localPos = this.transform.localPosition;
    }
}