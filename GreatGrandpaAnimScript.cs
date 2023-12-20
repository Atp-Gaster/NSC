using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatGrandpaAnimScript : MonoBehaviour
{
    MainController mc = new MainController();
    GreatGrandpaAgent agent;
    void Start() {
        agent = transform.GetComponent<GreatGrandpaAgent>();
        mc = FindObjectOfType<MainController>();
    }
    void CloseWeaponAttack () {
        transform.GetComponent<Animator>().SetBool("WeaponAttack", false);
    }
/*
    private void FixedUpdate() {
        if (agent.mana < 10) {
            agent.mana += 0.001f;
        }
    }

    void InstanceWeaponPrefab () {
        //GameObject weaponPrefab = transform.GetComponent<GreatGrandpaAgent>().weaponPrefab;
        GameObject a = Instantiate(weaponPrefab);
        a.transform.SetParent(transform);
        a.transform.localPosition = new Vector3(-0.25f, 2.15f, -0.45f);
        a.GetComponent<SpriteRenderer>().flipX = transform.parent.GetComponent<GreatGrandpaAgent>().spriteRenderer.flipX;
    }
*/
    public void SpellAttack (float atk) {
        
        GreatGrandpaAgent agent = transform.parent.GetComponent<GreatGrandpaAgent>();
        if (agent.mana >= 0.5) {
            //agent.mana -= 0.5f;
            //Debug.Log(agent.playerInAtkArea);
            if (agent.playerInAtkArea) {
                mc.player.health -= atk;
                agent.AddReward(atk);
            }
        }
        

    }
    public void CloseSpellAttack () {
        transform.GetComponent<Animator>().SetBool("SpellAttack", false);
        transform.GetComponent<Animator>().SetBool("Front", true);
    }
}
