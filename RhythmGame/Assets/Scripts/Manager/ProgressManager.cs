using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    AudioSource theCenter;
    public Image progress;

    Result theResult;
    NoteManager theNoteManager;
    bool endGame = true;
    bool startGame = false;


    // Start is called before the first frame update
    void Start()
    {
        theNoteManager = FindObjectOfType<NoteManager>();
        theCenter = FindObjectOfType<CenterFrame>().myAudio;
        theResult = FindObjectOfType<Result>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theCenter.isPlaying == true)
        {
            progress.fillAmount = theCenter.time / theCenter.clip.length;
            if(startGame == false)
                startGame = true;
        }

        if(theCenter.isPlaying == false && startGame == true && endGame == true)
        {
            endGame = false;
            theNoteManager.RemoveNote();
            // 이곳에 점수판을 넣기
            theResult.ShowResult();
        }
    }
}
