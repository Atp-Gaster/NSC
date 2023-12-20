using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TestEnemyData : MonoBehaviour
{
    //public EnemyEntity Enemy;
    public EnemyData Kamod;
    public EnemyData TaiHong;
    // Start is called before the first frame update
    void Start()
    {
        

        //TaiHong = new EnemyData("TaiHong",5,3);
        //TaiHong.ShowInfo(TaiHong.Name);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Store()
    {
        // Enemy = new EnemyEntity("KamodSmall",01,1,1,6); //การสร้าง obj
        //Enemy.PrintStatus(Enemy.name);
    }
}
