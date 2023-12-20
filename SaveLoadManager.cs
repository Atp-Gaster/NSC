using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public static class SaveLoadManager
{
    public static string saveFile = Application.persistentDataPath + "/savedData.txt";
    public static void SaveJSON (Database database)
    {
        string json = JsonUtility.ToJson(database);
        File.WriteAllText(saveFile, json);
    }

    public static DatabaseSavedData LoadJSON()
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            DatabaseSavedData data = JsonUtility.FromJson<DatabaseSavedData>(json);

            return data;
        }else
        {
            
           
            Database tempdatabase = new Database();
            Debug.LogError("No save file found");
            DatabaseSavedData data = new DatabaseSavedData(tempdatabase);
            return data;
           
        }
    }

    public static void SaveBinary(Database database)
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/database.save", FileMode.Create);
        DatabaseSavedData data = new DatabaseSavedData(database);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("saved");
    }

    public static DatabaseSavedData LoadBinary()
    {
        if (File.Exists(Application.persistentDataPath + "/database.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/database.save", FileMode.Open);

            DatabaseSavedData data = bf.Deserialize(stream) as DatabaseSavedData;
            stream.Close();
            Debug.Log("loaded");
            return data;
        }
        else
        {
            Database tempdatabase = new Database();
            Debug.LogError("No save file found");
            DatabaseSavedData data = new DatabaseSavedData(tempdatabase);
            return data;
        }
    }
    /*
    public static void SavePlayer(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.save", FileMode.Create);
        PlayerSavedData data = new PlayerSavedData(player);
        bf.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerSavedData LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.save", FileMode.Open);

            PlayerSavedData data = bf.Deserialize(stream) as PlayerSavedData;
            stream.Close();

            return data;
        }
        else
        {
            Player tempDummy = new Player(0, 0, 0, 0, new Vector3(), 0);
            Debug.LogError("No save file found");
            PlayerSavedData data = new PlayerSavedData(tempDummy);
            return data;
        }
    }
    */
}
/*
public class PlayerSavedData
{

    public int level;
    public float health;
    public float mana;
    public int money;
    public Vector3 pos;
    public int storyProgress;

    public PlayerSavedData(Player player)
    {
        level = player.level;
        health = player.health;
        mana = player.mana;
        pos = player.pos;
        storyProgress = player.storyProgress;
    }
}
*/
[Serializable]
public class DatabaseSavedData
{
    public Player player;
    public List<NPC> npcs;
    public List<Quest> quests;
    public List<Item> items;
    public List<SaveSpawn> saveSpawns;

    public DatabaseSavedData(Database database)
    {
        player = database.player;
        npcs = database.npcs;
        quests = database.quests;
        items = database.items;
        saveSpawns = database.saveSpawns;
    }
}


