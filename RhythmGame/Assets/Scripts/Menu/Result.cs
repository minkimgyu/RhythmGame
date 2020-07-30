using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] Text[] txtCount = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager[] theTiming;

    int count = 5;

    // Start is called before the first frame update
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectsOfType<TimingManager>();
    }

    public void ShowResult()
    {
        goUI.SetActive(true);

        for (int i = 0; i < txtCount.Length; i++)
            txtCount[i].text = "0";

        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] t_judgement = new int[5] { 0, 0, 0, 0, 0 };

        for (int x = 0; x < theTiming.Length; x++)
        {
            for (int j = 0; j < count; j++)
            {
                t_judgement[j] += theTiming[x].GetJudgementRecord()[j];
            }
        }

        int t_currentScore = theScore.GetCurrentScore();
        int t_maxCombo = theCombo.GetMaxCombo();

        for (int z = 0; z < txtCount.Length; z++)
        {
            txtCount[z].text = string.Format("{0:#,##0}", t_judgement[z]);
        }

        txtScore.text = string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);
    }
}
