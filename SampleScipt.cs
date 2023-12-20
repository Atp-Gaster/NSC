using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScipt : MonoBehaviour
{
    public SaveLoad sl;

    public int level = 1;
    public float health = 100;
    public float mana = 100;
    public int money = 0;
    //public Vector3 pos = new Vector3(0, 0, 0);
    public int storyProgress = 1;
    // Start is called before the first frame update
    /*
    private void Update()
    {
        sl.database.player.level = level;
        sl.database.player.health = health;
        sl.database.player.mana = mana;
        sl.database.player.money = money;
        sl.database.player.storyProgress = storyProgress;
    }

    public void UpdateUI ()
    {
        level = sl.database.player.level;
        health = sl.database.player.health;
        mana = sl.database.player.mana;
        money = sl.database.player.money;
        storyProgress = sl.database.player.storyProgress;
    }
    */
}
