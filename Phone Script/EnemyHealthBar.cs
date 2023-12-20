using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    //system
	public Slider slider;
	public Gradient gradient;
	public Image fill;

    public EachEntity EnemyStat;
    public int StatusType = 0;

    public GreatGrandpaAgent agent;

	void SetMaxHealth()
	{
        if (agent != null && StatusType == 0) {
            slider.maxValue = agent.health;
            slider.value = agent.health;

            fill.color = gradient.Evaluate(1f);
        }
        else if (StatusType == 0)
        {
            slider.maxValue = EnemyStat.health;
            slider.value = EnemyStat.health;

            fill.color = gradient.Evaluate(1f);
        }
        
	}/*//for Enemy CD Skill
    void SetMaxMana()
    {
        if (StatusType == 1)
        {
            slider.maxValue = EnemyStat.health;
            slider.value = EnemyStat.health;

            fill.color = gradient.Evaluate(1f);
        }
       
    }
   */
    public void SetHealth()
	{
        switch(StatusType)
        {
            case 0:
                if (agent != null) {
                    //Debug.Log(agent.health);
                    slider.value = agent.health;
                }
                else
                    slider.value = EnemyStat.health;

                fill.color = gradient.Evaluate(slider.normalizedValue);
                break;/*
            case 1:
                
                slider.value = EnemyStat.health;

                fill.color = gradient.Evaluate(slider.normalizedValue);
                break;
                */
        }
        
	}
    private void Start()
    {
        if (StatusType == 0)
            SetMaxHealth();
        //if (StatusType == 1)
            //SetMaxMana();
    }
    private void Update()
    {
        SetHealth();
    }

}
