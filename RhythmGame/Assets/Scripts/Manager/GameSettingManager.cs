using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingManager : MonoBehaviour
{
    Database TheDatabase;

    // Start is called before the first frame update
    void Start()
    {
        TheDatabase = Database.instance;

        // 배경 이미지를 가져와서 배경에 배치하자
        // 난이도, 배속 정보, 라인 수, 노래 제목, 노래 작곡가를 볼 수 있게 하자
        // 설정에 라인 추가, 배속 추가하자
        // Json을 이용하여 데이터를 저장하고 로드하자
        // 키세팅 클레스를 static를 없애고 Database 클레스의 싱글톤으로 접근하자
        // 이후 게임을 킬 때, 저장된 키세팅 클레스를 로드하는 방식으로 하자
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
