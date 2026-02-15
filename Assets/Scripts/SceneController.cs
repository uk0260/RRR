using UnityEngine;
using UnityEngine.SceneManagement; // 🚨 중요! 씬 이동 기능을 쓰려면 이 도구 상자가 꼭 필요해.

public class SceneController : MonoBehaviour
{
    // 버튼에 연결할 함수니까 반드시 'public'이어야 해
    public void GoToRoom()
    {
        // "Room"이라는 이름의 씬을 불러온다.
        // (주의: 씬 파일 이름이랑 대소문자까지 똑같아야 해!)
        SceneManager.LoadScene("Room");
    }

    public void GoToLoad()
    {
        SceneManager.LoadScene("Load");
    }

}