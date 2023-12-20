using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    //public Database database;
    public MainController mc;
    //public PlayerManager pm;

    public void Save()
    {
        foreach (SaveSpawn ss in mc.saveSpawnManager.saveSpawns)
        {
            ss.UpdateSaveSpawnToSave();
        }

        Database database = new Database(mc.player, mc.npcManager.npcs, mc.questManager.questList, mc.inventoryManager.items, mc.saveSpawnManager.saveSpawns);
        SaveLoadManager.SaveJSON(database);
        //SaveLoadManager.SaveBinary(database);
    }
    public void Load()
    {
        
        if (PlayerPrefs.HasKey("OldGame"))
        {
            //Debug.Log(Application.persistentDataPath);
            //DatabaseSavedData databaseData = SaveLoadManager.LoadBinary();
            DatabaseSavedData databaseData = SaveLoadManager.LoadJSON();

            mc.player = databaseData.player;
            mc.npcManager.npcs = databaseData.npcs;
            mc.questManager.questList = databaseData.quests;
            mc.inventoryManager.items = databaseData.items;
            mc.saveSpawnManager.saveSpawns = databaseData.saveSpawns;

            foreach (SaveSpawn ss in mc.saveSpawnManager.saveSpawns)
            {
                ss.UpdateSaveSpawnToLoad();
            }
        }
        else
        {
            PlayerPrefs.SetInt("OldGame", 1); //1 is old game, 0 is new game
        }

    }
}
