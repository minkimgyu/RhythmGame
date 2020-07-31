using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioManager theAudioManager;
    bool isPlaying = false;

    private void Start()
    {
        theAudioManager = AudioManager.instance;
    }

    public void SetBGMVolume(float volume)
    {
        theAudioManager.SetBGMVolume(volume);
    }

    public void SetSfXVolume(float volume)
    {
        theAudioManager.SetSfXVolume(volume);
    }

    public void PlayAndStopBGM()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
            theAudioManager.PlayBGM("BGM2");
        else
            theAudioManager.StopBGM();
    }

    public void CheckIsPlaying()
    {
        if (isPlaying)
        {
            theAudioManager.StopBGM();
            isPlaying = !isPlaying;
        }
    }
}
