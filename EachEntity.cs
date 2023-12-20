using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinding;

public enum AttackType 
{ 
    Normal,
    Fire
}


public class EachEntity : MonoBehaviour
{
    //public AIPath aiPath;
    public float health;
    public float xpToGive;
    public float secToRespawn;
    public int EntityID;
    public AttackType attackType;
    public SFXEnemyControl SFXControl;
    //public bool CanSee = false;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private Animator animator = null;


    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private Rigidbody rb;


    float maxHealth;
    [HideInInspector]
    public Vector3 habitatCenter;
    [HideInInspector]
    public float area;

    Vector3 nextPos, whereToMove;
    bool isMoving = false;
    public bool isTargeting = false;
    public bool isDamaging = false;
    [HideInInspector]
    public GameObject curTarget;
    float previousDistanceToTouchPos, currentDistanceToTouchPos;
    public float distanceToAtk;
    public float atkDamageTest;
    //public float timeToDamage;
    public float damageLocalArea = 40; //scale of atkArea
    [HideInInspector]
    public bool inPlayerDamageArea;
    bool dontDisplayTextStat = false;
    MainController mc;
    /*
    public void Seeing()
    {
        aiPath.canSearch = true;
    }

    public void UnSeeing()
    {
        aiPath.canSearch = false;
    }
    void isCollab()
    {
        Debug.LogWarning("Hit");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isCollab();
        }
    }
    */
    public void Attack()
    {
        if (attackType.ToString() == "Normal")
        {
            if (transform.GetChild(3).GetComponent<EntityDamageArea>().inDamageArea)
            {
                if (curTarget != null)
                {
                    if ((curTarget.transform.position - transform.position).magnitude <= damageLocalArea)
                        mc.player.health -= atkDamageTest;
                        
                }
            }
        }
        if (attackType.ToString() == "Fire" && transform.Find("Fire") != null)
        {
            transform.Find("Fire").GetComponent<SpellManager>().PlayerCastSpell("Fire");
            
        }
        
    }

    public void Death()
    {
        if (PlayerMovement.curTarget == this.gameObject)
        {
            //SFXControl.PlaySFXDeath();
            transform.GetChild(1).gameObject.SetActive(false);
            
            PlayerMovement.curTarget = null;
            PlayerMovement.isTargeting = false;
        }

        if (transform.parent.GetComponent<SpawnEntityGroup>().dontRespawn)
        {
            //Debug.Log("dead 4ever");
            Destroy(gameObject);
        }
        else
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(3).GetComponent<EntityDamageArea>().inDamageArea = false;
            animator.SetBool("Attacking", false);
            SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            SpriteRenderer shadow = transform.GetChild(5).GetComponent<SpriteRenderer>();
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0); //transparent
            shadow.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            StartCoroutine(WaitAndRespawn(secToRespawn));
            
        }

        
    }

    IEnumerator WaitAndRespawn (float secToRespawn)
    {
        yield return new WaitForSeconds(secToRespawn);
        health = maxHealth;

        SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer shadow = transform.GetChild(5).GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255); //fully visible
        shadow.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        transform.GetChild(4).gameObject.SetActive(true);
        dontDisplayTextStat = false;
    }

    private void Start()
    {
        maxHealth = health;
        mc = FindObjectOfType<MainController>();
        Vector3 damageAreaScale = new Vector3(damageLocalArea, damageLocalArea, 0);
        transform.GetChild(3).localScale = damageAreaScale;
        

    }

    private void Update()
    {
        /*
        if (CanSee == false)
            UnSeeing();
        if (CanSee == true)
            Seeing();

        //Set Flip Left-Right
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        */
        if (health <= 0)
        {
            Death();
            if (!dontDisplayTextStat) {
                StartCoroutine(mc.ShowTextStat(true, xpToGive, Stat.XP));
                dontDisplayTextStat = true;
            }
            
        }
        else //if (!CameraToTarget.isZooming) //alive and not zooming
        {
            //Debug.Log(nextPos);
            //Vector3 PlayerXYZ = this.transform.localPosition;
            float inputX = 0;
            float inputY = 0;

            if (isMoving)
                currentDistanceToTouchPos = (nextPos - transform.position).magnitude;

            if (isTargeting && curTarget != null) //targeting player
            {
                

                if ((curTarget.transform.position - transform.position).magnitude < distanceToAtk)
                {
                    
                    isMoving = false;
                    rb.velocity = Vector3.zero;
                    animator.SetBool("Attacking", true);
                    //animator.SetBool("Walk", false);
                }
                else
                {
                    animator.SetBool("Attacking", false);
                    nextPos = curTarget.transform.position;
                    //Vector3 touchPositionScreen = Camera.main.WorldToScreenPoint(curTarget.transform.position);

                    //float inputX = 0;
                    //float inputY = 0;

                    if (nextPos.z < transform.position.z) //front
                    {
                        //Debug.Log("front");
                        animator.SetBool("Front", true);
                        inputY = -1;
                    }
                    else //back
                    {
                        //Debug.Log("back");
                        animator.SetBool("Front", false);
                        inputY = 1;
                    }

                    if (nextPos.x < transform.position.x) //left
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
                    else //right
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

                    whereToMove = (nextPos - transform.position).normalized;
                    rb.velocity = new Vector3(whereToMove.x * speed, 0, whereToMove.z * speed);
                }
                
            }
            else if (!isMoving) //find new destination
            {
                if (transform.childCount >= 4)
                    transform.GetChild(3).gameObject.SetActive(false);
                //DoubleTap = Input.GetTouch(1);
                //animator.SetBool("Walk", true);

                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;

                isMoving = true;

                nextPos = Random.insideUnitCircle * area;
                nextPos = new Vector3(nextPos.x + habitatCenter.x, habitatCenter.y, nextPos.y + habitatCenter.z);

                if (nextPos.z < transform.position.z) //front
                {
                    //Debug.Log("front");
                    animator.SetBool("Front", true);
                    inputY = -1;
                }
                else //back
                {
                    //Debug.Log("back");
                    animator.SetBool("Front", false);
                    inputY = 1;
                }

                if (nextPos.x < transform.position.x) //left
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
                else //right
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

                whereToMove = (nextPos - transform.position).normalized;
                rb.velocity = new Vector3(whereToMove.x * speed, 0, whereToMove.z * speed);
            }
            

            if (currentDistanceToTouchPos > previousDistanceToTouchPos)
            {
                //Debug.Log("end round");
                isMoving = false;
                rb.velocity = Vector3.zero;
                //animator.SetBool("Walk", false);
                //animator.SetBool("Front", true);
            }

            if (isMoving)
            {
                previousDistanceToTouchPos = (nextPos - transform.position).magnitude;
                //Debug.Log(previousDistanceToTouchPos);
            }
        }
    }

}
