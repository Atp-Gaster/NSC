using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text NameText;
    public Text DialogText;
    public MainController mc;
    private Queue<string> sentences;
    private Queue<string> names;
    public Animator animator;
    DialogueEvent dialogueEvent;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialog (DialogueEvent dialogueEvent_, NPC npc = null)
    {
        //Debug.Log("in progress 3");
        dialogueEvent = dialogueEvent_;
        animator.SetBool("Isopen?", true);
        sentences.Clear();
        /*
        if (npc != null)
        {
            //Debug.Log("Start talking with" + npc.name);
            NameText.text = npc.name;
        }*/

        
        
        for (int i = 0; i < dialogueEvent_.speech.Count; i++)
        {
            names.
            Enqueue
            (dialogueEvent_.
            speech[i].
            name);
            sentences.Enqueue(dialogueEvent_.speech[i].speechTxt);
        }
        
        DisplayNextSentence();

        
    }
   
    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        //Debug.Log(sentence);
        //DialogText.text = sentence;
        StartCoroutine(TypeSentence(sentence, name));

    }

    IEnumerator TypeSentence (string sentence, string name)
    {
        DialogText.text = "";
        if (name != null)
            NameText.text = name;
        else 
            NameText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void EndDialog()
    {
        if (dialogueEvent.addStoryProgressAfter)
        {
            mc.player.storyProgress++;
        }

        if (dialogueEvent.actionAfterTalk == ActionAfterDialogue.SetObjectTrueAndView)
        {
            Debug.Log("Pan camera now");
            if (dialogueEvent.objectToBeActive != null)
            {
                dialogueEvent.objectToBeActive.SetActive(true);
                CameraToTarget.focusTarget = dialogueEvent.objectToBeActive.transform;
                CameraToTarget.isFocusing = true;
            }
        }
        else if (dialogueEvent.actionAfterTalk == ActionAfterDialogue.EntityWalkToPlace)
        {
            if (dialogueEvent.entityWalk != null && dialogueEvent.Destination != null)
            {
                StartCoroutine(EntityWalkToPlace(dialogueEvent.entityWalk, dialogueEvent.Destination));
            }
        }

        animator.SetBool("Isopen?", false);
        //Debug.Log("End");
    }

    IEnumerator EntityWalkToPlace (GameObject entity, GameObject destination)
    {
        float inputX = 0;
        float inputY = 0;
        //Rigidbody rb = entity.GetComponent<Rigidbody>();
        Animator animator = entity.transform.GetComponent<Animator>();

        animator.SetBool("Walk", true);
        animator.SetBool("Sit", false);
        Vector3 nextPos = destination.transform.position, whereToMove;
        SpriteRenderer spriteRenderer = entity.transform.Find("CharacterSprite").GetComponent<SpriteRenderer>();
        float speed = 0.1f;
        
        
        while ((nextPos - entity.transform.position).magnitude > 2) //not arrive yet
        {
            //Debug.Log((nextPos - transform.position).magnitude);
            if (nextPos.z < transform.position.z) //front
            {
                //Debug.Log("front");
                animator.SetBool("Front", true);
                inputY = -1;
            }
            else //back
            {
                //Debug.Log("back");
                animator.SetBool("Front", false);
                inputY = 1;
            }

            if (nextPos.x < transform.position.x) //left
            {
                inputX = -1;

                if (inputY == -1) //front 
                {
                    spriteRenderer.flipX = false;
                }
                else //back
                {
                    spriteRenderer.flipX = true;
                }

            }
            else //right
            {
                inputX = 1;
                if (inputY == -1) //front
                {
                    spriteRenderer.flipX = true;
                }
                else //back
                {
                    spriteRenderer.flipX = false;
                }

            }

            whereToMove = (nextPos - entity.transform.position).normalized;
            //Debug.Log(whereToMove);
            entity.transform.position += new Vector3(whereToMove.x * speed, 0, whereToMove.z * speed);

            //rb.velocity = new Vector3(whereToMove.x * speed, 0, whereToMove.z * speed);

            yield return new WaitForEndOfFrame();
        }
        animator.SetBool("Walk", false);
        //arrived
        //rb.velocity = Vector3.zero;


    }
}
