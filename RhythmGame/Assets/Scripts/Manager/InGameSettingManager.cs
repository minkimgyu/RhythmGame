using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettingManager : MonoBehaviour
{
    DatabaseManager theDatabaseManager;

    public Text speed;
    public GameObject optionPanel;
    public GameObject gameSettingPanel;

    bool isSave = false;

    int tempSpeed = -1;
    int tempLines = -1;

    private void Start()
    {
        theDatabaseManager = DatabaseManager.instance;
    }

    public void SetGameSettingPanel() // 이동시킴
    {
        if (optionPanel.activeSelf == true)
        {
            optionPanel.SetActive(false);
            gameSettingPanel.SetActive(true);

            tempSpeed = theDatabaseManager.returnData.speed;
            tempLines = theDatabaseManager.returnData.lines;
        }
        else
        {
            gameSettingPanel.SetActive(false);

            if (!isSave)
            {
                // 만일 저장이 안 되었을 경우 이전에 세이브한 값을 넣어준다.
                theDatabaseManager.returnData.speed = tempSpeed;
                theDatabaseManager.returnData.lines = tempLines;
            }
            else
                isSave = false;
        }
    }

    public void SaveGameOption() // 이동시킴
    {
        theDatabaseManager.SaveData();
        // 저장하는 코드 추가
        isSave = true;
    }

    public void SetLines(int num)
    {
        theDatabaseManager.returnData.lines = num;
    }

    public void SetSpeed(float num)
    {
        theDatabaseManager.returnData.speed = (int)num;
        speed.text = "X" + num;
    }

}
