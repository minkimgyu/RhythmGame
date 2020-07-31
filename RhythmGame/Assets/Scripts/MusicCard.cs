﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicCard : MonoBehaviour
{
    public Text difficulty;
    public Image difficultyImage;
    public Image cardImage;
    public Text musicName;
    public Text composer;
    public Text score;

    SelectMenu TheSelectMenu;

    private void Start()
    {
        TheSelectMenu = FindObjectOfType<SelectMenu>();
    }

    public void SetDifficulty()
    {
        if (TheSelectMenu.CheckNowCard(this))
            TheSelectMenu.SetDifficulty();
    }

    public void SetCard(MusicTable table)
    {
        if (!musicName.text.Equals(table.musicName) || !difficulty.text.Equals(table.difficulty.ToString())) 
        {
            difficulty.text = table.difficulty.ToString();
            cardImage.sprite = table.sprite;
            musicName.text = table.musicName;
            composer.text = table.composer;
        }
        // 곡의 이름이 다르거나 곡의 난이도가 바뀔 경우 다시 넣어줌

        if (difficulty.text.Equals("Easy")) // 곡의 난이도가 바뀔 때 마다 스코어를 재배치 해준다.
        {
            score.text = table.score[0].ToString(); // 난이도에 따른 점수 변경
            difficultyImage.color = table.easy; // 난이도에 따른 색 변경
        }
        else if(difficulty.text.Equals("Normal"))
        {
            score.text = table.score[1].ToString();
            difficultyImage.color = table.normal;
        }
        else if (difficulty.text.Equals("Hard"))
        {
            score.text = table.score[2].ToString();
            difficultyImage.color = table.hard;
        }
    }

}
