using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject musicCard;

    public GameObject startPanel;
    public Transform content;

    MusicCard[] cards;

    DatabaseManager TheDatabaseManager;
    AudioManager TheAudioManager;
    Transform nowCard;
    float scroll_pos = 0;
    float[] pos;
    float distance = 0;

    int cardNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        TheDatabaseManager = DatabaseManager.instance;
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
        for (int i = 0; i < TheDatabaseManager.returnData.musicTables.Count; i++)
        {
            MusicCard card = Instantiate(musicCard, content).GetComponent<MusicCard>();
            card.SetCard(TheDatabaseManager.returnData.musicTables[i]);
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
            TheDatabaseManager.ChangeDifficulty(cards[i], Difficulty.Easy);
        }
    }

    public void SetDifficulty() // 버튼을 눌러서 난이도를 변경한다.
    {
        MusicCard card = nowCard.GetComponent<MusicCard>();

        if (card.difficulty.text.Equals("Easy"))
        {
            TheDatabaseManager.ChangeDifficulty(card, Difficulty.Normal);
        }
        else if (card.difficulty.text.Equals("Normal"))
        {
            TheDatabaseManager.ChangeDifficulty(card, Difficulty.Hard);
        }
        else if (card.difficulty.text.Equals("Hard"))
        {
            TheDatabaseManager.ChangeDifficulty(card, Difficulty.Easy);
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
        TheDatabaseManager.ReturnTable(cardNum); // 데이터 베이스에 어떤 테이블을 로드할지 보내준다.
        TheAudioManager.StopBGM(); // 인 게임 씬에서 필요한 노래를 로드하고 다시 실행시킨다.
        Debug.Log("게임 시작");
        SceneManager.LoadScene("InGame");
    }
}
