using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;

public class AgentEnvironment : MonoBehaviour
{
    public GreatGrandpaAgent greatGrandpaAgent;
    //public PlayerAgentMovement playerAgent;
    public PlayerMovement playerAgent;
    public TextMeshPro cumulativeRewardText;
    public float playerHealth = 500;
    public float playerMaxHealth = 500;
    //public float agentHealth;
    // Start is called before the first frame update
    public void ResetArea()
    {
        playerHealth = playerMaxHealth;
        PlaceGreatGrandpa();
        PlacePlayer();
        
    }

    private void Start()
    {
        ResetArea();
    }

    private void Update()
    {
        // Update the cumulative reward text
        cumulativeRewardText.text = greatGrandpaAgent.GetCumulativeReward().ToString("0.00");
    }

    private void PlaceGreatGrandpa()
    {
        Rigidbody rigidbody = greatGrandpaAgent.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        greatGrandpaAgent.transform.position = ChooseRandomPosition(transform.position, 20, -20, 20, -20); //+ Vector3.up * .5f;
        //greatGrandpaAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
    }
    private void PlacePlayer()
    {
        Rigidbody rigidbody = playerAgent.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        playerAgent.transform.position = ChooseRandomPosition(transform.position, 20, -20, 20, -20); //+ Vector3.up * .5f;
        //playerAgent.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }



    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;
        float angle = minAngle;

        if (maxRadius > minRadius)
        {
            // Pick a random radius
            radius = UnityEngine.Random.Range(minRadius, maxRadius);
        }

        if (maxAngle > minAngle)
        {
            // Pick a random angle
            angle = UnityEngine.Random.Range(minAngle, maxAngle);
        }

        // Center position + forward vector rotated around the Y axis by "angle" degrees, multiplies by "radius"
        return center + Vector3.forward * radius + Vector3.right * angle;//return center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
    }

}
