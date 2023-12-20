using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //system
	public Image HPImg;
    public GameObject MPBar;
    public GameObject ManaPrefab;
	public Gradient gradient;
	public Image fill;

    public MainController PlayerStat;
    //public int StatusType = 0;

    
	void SetMaxHealth()
	{
        HPImg.fillAmount = 1;//PlayerStat.player.health / PlayerStat.player.maxHealth;

        fill.color = gradient.Evaluate(1f);
	}
    void SetMaxMana()
    {
        for (int i = 0; i < MPBar.transform.childCount; i++)
        {
            Destroy(MPBar.transform.GetChild(i));
        }
        for (int i = 0; i < PlayerStat.player.maxMana; i++)
        {
            GameObject a = Instantiate(ManaPrefab);
            a.transform.SetParent(MPBar.transform);
        }
       
    }

    public void SetMana () {
        for (int i = 0; i < MPBar.transform.childCount; i++)
        {
            Destroy(MPBar.transform.GetChild(i));
        }
        
        for (int i = 0; i < PlayerStat.player.mana; i++)
        {
            GameObject a = Instantiate(ManaPrefab);
            a.transform.SetParent(MPBar.transform);
        }
    }
    
    public void SetHealth()
	{
        HPImg.fillAmount = PlayerStat.player.health / PlayerStat.player.maxHealth;
        fill.color = gradient.Evaluate(HPImg.fillAmount);
	}

    public void UseMana(int manaUsed) {
        for (int i = 0; i < manaUsed; i++)
        {
            Destroy(MPBar.transform.GetChild(MPBar.transform.childCount - 1));
        }
    }
    private void Start()
    {
        SetHealth();
        SetMana();
            
    }
    private void Update()
    {
        SetHealth();
    }

}
