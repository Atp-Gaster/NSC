using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSpawnManager : MonoBehaviour
{
    public MainController mc;
    public List<SaveSpawn> saveSpawns;
    Transform player;
    public void SavePoint(int saveSpawnID)
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        //player.GetChild(0).GetComponent<Animator>().SetBool("Wai", true);
        StartCoroutine(RestoreHealthAndMana(1));
        mc.player.curSaveSpawnID = saveSpawnID;

        
        

    }

    

    IEnumerator RestoreHealthAndMana (float sec)
    {
        //float gapMana = (mc.player.maxMana - mc.player.mana) / (sec * 60);
        float gapHealth = (mc.player.maxHealth - mc.player.health) / (sec * 60);
        int count = 0;
        //Debug.Log(gapHealth + " " + (sec / 60));
        mc.CallRestorePartialStat(0, mc.player.maxMana, 0);
        while (count <= 60)
        {
            //Debug.Log("healing");
            yield return new WaitForSecondsRealtime(sec / 60);
            count++;
            mc.player.health += gapHealth;
           // mc.player.mana += gapMana;
        }

        if (mc.player.health > mc.player.maxHealth)
            mc.player.health = mc.player.maxHealth;
        //if (mc.player.mana > mc.player.maxMana)
        //    mc.player.mana = mc.player.maxMana;
        //player.GetChild(0).GetComponent<Animator>().SetBool("Wai", false);
        //Debug.Log("heal completed");

        mc.saveLoad.Save();
    }
}
