using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("밥 인벤토리 창")]
    public GameObject inventoryPanel;

    [Header("가구 인벤토리 창 (새로 추가!)")]
    public GameObject furniturePanel;

    void Start()
    {
        inventoryPanel.SetActive(false);
        furniturePanel.SetActive(false); // 시작할 때 가구 창도 닫기
    }

    // 밥 버튼 기능 (원래 있던 것)
    public void ToggleInventory()
    {
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);

        // 꿀팁: 밥 창 열 때 가구 창은 닫아주면 깔끔함!
        if (!isActive) furniturePanel.SetActive(false);
    }

    // [NEW] 가구(서랍장) 버튼 기능
    public void ToggleFurniture()
    {
        bool isActive = furniturePanel.activeSelf;
        furniturePanel.SetActive(!isActive);

        // 가구 창 열 때 밥 창은 닫아주기
        if (!isActive) inventoryPanel.SetActive(false);
    }

    //인트로로 돌아가는 함수
    public void GoToIntro()
    {
        // "Title" 부분에 실제 인트로 씬의 이름을 적으세요! (대소문자 정확히)
        SceneManager.LoadScene("Intro");
    }
}