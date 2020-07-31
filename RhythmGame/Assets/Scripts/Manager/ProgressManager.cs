using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    NoteManager theNoteManager;
    Result theResult;
    AudioManager theAudioManager;

    public Image progress;

    bool endGame = true;
    bool startGame = false;


    // Start is called before the first frame update
    void Start()
    {
        theAudioManager = AudioManager.instance;
        theNoteManager = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theAudioManager.IsBGMPlaying())
        {
            progress.fillAmount = theAudioManager.CheckProgress();
            if (startGame == false)
                startGame = true;
        }

        if(theAudioManager.IsBGMPlaying() == false && startGame == true && endGame == true)
        {
            endGame = false;
            theNoteManager.RemoveNote();
            // 이곳에 점수판을 넣기
            theResult.ShowResult();
        }
    }
}
