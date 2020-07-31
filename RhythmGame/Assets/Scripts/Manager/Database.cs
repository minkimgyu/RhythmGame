using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction { B1, B2, B3, B4, B5, B6, KEYCOUNT }

public class KeySetting
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
}

public class Database : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.M, KeyCode.N, KeyCode.X, KeyCode.Z, KeyCode.X, KeyCode.Z };

    private static Database Instance;

    public static Database instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<Database>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newobj = new GameObject().AddComponent<Database>();
                    Instance = newobj;
                }
            }
            return Instance;
        }
    }

    public int cardNum = -1;
    public List<MusicTable> musicTables = new List<MusicTable>();

    private void Awake()
    {
        var objs = FindObjectsOfType<Database>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);



        // 이곳에서 앞으로 사용할 키 세팅이나 뮤직 테이블을 불러온다.

        Application.targetFrameRate = 90;

        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++) // --> 키 매니저가 인게임 씬에 없어서 여기로 옮겨준다.
        {
            //KeySetting key = new KeySetting();
            //key.keys.Add((KeyAction)i, defaultKeys[i]);
           KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }

    public void ReturnTable(int num)
    {
        cardNum = num;
    }

    public void ChangeDifficulty(MusicCard card, Difficulty difficulty)
    {
        MusicTable table = musicTables.Find(x => x.musicName.Equals(card.musicName.text));
        table.difficulty = difficulty;

        card.SetCard(table);
    }
}
