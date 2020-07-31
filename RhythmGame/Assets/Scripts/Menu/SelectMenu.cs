using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Difficulty { Easy, Normal, Hard }

[System.Serializable]
public class MusicTable // 음악에 대한 정보를 담고 있음
{
    public string musicName;
    public string composer;
    public Sprite sprite;
    public Difficulty difficulty;
    public int[] score = new int[3] { 0, 0, 0 };

    [HideInInspector] public Color easy = new Color(173f / 255f, 255f / 255f, 165f / 255f);
    [HideInInspector] public Color normal = new Color(165f / 255f, 232f / 255f, 255f / 255f);
    [HideInInspector] public Color hard = new Color(255f / 255f, 168f / 255f, 165f / 255f);
}

public class SelectMenu : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject musicCard;

    public GameObject startPanel;
    public Transform content;

    MusicCard[] cards;

    Database TheDatabase;
    AudioManager TheAudioManager;
    Transform nowCard;
    float scroll_pos = 0;
    float[] pos;
    float distance = 0;

    int cardNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        TheDatabase = Database.instance;
        TheAudioManager = AudioManager.instance;

        SetMusicTable(); // 카드를 생성시킨다. --> 카드 생성이 우선임

        cards = content.GetComponentsInChildren<MusicCard>(); // 카드 생성 이후 카드를 받아온다.

        pos = new float[content.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i; // 1/5, 2/5, 3/5, 4/5, 5/5
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, pos[i], 0.1f);
                }
            }

            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    content.GetChild(i).localScale = Vector2.Lerp(content.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                    if (nowCard != content.GetChild(i) && startPanel.activeSelf == false)
                    {
                        nowCard = content.GetChild(i); // 미리 배열에 넣어두고 i 인덱스로 찾아서 재생하자

                        TheAudioManager.PlayBGM(cards[i].musicName.text);
                        cardNum = i;

                        SetDifficultyDefault(); // 모든 난이도를 Easy로 초기화 해준다.
                    }
                }
                else
                    content.GetChild(i).localScale = Vector2.Lerp(content.GetChild(i).localScale, new Vector2(0.8f, 0.8f), 0.1f);
            }
        }

    }

    public void SetMusicTable()
    {
        for (int i = 0; i < TheDatabase.musicTables.Count; i++)
        {
            MusicCard card = Instantiate(musicCard, content).GetComponent<MusicCard>();
            card.SetCard(TheDatabase.musicTables[i]);
        }
    }

    public bool CheckNowCard(MusicCard card_)
    {
        return nowCard.GetComponent<MusicCard>() == card_;
    }

    public void SetDifficultyDefault() // 버튼을 눌러서 난이도를 변경한다.
    {
        for (int i = 0; i < cards.Length; i++)
        {
            TheDatabase.ChangeDifficulty(cards[i], Difficulty.Easy);
        }
    }

    public void SetDifficulty() // 버튼을 눌러서 난이도를 변경한다.
    {
        MusicCard card = nowCard.GetComponent<MusicCard>();

        if (card.difficulty.text.Equals("Easy"))
        {
            TheDatabase.ChangeDifficulty(card, Difficulty.Normal);
        }
        else if (card.difficulty.text.Equals("Normal"))
        {
            TheDatabase.ChangeDifficulty(card, Difficulty.Hard);
        }
        else if (card.difficulty.text.Equals("Hard"))
        {
            TheDatabase.ChangeDifficulty(card, Difficulty.Easy);
        }
    }

    public void BackToMenu() // 로비 매뉴에 넣기
    {
        startPanel.SetActive(true);
        if (TheAudioManager.IsBGMPlaying())
        {
            TheAudioManager.StopBGM();
        }
        scrollbar.value = 0;
        scroll_pos = scrollbar.value;
        SetDifficultyDefault();
    }

    public void GoToGamePlayScene()
    {
        TheDatabase.ReturnTable(cardNum); // 데이터 베이스에 어떤 테이블을 로드할지 보내준다.
        TheAudioManager.StopBGM(); // 인 게임 씬에서 필요한 노래를 로드하고 다시 실행시킨다.
        Debug.Log("게임 시작");
        SceneManager.LoadScene("InGame");
    }
}
