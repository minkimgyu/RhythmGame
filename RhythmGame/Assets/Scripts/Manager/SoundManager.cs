using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioManager theAudioManager;
    DatabaseManager theDatabaseManager;

    public GameObject soundOptionPanel;
    bool isPlaying = false;

    float tempBgmVol = -1;
    float tempSfxVol = -1;

    bool isSave = false;

    public GameObject optionPanel;

    private void Start()
    {
        theAudioManager = AudioManager.instance;
        theDatabaseManager = DatabaseManager.instance;
    }

    public void SetSoundOptionPanel() // 이동시킴
    {
        if (optionPanel.activeSelf == true)
        {
            optionPanel.SetActive(false);
            soundOptionPanel.SetActive(true);
            tempBgmVol = theAudioManager.GetBGMVolume(); // 켜질 때 가져옴
            tempSfxVol = theAudioManager.GetSFXVolume();
        }
        else
        {
            soundOptionPanel.SetActive(false);
            CheckIsPlaying();

            if (!isSave)
            {
                // 만일 저장이 안 되었을 경우 이전에 세이브한 값을 넣어준다.
                theAudioManager.SetBGMVolume(tempBgmVol);
                theAudioManager.SetSFXVolume(tempSfxVol);
            }
            else
                isSave = false;

        }
    }

    public void SaveSoundOption() // 이동시킴
    {
        theDatabaseManager.SaveData();
        // 저장하는 코드 추가
        isSave = true;
    }

    public void SetBGMVolume(float volume)
    {
        theAudioManager.SetBGMVolume(volume);
    }

    public void SetSfXVolume(float volume)
    {
        theAudioManager.SetSFXVolume(volume);
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
