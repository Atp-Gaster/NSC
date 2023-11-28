using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Transition : MonoBehaviour
{
    public float transitionDelay = 1f;
    public Animator transition;
    [SerializeField] public bool Start = false;
    bool Lock = false;
    public int Sceennumber = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (Start == true && Lock == false)
        {
            LoadNextLevel();
            Lock = true;
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1));
        
        
    }
   

    IEnumerator LoadLevel(int levelIndex)
    {
        //play
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionDelay);
        //load scene
         switch(Sceennumber)
         {
            case 0:
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            break;
                
            case 1:
            UnityEngine.SceneManagement.SceneManager.LoadScene("Test");
            break;
        } 

     }
     public void GotoNewGame()
    {
        Start = true;
        Sceennumber = 1;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

