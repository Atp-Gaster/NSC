using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class GreatGrandpaAgent : Agent
{
    //public MainController mc;
    private AgentEnvironment agentEnvironment;
    new private Rigidbody rigidbody;
    //private PlayerAgentMovement player;
    private PlayerMovement player;
    private bool playerIsDead;
    public bool playerInAtkArea;

    public bool inPlayerDamageArea;

    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private float speed = 5f;

    public float atkValue = 50;
    bool attacking = false;
    bool spellAttack;

    public float health = 500;
    public float mana = 100;
    MainController mc;
    // Start is called before the first frame update
    public override void Initialize()
    {
        base.Initialize();
        agentEnvironment = GetComponentInParent<AgentEnvironment>();
        player = agentEnvironment.playerAgent;
        mc = FindObjectOfType<MainController>();
        //rigidbody = GetComponent<Rigidbody>();
    }

    IEnumerator SpellAttacking () {
        animator.SetBool("SpellAttack", true);
        yield return new WaitForSeconds(3f);
        spellAttack = false;
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        
        float inputX = 0;
        float inputY = 0;
        
        if (actionBuffers.DiscreteActions[2] == 1 && !spellAttack) {
            spellAttack = true;
            StartCoroutine(SpellAttacking());
        }
        else {
            animator.SetBool("SpellAttack", false);
            if (actionBuffers.DiscreteActions[0] == 1) //W front
            {
                animator.SetBool("Front", true);
                inputY = 1;
            }
            else if (actionBuffers.DiscreteActions[0] == 2) //A back
            {
                animator.SetBool("Front", false);
                inputY = -1;
            }

            if (actionBuffers.DiscreteActions[1] == 1) //S left
            {
                inputX = -1;
                if (inputY == -1) //front 
                {
                    spriteRenderer.flipX = false;
                }
                else //back
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (actionBuffers.DiscreteActions[1] == 2)//D right
            {
                inputX = 1;
                if (inputY == -1) //front
                {
                    spriteRenderer.flipX = true;
                }
                else //back
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
        


        if (!(inputX == 0 && inputY == 0))
        {
            //Debug.Log("1");
            transform.localPosition = new Vector3(transform.localPosition.x + (inputX * speed / 500), 0, transform.localPosition.z + (inputY * speed/10));
        }
        else
        {
            
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            //Debug.Log(transform.position.y);
        }
        //Debug.Log(transform.position.y + "  2");
        if (MaxStep > 0) AddReward(-1f / MaxStep);
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        int turnAction = 0;
        int spellAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) //front
        {
            forwardAction = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) //back
        {
            forwardAction = 2;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //left
        {
            turnAction = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))//right
        {
            turnAction = 2;
        }

        if (Input.GetKey(KeyCode.Space)) {
            spellAction = 1;
        }

        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;
        actionsOut.DiscreteActions.Array[2] = spellAction;
    }

    public override void OnEpisodeBegin()
    {
        agentEnvironment.ResetArea();
        playerInAtkArea = false;
        playerIsDead = false;
        //AddReward(1f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Whether the penguin has eaten a fish (1 float = 1 value)
        sensor.AddObservation(playerInAtkArea); //1

        sensor.AddObservation(playerIsDead); //1

        // Distance to the player (1 float = 1 value)
        sensor.AddObservation(Vector3.Distance(player.transform.position, transform.position)); //1

        // Direction to player (1 Vector3 = 3 values)
        sensor.AddObservation((player.transform.position - transform.position).normalized); //3

        // Direction penguin is facing (1 Vector3 = 3 values)
        sensor.AddObservation(transform.forward); //3

        sensor.AddObservation(transform.right); //3

        // 1 + 1 + 1 + 3 + 3 + 3= 12 total values
    }

    private void OnTriggerEnter (Collider col)
    {
        //Debug.Log("in collider");
        if (col.tag == "Player")
        {
            //Debug.Log("player in collider");
            playerInAtkArea = true;
            AttackPlayer(col.gameObject);
            if (playerIsDead) {
                
                EndEpisode();
            }
            
        }
    }

    private void OnTriggerExit(Collider col) {
        playerInAtkArea = false;
    }

    private void AttackPlayer(GameObject playerObj)
    {
        //if (playerIsDead) return; // Can't eat another fish while full
        //Debug.Log("try attack");
        
        if (!attacking) {
            agentEnvironment.playerHealth -= atkValue;
            StartCoroutine(SetAttackStatus());
            AddReward(1f);
        }
        if (mc.player.health <= 0) {//(agentEnvironment.playerHealth <= 0) {
            Debug.Log("Dead");
            playerIsDead = true;
            EndEpisode();
        }

        
    }

    IEnumerator SetAttackStatus () {
        //Debug.Log("set attack");
        attacking = true;
        yield return new WaitForSeconds(2);
        attacking = false;
    }


}
