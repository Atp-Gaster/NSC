using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void PauseGames()
    {
        Time.timeScale = 0;
    }

    public void ResumeGames()
    {
        Time.timeScale = 1;
    }
}
