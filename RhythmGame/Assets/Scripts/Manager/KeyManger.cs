using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManger : MonoBehaviour
{
    int key = -1;
    Text[] buttonTxt = new Text[6];
    public GameObject[] Lines = new GameObject[6];

    public GameObject keyOptionPanel;
    public GameObject optionPanel;

    Dictionary<KeyAction, KeyCode> tempKeySetting = new Dictionary<KeyAction, KeyCode>();
    bool isSave = false;

    DatabaseManager theDatabaseManager;

    public void SetKeyOptionPanel() // 이동시킴
    {
        if (optionPanel.activeSelf == true)
        {
            optionPanel.SetActive(false);
            keyOptionPanel.SetActive(true);

            tempKeySetting = theDatabaseManager.returnData.keySetting;
        }
        else
        {
            keyOptionPanel.SetActive(false);

            if (!isSave)
            {
                // 만일 저장이 안 되었을 경우 이전에 세이브한 값을 넣어준다.
                theDatabaseManager.returnData.keySetting = tempKeySetting;
            }
            else
                isSave = false;
        }
    }

    public void SaveKeyOption() // 이동시킴
    {
        theDatabaseManager.SaveData();
        // 저장하는 코드 추가
        isSave = true;
    }

    private void Start()
    {
        theDatabaseManager = DatabaseManager.instance;

        for (int i = 0; i < Lines.Length; i++)
        {
            buttonTxt[i] = Lines[i].GetComponentInChildren<Text>();
        }
        SetText();
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            theDatabaseManager.returnData.keySetting[(KeyAction)key] = keyEvent.keyCode;
            SetText();
            key = -1;
        }
    }

    public void SetLine(int lineNum)
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            if (i < lineNum)
            {
                if (Lines[i].activeSelf == false)
                    Lines[i].SetActive(true);
            }
            else if (i >= lineNum)
            {
                if (Lines[i].activeSelf == true)
                    Lines[i].SetActive(false);
            }
        }
    }

    public void ChangeKey(int num)
    {
        key = num;
    }

    private void SetText()
    {
        for (int i = 0; i < buttonTxt.Length; i++)
        {
            string tempTxt = theDatabaseManager.returnData.keySetting[(KeyAction)i].ToString();

            if (!buttonTxt[i].text.Equals(tempTxt))
                buttonTxt[i].text = tempTxt;
        }
    }
}