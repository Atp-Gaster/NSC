using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimScript : MonoBehaviour
{
    

    Transform fullFillImage;
    Transform fillImage;

    GameObject curTarget;
    [HideInInspector]
    public MainController mc;
    //public bool isDamaging = false;

    void Start()
    {
        fullFillImage = transform.parent.GetChild(3).GetChild(1);
        fillImage = transform.parent.GetChild(3).GetChild(0);

        // Default to an empty bar
        var newScale = this.fillImage.localScale;
        newScale.y = 0;
        newScale.x = 0;
        this.fillImage.localScale = newScale;
    }
    /*
    void SetFillBar(float fillAmount) //fillamount from 0 - 1
    {
        // Make sure value is between 0 and 1
        fillAmount = Mathf.Clamp01(fillAmount);

        // Scale the fillImage accordingly
        var newScale = this.fillImage.localScale;
        newScale.x = this.fullFillImage.localScale.x * fillAmount;
        newScale.y = this.fullFillImage.localScale.y * fillAmount;
        this.fillImage.localScale = newScale;
    }
    
    public void StartFill()
    {
        
        if (transform.parent.GetChild(3).gameObject != null) //AtkFill is present
        {
            transform.parent.GetChild(3).gameObject.SetActive(true);
            StartCoroutine(fillBar()); 
        }
    }


    IEnumerator fillBar()
    {
        SetFillBar(0);
        Animator anim = transform.GetComponent<Animator>();
        //float gap = GetAnimationLength(anim) / 100;
        float gap = transform.parent.GetComponent<EachEntity>().timeToDamage / 100;
        float count = 0;

        while (count < 1)//(fillImage.localScale.x != 1)
        {
            yield return new WaitForSeconds(gap);
            count += 0.01f;
            SetFillBar(count);
        }
        //Attack();
        transform.parent.GetChild(3).gameObject.SetActive(false);
        transform.parent.GetChild(3).GetComponent<EntityDamageArea>().inDamageArea = false;

    }
    
    public void Attack()
    {
        EachEntity entityScript = transform.parent.GetComponent<EachEntity>();
        if (entityScript.transform.GetChild(3).GetComponent<EntityDamageArea>().inDamageArea)
        {
            curTarget = transform.parent.GetComponent<EachEntity>().curTarget;

            if (curTarget != null)
            {
                mc = FindObjectOfType<MainController>();
                if ((curTarget.transform.position - transform.parent.position).magnitude <= entityScript.damageLocalArea)
                    mc.player.health -= entityScript.atkDamageTest;
            }
        }
        
        

    }


    float GetAnimationLength(Animator anim)
    {
        float time = 0;
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
        {
            if (ac.animationClips[i].name == "AtkFront" || ac.animationClips[i].name == "AtkBack")        //If it has the same name as your clip
            {
                time = ac.animationClips[i].length;
                break;
            }
        }
        return time;
    }
    */
}
