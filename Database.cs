using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;

[Serializable]
public class SaveSpawn 
{
    public string name;
    public int id;
    public GameObject place;
    [HideInInspector]
    public Vector3 posVector;
    [HideInInspector]
    public float posX;
    [HideInInspector]
    public float posY;
    [HideInInspector]
    public float posZ;

    public void UpdateSaveSpawnToSave ()
    {
        try
        {
            posX = place.transform.position.x;
            posY = place.transform.position.y;
            posZ = place.transform.position.z;
            //Debug.Log("save " + posX + ", " + posY + ", " + posZ);
            posVector = new Vector3(posX, posY, posZ);
        }
        
        catch
        {
            posX = 0;
            posY = 0;
            posZ = 0;
            posVector = new Vector3(posX, posY, posZ);
        }
        
    }

    public void UpdateSaveSpawnToLoad()
    {
        //if (place = null)
        //    place = new GameObject();
        //place.transform.position = new Vector3(posX, posY, posZ);
        if (place != null)
        {
            Debug.Log(place.transform.position + " 0");
            posVector = place.transform.position;
        }
        else
        {
            //Debug.Log(posVector + " 000");
            //Debug.Log("load " + posX + ", " + posY + ", " + posZ);
            posVector = new Vector3(posX, posY, posZ);
            //Debug.Log(posVector);
        }
            
    }

    public SaveSpawn(Vector3 position)
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;
    }

}

public enum ItemEffectType
{ 
    None,
    BuffPlayerAtk
}


[Serializable]
public class Item
{
    public string name;
    public int id;
    public int owned;
    public float atk;
    public float price;
    public bool inHotItem;
    public string description;
    public string pic;
    public ItemEffectType itemEffectType;
    public Texture2D picFile;
    public Sprite sprite;
}

[Serializable]
public class Quest
{
    public string name;
    public int id;
    public int npcID;
    public int requireStoryProgress;
    public float currCollect;
    public float passCollect;
    public int status; //0 = not inquest, 1 = in progress, 2 = completed
    public bool finished;
}

[Serializable]
public class NPC
{
    public string name;
    public int id;
    public int currEventID;
    public List<DialogueEvent> dialogueEvent;
    
}
[Serializable]
public class EnemyEntity
{
    public string name;
    public int id;
    public int Hp;
    public float atk;
    public int amountInScene;
    public Vector3 scope;
    public GameObject target;

    public EnemyEntity()
    {
        
    }

    public EnemyEntity(string _name, int _id,int _Hp, float _atk, int _amountInScene)
    {
        name = _name;//name อันแรกคือ ค่าด้านบน _name คืออันที่รับค่ามาจาก EnemyData
        id = _id;
        Hp = _Hp;
        atk = _atk;
        amountInScene = _amountInScene;
        Debug.Log("==============================");
        

    }

    public void PrintStatus(string NameEnemy)
    {
        Debug.Log("========= " + NameEnemy + " =========");
        Debug.Log(name + " "+ "Hp: " + Hp + " ATK: " + atk);
    }
    

}//Cancel 


public enum ActionAfterDialogue
{
    None,
    SetObjectTrueAndView,
    EntityWalkToPlace
}

[Serializable]
public class Speech 
{
    public string speechTxt;
    public string name;
    
}

[Serializable]
public class DialogueEvent
{
    public string eventName;
    public int requiredStoryProgress;
    public int questID;
    //public bool finished;
    //public int currProgressID;
    public bool addStoryProgressAfter;
    public List<Speech> speech;

    public ActionAfterDialogue actionAfterTalk;
    [Header("SetObjectTrueAndView")]
    public GameObject objectToBeActive;
    [Header("EntityWalkToPlace")]
    public GameObject entityWalk;
    public GameObject Destination;

}
[Serializable]
public class Player
{
    public int level;
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public float exp;
    public float maxExp;
    public int money;
    //public Vector3 pos;
    public int storyProgress;
    public float atk;
    public bool IsAtk;
    public int curSaveSpawnID;

    public Player(int _level = 1, float _health = 100, float _mana = 100, int _money = 0, int _storyProgress = 0, int _ATK = 0, bool _IsAtk = false, int _curSaveSpawnID = 0) //, Vector3 _pos = new Vector3()
    {
        level = _level;
        health = _health;
        mana = _mana;
        //pos = _pos;
        storyProgress = _storyProgress;
        atk = _ATK;
        IsAtk = _IsAtk;
        curSaveSpawnID = _curSaveSpawnID;


    }

}

[Serializable]
public class Database 
{
    public Player player;
    public List<NPC> npcs;
    public List<Quest> quests;
    public List<Item> items;
    public List<SaveSpawn> saveSpawns;

    public Database (Player _player = null, List<NPC> _npcs = null, List<Quest> _quests = null, List<Item> _items = null, List<SaveSpawn> _saveSpawns = null)
    {
        player = _player;
        npcs = _npcs;
        quests = _quests;
        items = _items;
        saveSpawns = _saveSpawns;
    }
}
