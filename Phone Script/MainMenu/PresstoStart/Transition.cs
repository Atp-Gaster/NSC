using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Transition : MonoBehaviour
{
    public GameObject loadingScreen;
    public Text progressTxt;
    public float transitionDelay = 1f;
    //public AudioClip ClickSFX;   
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

    public void LoadThisLevel (int index) {
        StartCoroutine(LoadLevel(index));
    }


   

    IEnumerator LoadLevel(int levelIndex)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressTxt.text = progress * 100 + "%";
            yield return null;
        }
        /*
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

            case 2:
            UnityEngine.SceneManagement.SceneManager.LoadScene("InventoryTest");
            break;

            case 3:
            UnityEngine.SceneManagement.SceneManager.LoadScene("DragonNewTestscene");
            break;

         } 
*/
    }
     public void GotoNewGame()
     {
        
        Start = true;
        //Sceennumber = 2;
     }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    /*
    public void PlayClickbottonSFX()
    {
        this.GetComponent<AudioSource>().PlayOneShot(ClickSFX);
    }
    */
}

