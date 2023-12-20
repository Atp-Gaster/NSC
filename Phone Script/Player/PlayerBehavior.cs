using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Animator Anim;
    SpriteRenderer Render;
    Rigidbody2D body;

    [SerializeField] float MoveSpeedRun = 0;
    [SerializeField] float MoveSpeedWalk = 0;
    [SerializeField] bool Attaking = false;

    public Player player;
    public MainController mc;

    public Vector3 PlayerXYZ;


    public bool CanWalk = true;
    public int LevelDialogEagle = 0;
    void Start()
    {
        Anim = this.GetComponent<Animator>();
        Render = this.transform.Find("Render").GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();

        
    }

    
    void Update()
    {
        Attack();
        WalkingPC();
    }
       
    void Stop()
    {
        CanWalk = false;
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.G))
        {
            mc.player.IsAtk = true;
            Attaking = true;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            mc.player.IsAtk = false;
            Attaking = false;
        }
    }
   
    void WalkingPC()
    {
        PlayerXYZ = this.transform.localPosition;



        if (CanWalk == true)
        {
            //Computer Version
            if (Input.GetKey(KeyCode.W))//forward
            {
                //walk
                PlayerXYZ.y = PlayerXYZ.y + MoveSpeedWalk;
                this.transform.localPosition = PlayerXYZ;
                //Animate
                Anim.SetBool("Walk", true);
                Anim.SetBool("Back", true);
                //Run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    print("Running");
                    PlayerXYZ.y = PlayerXYZ.y + MoveSpeedRun;
                    this.transform.localPosition = PlayerXYZ;

                    Anim.SetBool("Run", true);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Anim.SetBool("Run", false);
                    print("Walking");

                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Anim.SetBool("Walk", false);
                Anim.SetBool("Back", false);
            }

            if (Input.GetKey(KeyCode.A))//Left

            {
                //walk
                PlayerXYZ.x = PlayerXYZ.x - MoveSpeedWalk;
                this.transform.localPosition = PlayerXYZ;
                //Animate
                Anim.SetBool("Walk", true);
                Render.flipX = false;
                //Run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerXYZ.x = PlayerXYZ.x - MoveSpeedRun;
                    this.transform.localPosition = PlayerXYZ;

                    Anim.SetBool("Run", true);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Anim.SetBool("Run", false);
                }

            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                Anim.SetBool("Walk", false);
            }
            if (Input.GetKey(KeyCode.S))//Backward
            {
                //walk
                PlayerXYZ.y = PlayerXYZ.y - MoveSpeedWalk;
                this.transform.localPosition = PlayerXYZ;
                //Animate
                Anim.SetBool("Walk", true);
                //Run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerXYZ.y = PlayerXYZ.y - MoveSpeedRun;
                    this.transform.localPosition = PlayerXYZ;

                    Anim.SetBool("Run", true);

                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Anim.SetBool("Run", false);
                }

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                Anim.SetBool("Walk", false);
            }

            if (Input.GetKey(KeyCode.D))//Right

            {
                //walk
                PlayerXYZ.x = PlayerXYZ.x + MoveSpeedWalk;
                this.transform.localPosition = PlayerXYZ;
                //Animate
                Anim.SetBool("Walk", true);
                Render.flipX = true;
                //Run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerXYZ.x = PlayerXYZ.x + MoveSpeedRun;
                    this.transform.localPosition = PlayerXYZ;

                    Anim.SetBool("Run", true);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Anim.SetBool("Run", false);
                }

            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                Anim.SetBool("Walk", false);

            }

        }
    }

   
}
