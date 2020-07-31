using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManger : MonoBehaviour
{
    int key = -1;
    public Text[] buttonTxt;
    public GameObject[] Lines = new GameObject[6];

    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode;
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
            buttonTxt[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }
}