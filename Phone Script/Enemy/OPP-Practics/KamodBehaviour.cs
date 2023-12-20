using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;//เรียก function การใช้ A*pathfinding


public class KamodBehaviour : MonoBehaviour
{
    //public Player player = new Player();
    public MainController mc;
    public EnemyData Kamod;
    public PlayerBehavior PlayerRender;      

    public AIPath aiPath;
    public bool CanSee = false;

    // Start is called before the first frame update
    void Start()
    {
        Kamod = new EnemyData("Kamod", 20, 10);
       
        Kamod.ShowInfo(Kamod.Name);

    }

    // Update is called once per frame
    void Update()
    {        
        if (Kamod.Hp == 0)
        {
            Destroy(this.gameObject);
        }
        //for Looking player
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attacking(collision);
        ReceiveDmg(collision);     
        
    }
    public void Attacking(Collider2D collision)
    {
        if (Kamod.Hp > 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                print("Collab!");
                mc.player.health = mc.player.health - Kamod.Atk;
                Debug.LogWarning("Now Player HP: " + mc.player.health);
            }

            if (mc.player.health <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Test");
            }

        }
    }

    public void ReceiveDmg(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Kamod.Hp > 0)
        {
            if (mc.player.IsAtk == true)
            {
                Kamod.Hp = Kamod.Hp - (int)mc.player.atk;
                Debug.LogWarning("Kamod HP :" + Kamod.Hp);
            }
        }
        else if (collision.gameObject.tag == "Player" && Kamod.Hp <= 0)
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Invoke("Attacking", 3);
    }

    public void Seeing()
    {
        aiPath.canSearch = true;
    }

    public void UnSeeing()
    {
        aiPath.canSearch = false;
    }


}


