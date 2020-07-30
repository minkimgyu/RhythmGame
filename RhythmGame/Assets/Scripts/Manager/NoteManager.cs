using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;
    int ran = -1;

    bool noteActive = true;

    public Transform[] tfNoteAppear;

    public TimingManager[] theTimingManager;
    public EffectManager theEffectManager;
    ComboManager theComboManager;

    private void Start()
    {
        theTimingManager = GetComponentsInChildren<TimingManager>();
        theComboManager = FindObjectOfType<ComboManager>();
    }

    private int[] WhatNote(int ranNum)
    {
        // 같은 타이밍에 한 곳애서 두 개의 노트가 생성되면 안 됨
        // 매개 변수로 생성될 노트의 개수를 받아서 (0, 1, 2, 3) 중에서 랜덤으로 고른다.

        int[] tempArray = new int[4] { 0, 0, 0, 0 };
        List<int> tempList = new List<int>() { 0, 1, 2, 3 };
        for (int i = 0; i < tempList.Count; i++)
        {
            int temp = Random.Range(0, tempList.Count);
            tempArray[i] = tempList[temp];
            tempList.RemoveAt(temp);
        }

        int[] usingArray = new int[ranNum];

        for (int i = 0; i < ranNum; i++)
            usingArray[i] = tempArray[i];

        return usingArray;
    }

    // Update is called once per frame
    void Update()
    {
        if (noteActive)
        {
            currentTime += Time.deltaTime;

            ran = Random.Range(1, 3); // 한번에 생성될 노트의 개수를 정함

            if (currentTime >= 60d / bpm)
            {
                // 여기에 for문을 씌워서 배열의 개수만큼 돌린다

                int[] temp = WhatNote(ran);

                for (int i = 0; i < temp.Length; i++)
                {
                    GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                    t_note.transform.position = tfNoteAppear[temp[i]].position;
                    t_note.transform.rotation = Quaternion.Euler(0, 0, 90);
                    t_note.SetActive(true);
                    theTimingManager[temp[i]].boxNoteList.Add(t_note);
                }

                currentTime -= 60d / bpm;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                for (int i = 0; i < theTimingManager.Length; i++)
                {
                    if (theTimingManager[i].boxNoteList.Contains(collision.gameObject))
                    {
                        theTimingManager[i].MissRecord();
                    }
                }

                theEffectManager.judgementEffect(4);
                theComboManager.ResetCombo();
            }

            for (int i = 0; i < theTimingManager.Length; i++)
            {
                // 버그 이유: 타이밍 메니저에서 먼저 리스트에 담긴 아이템을 지워버려서 여기서 아이템을 찾을 수 없음
                if (theTimingManager[i].boxNoteList.Contains(collision.gameObject))
                {
                    theTimingManager[i].boxNoteList.Remove(collision.gameObject);
                    ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    return;
                }
            }
        }
    }

    public void RemoveNote()
    {
        noteActive = false;

        for (int i = 0; i < theTimingManager.Length; i++)
        {
            for (int j = 0; j < theTimingManager[i].boxNoteList.Count; j++)
            {
                theTimingManager[i].boxNoteList[j].SetActive(false);
                ObjectPool.instance.noteQueue.Enqueue(theTimingManager[i].boxNoteList[j]);
            }
        }
    }
}