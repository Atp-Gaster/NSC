using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemPrefabScript : MonoBehaviour
{
    public GameObject hotItem;
    public int id;
    MainController mc;// = new MainController();

    private void Start()
    {
        mc = FindObjectOfType<MainController>();
    }

    private void Update()
    {
        if (mc.inventoryManager.itemPanel.activeSelf)
        {
            transform.GetChild(3).GetComponent<Text>().text = mc.inventoryManager.items[id].owned.ToString();
            if (mc.inventoryManager.items[id].owned <= 0)
                Destroy(gameObject);

            if (mc.inventoryManager.hotItem.Contains(id))
            {
                hotItem.SetActive(true);
            }
            else
            {
                hotItem.SetActive(false);
            }
        }


        
    }

    public void ClickItem()
    {
        //Debug.Log("clicked");
        Item thisItem = mc.inventoryManager.items[id];
        if (transform.parent.name == "ItemPanel")
        {
            if (mc.inventoryManager.itemPanel.activeSelf)
            {
                Transform descPanel = mc.inventoryManager.itemDescPanel.transform;
                descPanel.GetChild(0).GetComponent<Text>().text = thisItem.name;
                descPanel.GetChild(1).GetComponent<Text>().text = thisItem.description;
                descPanel.GetChild(2).GetComponent<Image>().sprite = thisItem.sprite;
                if (thisItem.atk > 0)
                {
                    descPanel.GetChild(2).gameObject.SetActive(true);
                    descPanel.GetChild(3).GetComponent<Text>().text = thisItem.atk.ToString();
                }
                descPanel.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                descPanel.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { SelectHotItem(id); });
                descPanel.GetChild(5).GetComponent<Button>().onClick.RemoveAllListeners();
                descPanel.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { ItemToTrash(id); });
            }
        }
        else //hot item panel
        {
            //Debug.Log(thisItem.itemEffectType.ToString());
            if (thisItem.itemEffectType.ToString() == "BuffPlayerAtk")
            {
                StartCoroutine(BuffPlayerAtk(30, 1.5f));
            }
        }
        
    }
    IEnumerator BuffPlayerAtk (float timeSec, float multiplyAtkBy)
    {
        Debug.Log("up");
        float formerAtk = mc.player.atk;
        mc.player.atk *= multiplyAtkBy;
        yield return new WaitForSeconds(timeSec);
        mc.player.atk = formerAtk;
    }

    public void ItemToTrash(int id)
    {
        if (mc.inventoryManager.items[id].owned > 0)
            mc.inventoryManager.items[id].owned--;
    }
    public void SelectHotItem(int id)
    {
        
        if (!mc.inventoryManager.hotItem.Contains(id))
        {
            if (mc.inventoryManager.hotItem.Count >= 3)
            {

                mc.inventoryManager.hotItem.Dequeue();
            }
            mc.inventoryManager.hotItem.Enqueue(id);
        }
        else
        {
            mc.inventoryManager.hotItem = new Queue<int>(mc.inventoryManager.hotItem.Where(x => x != id));
            
        }
        
        string hot = "";
        foreach (int i in mc.inventoryManager.hotItem)
        {
            hot += i + " ";
        }
        Debug.Log(hot);
    }
}
