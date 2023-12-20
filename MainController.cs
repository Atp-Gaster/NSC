using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat {
        XP,
        HP,
        MP
    }
public class MainController : MonoBehaviour
{
    public GameObject playerObj;
    public NPCManager npcManager;
    public DialogTrigger dialogTrigger;
    public QuestManager questManager;
    public InventoryScript inventoryManager;
    public SaveSpawnManager saveSpawnManager;
    public SaveLoad saveLoad;
    public GameObject TextStatPrefab;
    public GameObject ManaUI;
    public Player player = new Player();
    
    public GameObject ManaPrefab;
    float timer;
    float holdDur = 1f;
    bool showTextStatIsOn;
    //public List<DialogueLine> testDialogEvent;
    // Start is called before the first frame update
    void Start()
    {
        RespawnPlayer();
    }

    public void CallRestorePartialStat (float sec, float unitMana, float unitHealth) 
    {
        if (player.health + unitHealth > player.maxHealth) 
        {
            unitHealth = player.maxHealth - player.health;
        }
        if (player.mana + unitMana > player.maxMana) 
        {
            unitMana = player.maxMana - player.mana;
        }
        StartCoroutine(RestorePartialStat(sec, unitMana, unitHealth));
    }

    IEnumerator RestorePartialStat (float sec, float unitMana, float unitHealth)
    {
        //float gapMana = unitMana / (sec * 60);
        float gapHealth = unitHealth / (sec * 60);
        int count = 0;

        float oldHealth = player.health;
        //float oldMana = player.mana;
        //Debug.Log(gapHealth + " " + (sec / 60));
        AddMana(unitMana);
        while (player.health < oldHealth + unitHealth)//(count <= 60)
        {
            //Debug.Log("healing");
            yield return new WaitForSecondsRealtime(sec / 60);
            count++;
            player.health += gapHealth;
            //player.mana += gapMana;
        }
        

        if (player.health != oldHealth + unitHealth)
            player.health = oldHealth + unitHealth;
        //if (player.mana > oldMana + unitMana)
            //player.mana = oldMana + unitMana;
        //player.GetChild(0).GetComponent<Animator>().SetBool("Wai", false);
        //Debug.Log("heal completed");

        //mc.saveLoad.Save();
    }
    

    public IEnumerator ShowTextStat (bool add, float num, Stat stat) {
        if (showTextStatIsOn) {
            
            yield return new WaitForSeconds(0.1f);
        }
        //Debug.Log("Wait");
        showTextStatIsOn = true;
        string txt = "";

        if (add) txt += "+ ";
        else txt += "- ";

        txt += num + " " + stat.ToString();

        GameObject a = Instantiate(TextStatPrefab, playerObj.transform);
        //a.transform.SetParent(playerObj.transform);
        a.transform.GetChild(0).GetComponent<TextMesh>().text = txt;
        showTextStatIsOn = false;
    }

    public IEnumerator ShowTextStat (string txt) {
        if (showTextStatIsOn) {
            
            yield return new WaitForSeconds(0.1f);
        }
        
        showTextStatIsOn = true;
        

        GameObject a = Instantiate(TextStatPrefab, playerObj.transform);
        //a.transform.SetParent(playerObj.transform);
        a.transform.GetChild(0).GetComponent<TextMesh>().text = txt;
        showTextStatIsOn = false;
    }

    public void TriggerDialog(DialogueEvent dialogueEvent, NPC npc = null)
    {
        

        FindObjectOfType<DialogManager>().StartDialog(dialogueEvent, npc);
        /*
        if(Done == false)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialogueEvent, npc);
            
            Done = true;
        }

        */
    }

    public void RespawnPlayer ()
    {
        saveLoad.Load();
        //Debug.Log(saveSpawnManager.saveSpawns[player.curSaveSpawnID].posVector);
        if (saveSpawnManager.saveSpawns[player.curSaveSpawnID].place != null)
        {
            //Debug.Log(saveSpawnManager.saveSpawns[player.curSaveSpawnID].place.transform.position + " place");
            //Debug.Log(saveSpawnManager.saveSpawns[player.curSaveSpawnID].place.transform.position + " 22");
            playerObj.transform.position = saveSpawnManager.saveSpawns[player.curSaveSpawnID].place.transform.position;
        }
        else
        {
            //Debug.Log(saveSpawnManager.saveSpawns[player.curSaveSpawnID].posVector + " posVecter");
            //Debug.Log(saveSpawnManager.saveSpawns[player.curSaveSpawnID].posVector + " 11");
            playerObj.transform.position = saveSpawnManager.saveSpawns[player.curSaveSpawnID].posVector;
        }
        saveLoad.Save();
        //Instantiate(playerPrefab, saveSpawnManager.saveSpawns[player.curSaveSpawnID].place.transform.position, Quaternion.identity);
    }

    public void AddMana (float manaAdded) {
        if (manaAdded > player.maxMana) {
            manaAdded = player.maxMana - player.mana;
        }
        player.mana += manaAdded;
        //int i = 0;
        for (int i = 0; i < manaAdded; i++)
        {
            Instantiate(ManaPrefab, ManaUI.transform);
        }
        

    /*
        for (int i = 0; i < manaUsed; i++)
        {
            Debug.Log("mana destroy");
            Destroy(ManaUI.transform.GetChild(0).gameObject);//ManaUI.transform.childCount - 1).gameObject);
        }
    */
    }

    public void UseMana (float manaUsed) {
        if (manaUsed > player.mana) {
            manaUsed = player.mana;
        }
        player.mana -= manaUsed;
        int i = 0;
        foreach (Transform child in ManaUI.transform) {
            if (i > player.mana - 1) {
                //Debug.Log(i);
                Destroy(child.gameObject);
            }
            i++;
        }

    /*
        for (int i = 0; i < manaUsed; i++)
        {
            Debug.Log("mana destroy");
            Destroy(ManaUI.transform.GetChild(0).gameObject);//ManaUI.transform.childCount - 1).gameObject);
        }
    */
    }


    public void StoryProgressUp ()
    {
        player.storyProgress++;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
