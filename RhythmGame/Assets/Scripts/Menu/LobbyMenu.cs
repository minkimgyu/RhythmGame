using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionPanel;
    public GameObject soundOptionPanel;
    public GameObject keyOptionPanel;

    public SoundManager theSoundManager;
    public AudioManager theAudioManager;

    bool isActive = false;

    private void Start()
    {
        theAudioManager = AudioManager.instance;
    }

    public void PlayGame()
    {
        // 씬 로드를 사용하지 말고 패널을 끄는 방식으로 진행하자
        if (!theAudioManager.IsBGMPlaying())
            theAudioManager.ReplayBGM();

        startPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit() // 어플리케이션 종료
#endif
    }

    public void SetOptionPanel()
    {
        isActive = !isActive;
        optionPanel.SetActive(isActive);
    }

    public void SetSoundOptionPanel()
    {
        isActive = !isActive;
        optionPanel.SetActive(isActive);
        soundOptionPanel.SetActive(!isActive);
    }

    public void SetSoundOptionPanelLeft()
    {
        soundOptionPanel.SetActive(isActive);
        theSoundManager.CheckIsPlaying();
    }

    public void SetKeyOptionPanel()
    {
        isActive = !isActive;
        optionPanel.SetActive(isActive);
        keyOptionPanel.SetActive(!isActive);
    }

    public void SetKeyOptionPanelLeft()
    {
        keyOptionPanel.SetActive(isActive);
    }
}
