using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public MainController mc;
    //public GameObject backBtn;
    public GameObject profilePanel;
    public Text healthText;
    public Text manaText;
    public Text moneyText;

    public void OpenProfile()
    {
        profilePanel.SetActive(true);
        healthText.text = "Health: " + mc.player.health.ToString();
        manaText.text = "Mana: " + mc.player.mana.ToString();
        moneyText.text = "Money: " + mc.player.money.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
