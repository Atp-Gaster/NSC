using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayContious : MonoBehaviour
{
    void Awake()
    {
        GameObject[] Music = GameObject.FindGameObjectsWithTag("MusicPlayer");
        if (Music.Length > 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
