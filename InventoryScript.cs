using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<Item> items;
    public GameObject itemPrefab;
    public GameObject itemPanel;
    public GameObject itemDescPanel;
    public GameObject hotItemPanel;
    public Queue<int> hotItem = new Queue<int>();

    

    public void CloseProfile()
    {
        //itemPanel.SetActive(false);

        Debug.Log("close");
        if (hotItem.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (hotItem.Contains(i))
                    items[i].inHotItem = true;
                else
                    items[i].inHotItem = false;
            }
        }

        DestroyAllChildOf(hotItemPanel.transform);
        //Debug.Log(hotItem.Count);
        foreach (int i in hotItem)
        {
            //Debug.Log("1");
            GameObject a = InstantiateItemPrefab(items[i]);
            a.transform.SetParent(hotItemPanel.transform);
        }
    }

    public GameObject InstantiateItemPrefab(Item item)
    {
        GameObject a = Instantiate(itemPrefab);
        Texture2D tex = new Texture2D(256, 256, TextureFormat.RGBA32, false); //TextureFormat.ARGB32, false);
        //a.transform.SetParent(itemPanel.transform);

        if (item.sprite != null)
        {
            //Debug.Log("sprite");
            a.transform.GetChild(1).GetComponent<Image>().overrideSprite = item.sprite;
            //a.transform.GetChild(1).GetComponent<RawImage>().texture = item.picFile;
        }
        
        else //if (true)//base64
        {
            byte[] bytePic = Convert.FromBase64String(item.pic);//System.Text.Encoding.UTF8.GetBytes(item.pic);

            tex.LoadImage(bytePic);
            tex.Apply();
            a.transform.GetChild(2).GetComponent<RawImage>().texture = tex;

        }
        a.transform.GetChild(3).GetComponent<Text>().text = item.owned.ToString();
        a.GetComponent<ItemPrefabScript>().id = item.id;
        a.transform.GetChild(4).gameObject.SetActive(item.inHotItem);

        return a;
    }

    public void DestroyAllChildOf(Transform a)
    {
        foreach (Transform child in a)
        {
            Destroy(child.gameObject);
            //Debug.Log("destroy");
        }

        
    }

    public void OpenProfile ()
    {



        DestroyAllChildOf(itemPanel.transform);
        hotItem.Clear();
        foreach (Item item in items)
        {
            if (item.owned > 0)
            {
                GameObject a = InstantiateItemPrefab(item);

                a.transform.SetParent(itemPanel.transform);

                if (item.inHotItem)
                {
                    hotItem.Enqueue(item.id);
                }
            }
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        //itemDescPanel = itemDescPanel.transform.parent.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
