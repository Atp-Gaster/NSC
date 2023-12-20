using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EnemyData 
{
    public string Name;
    public int Hp;
    public float Atk;

    public EnemyData(string _Name, int _Hp, float _Atk)
    {
        Name = _Name;
        Hp = _Hp;
        Atk = _Atk;

    }
    public void ShowInfo(string NameEnemy)
    {
        Debug.LogWarning("========= " + NameEnemy + " =========");
        Debug.LogWarning(Name + " " + "Hp: " + Hp + " ATK: " + Atk);
    }

    
    
   
}
