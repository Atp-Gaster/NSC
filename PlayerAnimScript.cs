using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAnimScript : MonoBehaviour
{
    public void Attack()
    {
        Debug.Log("player attack!");
        if (PlayerMovement.curTarget != null) {
            
            
                PlayerMovement.curTarget.GetComponent<EachEntity>().health -= transform.parent.GetComponent<PlayerMovement>().mc.player.atk;
            
        }
            
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //mc.RespawnPlayer();
    }

    public void SetTextDisplay (string txt) {
        GameObject a = FindObjectOfType<TextDisplayScript>().gameObject;
        a.transform.GetChild(0).GetComponent<Text>().text = txt;
        a.transform.GetComponent<Animator>().SetBool("On", true);
    }

    public void StopWaiAnim() {
        transform.GetComponentInParent<Animator>().SetBool("Wai", false);
    }

    
}
