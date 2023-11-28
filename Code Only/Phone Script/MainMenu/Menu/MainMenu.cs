using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetSFXVolume (float SfxVolume)
    {
        Debug.Log(SfxVolume);
        audioMixer.SetFloat("SFXVolume", SfxVolume);
    }

    public void SetBGVolume(float BgVolume)
    {
        Debug.Log(BgVolume);
        audioMixer.SetFloat("BGVolume", BgVolume);
    }

    public void SetGraphics (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);     
    }
}
