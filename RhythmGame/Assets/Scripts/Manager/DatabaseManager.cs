using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public enum KeyAction { B1, B2, B3, B4, B5, B6, KEYCOUNT }

public enum Difficulty { Easy, Normal, Hard }

[System.Serializable]
public class MusicTable // 음악에 대한 정보를 담고 있음
{
    public string musicName;
    public string composer;
    public string spriteName;
    public Difficulty difficulty;
    public int[] score;
}

public class Database // 음악에 대한 정보를 담고 있음
{
    public Dictionary<KeyAction, KeyCode> keySetting = new Dictionary<KeyAction, KeyCode>();
    public List<MusicTable> musicTables = new List<MusicTable>();

    public float bgmPlayerVolume = 1;
    public float sfxPlayerVolume = 1;

    public int cardNum = -1; // 플레이 할 곡의 인덱스 정보
    public int lines = 4; // 라인 수 정함
    public int speed = 1; // 노트의 속도를 정함
}

public class DatabaseManager : MonoBehaviour
{
    // 기본 키 설정
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.Z, KeyCode.X, KeyCode.N, KeyCode.M, KeyCode.C, KeyCode.B };
    public List<MusicTable> defaultmusicTables = new List<MusicTable>();
    // Resources.Load를 이용해서 로드 한 후에 여기에 저장하여 사용하자
    public Dictionary<string, Sprite> musicImage = new Dictionary<string, Sprite>();

    private Database data = new Database();
    public Database returnData { get { return data; }}
    public Database setData { set { data = value; }}

    private static DatabaseManager Instance;

    public static DatabaseManager instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<DatabaseManager>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newobj = new GameObject().AddComponent<DatabaseManager>();
                    Instance = newobj;
                }
            }
            return Instance;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<DatabaseManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        // 이곳에서 앞으로 사용할 키 세팅이나 뮤직 테이블을 불러온다.

        Application.targetFrameRate = 90;

        if (!CheckFileExist()) // default 데이터를 만들어서 피일이 없다면 여기에 넣어줌
            SetDefaultData();
        else
            LoadData();


        for (int i = 0; i < data.musicTables.Count; i++)
        {
            Debug.Log(data.musicTables[i].musicName);
            Debug.Log(data.musicTables[i].spriteName);
        }

        for (int j = 0; j < data.musicTables.Count; j++)
        {
            string name = data.musicTables[j].spriteName;

            Debug.Log(Resources.Load<Sprite>("ImgBGM0"));

            musicImage.Add(name, Resources.Load<Sprite>("TableImage/ImgBGM1"));
            print(musicImage[name]);
        }
    }

    private void SetDefaultData()
    {
        data.musicTables = defaultmusicTables;
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++) // --> 키 매니저가 인게임 씬에 없어서 여기로 옮겨준다.
        {
            data.keySetting.Add((KeyAction)i, defaultKeys[i]);
        }
    }

    public void ReturnTable(int num)
    {
        data.cardNum = num;
    }

    public void ChangeDifficulty(MusicCard card, Difficulty difficulty)
    {
        MusicTable table = data.musicTables.Find(x => x.musicName.Equals(card.musicName.text));
        table.difficulty = difficulty;

        card.SetCard(table);
    }

    public void SetScore(int score)
    {
        if (data.musicTables[data.cardNum].difficulty.Equals(Difficulty.Easy))
        {
            data.musicTables[data.cardNum].score[0] = score;
            data.musicTables[data.cardNum].difficulty = Difficulty.Easy;
        }
        else if (data.musicTables[data.cardNum].difficulty.Equals(Difficulty.Normal))
        {
            data.musicTables[data.cardNum].score[1] = score;
            data.musicTables[data.cardNum].difficulty = Difficulty.Easy;
        }
        else if (data.musicTables[data.cardNum].difficulty.Equals(Difficulty.Hard))
        {
            data.musicTables[data.cardNum].score[2] = score;
            data.musicTables[data.cardNum].difficulty = Difficulty.Easy;
        }
        SaveData();
    }

    public bool CheckFileExist()
    {
        return File.Exists(Application.dataPath + "/GameData.json");
    }

    public void SaveData()
    {
        // 파일이 존재할 경우 기존 값과 달라졌는지를 확인하고 저장함
        if (File.Exists(Application.dataPath + "/GameData.json") &&
            File.ReadAllText(Application.dataPath + "/GameData.json").Equals(JsonConvert.SerializeObject(data)))
            return;
        string jTableData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.dataPath + "/GameData.json", jTableData);
    }

    public void LoadData()
    {
        // 만일 이미 저장해둔 파일이 존재한다면
        if (!CheckFileExist() || File.ReadAllText(Application.dataPath + "/GameData.json").Equals(JsonConvert.SerializeObject(data)))
            return;
        string jTableData = File.ReadAllText(Application.dataPath + "/GameData.json");
        data = JsonConvert.DeserializeObject<Database>(jTableData);
    }
}