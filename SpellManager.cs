using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spells
{
    Fire,
    Wind,
    Dirt,
    DrinkHerb,
    Lightning

}

public class SpellManager : MonoBehaviour
{
    public Spells spellType;
    public bool isPlayer;
    bool inEntityDamageArea;
    int entityLayerMask = 1 << 11; //entity is in 11th layerMask
    int ignoreRaycastLayerMask = 2 << 11;
    int playerLayerMask = 1 << 5; // UI
    float playerFireDamage = 70;
    //float entityFireDamage
    Animator animator;
    Animator playerAnimator;
    MainController mc;
    //InventoryScript inventory;
    public void Start()
    {
        animator = transform.GetComponent<Animator>();
        if (isPlayer)
        {
            playerAnimator = transform.parent.Find("CharacterSprite").GetComponent<Animator>();
            mc = FindObjectOfType<MainController>();
            //inventory = FindObjectOfType<InventoryScript>();
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.tag == "Entity")
        {
            other.transform.GetComponent<EachEntity>().inPlayerDamageArea = true;
        }
        else if (isPlayer && other.tag == "Boss") 
        {
             other.transform.GetComponent<GreatGrandpaAgent>().inPlayerDamageArea = true;
        }
        else if (!isPlayer && other.tag == "Player")
        {
            inEntityDamageArea = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (isPlayer && other.tag == "Entity")
        {
            other.transform.GetComponent<EachEntity>().inPlayerDamageArea = false;
        }
        else if (isPlayer && other.tag == "Boss") 
        {
             other.transform.GetComponent<GreatGrandpaAgent>().inPlayerDamageArea = false;
        }
        else if (!isPlayer && other.tag == "Player")
        {
            inEntityDamageArea = false;
        }
    }

    


    public void PlayerCastSpell(string spell)
    {
        if (spell == Spells.Fire.ToString())
        {
            FireSpell(2);
        }
        else if (spell == Spells.Wind.ToString())
        {
            WindSpell(1);
        }
        else if (spell == Spells.Dirt.ToString()) 
        {
            DirtSpell(2);
        }
        else if (spell == Spells.DrinkHerb.ToString())  
        {
            DrinkHerb(0);
        }
        else if (spell == Spells.Lightning.ToString())  
        {
            LightningSpell(4);
        }
        else {
            Debug.Log("none");
        }
    }

    public void Attack (float damage)
    {
        if (isPlayer) //player
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f, entityLayerMask);
            //Collider[] hitColliders_ignoreRaycast = Physics.OverlapSphere(transform.position, 20f, ignoreRaycastLayerMask);
            /*
            foreach (Collider hitCollider in hitColliders_ignoreRaycast) {
                Debug.Log(hitCollider.name);
                if (hitCollider.tag == "Boss" && hitCollider.GetComponent<GreatGrandpaAgent>().inPlayerDamageArea) 
                {
                    hitCollider.transform.GetComponent<GreatGrandpaAgent>().health -= damage;
                }
            }*/
            GreatGrandpaAgent ggpAgent = FindObjectOfType<GreatGrandpaAgent>();
            if (ggpAgent != null && ggpAgent.inPlayerDamageArea) {
                ggpAgent.health -= damage;
            }
            //Debug.Log(hitColliders.Length);
            foreach (Collider hitCollider in hitColliders)
            {
                
                if (hitCollider.tag == "Boss") {
                    Debug.Log(hitCollider.name);
                    hitCollider.transform.GetComponent<GreatGrandpaAgent>().health -= damage;
                }
                else if (hitCollider.tag == "Entity")
                {
                    //Debug.Log(hitCollider.name);
                    hitCollider.transform.GetComponent<EachEntity>().health -= damage;
                }
                
            }
        }
        else //entity
        {
            if (transform.parent.GetChild(3).GetComponent<EntityDamageArea>().inDamageArea)
            {
                mc.player.health -= damage;
            }
            /*
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, playerLayerMask);
            Debug.Log(hitColliders.Length);
            foreach (Collider hitCollider in hitColliders)
            {
                Debug.Log("hit " + damage);
                mc.player.health -= damage;
                //hitCollider.transform.GetComponent<PlayerMovement>()
            }
            */
        }

        animator.SetBool("On", false);
        
    }

    public void TurnCastAnimationOff ()
    {
        if (isPlayer) {
            playerAnimator.SetBool("FireSpell", false);
            playerAnimator.SetBool("WindSpell", false);
            playerAnimator.SetBool("DirtSpell", false);
            playerAnimator.SetBool("LightningSpell", false);
        }
        //Debug.Log("TurnCastAnimationOff");
        

        //Debug.Log(playerAnimator.GetBool("WindSpell"));
    }

    public void DrinkHerb (float mana)
    {
        Debug.Log("drink herb");
        if (mc.player.mana >= mana && isPlayer && mc.inventoryManager.items[4].owned > 0) {
            mc.CallRestorePartialStat(0.5f, 5, 100);
            mc.inventoryManager.items[4].owned--;
            StartCoroutine(mc.ShowTextStat("+ 100 HP\n+ 5 MP"));
            //mc.UseMana(2);
            //playerAnimator.SetBool("FireSpell", true);
        }
            
        //animator.SetBool("On", true);
        

    }

    public void LightningSpell (float mana)
    {
        Debug.Log("lightning spell");
        if (mc.player.mana >= mana && isPlayer) {
            mc.UseMana(mana);
            playerAnimator.SetBool("LightningSpell", true);
        }
            
        animator.SetBool("On", true);
        

    }

    public void FireSpell (float mana)
    {
        Debug.Log("fire spell");
        if (mc.player.mana >= mana && isPlayer) {
            mc.UseMana(mana);
            playerAnimator.SetBool("FireSpell", true);
        }
            
        animator.SetBool("On", true);
        

    }

    public void WindSpell (float mana)
    {
        Debug.Log("Wind spell");
        if (mc.player.mana >= mana && isPlayer) {
            mc.UseMana(mana);
            playerAnimator.SetBool("WindSpell", true);
        }
        animator.SetBool("On", true);
        
    }

    public void DirtSpell (float mana)
    {
        Debug.Log("Dirt spell");
        if (mc.player.mana >= mana && isPlayer) {
            mc.UseMana(mana);
            playerAnimator.SetBool("DirtSpell", true);
        }
        animator.SetBool("On", true);
    }


    public void ToggleSpell ()
    {
        transform.gameObject.SetActive(!transform.gameObject.activeSelf);
    }
    private void Update()
    {
        
    }
}
