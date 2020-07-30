using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    int[] judgementRecord = new int[5];

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    [SerializeField] Image containerImage;

    Color normal = new Color(0, 141f / 255f, 183f / 255f, 0);
    Color click = new Color(0, 141f / 255f, 183f / 255f, 70f / 255f);

    public EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.y - (timingRect[i].rect.height / 2), Center.localPosition.y + (timingRect[i].rect.height / 2));
        }
    }

    public void setClickColor()
    {
        if(containerImage.color != click)
            containerImage.color = click;

        Debug.Log(containerImage.color.r);
        Debug.Log(containerImage.color.g);
        Debug.Log(containerImage.color.b);
        Debug.Log(containerImage.color.a);
    }

    public void setUpColor()
    {
        if (containerImage.color != normal)
            containerImage.color = normal;

        Debug.Log(containerImage.color.r);
        Debug.Log(containerImage.color.g);
        Debug.Log(containerImage.color.b);
        Debug.Log(containerImage.color.a);
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosY = boxNoteList[i].transform.localPosition.y;

            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosY && t_notePosY <= timingBoxs[j].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    if (j < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();
                    theEffect.judgementEffect(j);
                    judgementRecord[j]++;

                    theScoreManager.IncreaseScore(j);
                    return;
                }
            }
        }
        //theComboManager.ResetCombo();
        //theEffect.judgementEffect(timingBoxs.Length);
        //MissRecord();
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;
    }
}
