using UnityEngine;
using UnityEngine.EventSystems; // UI 이벤트를 쓰기 위해 꼭 필요해!

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("설정")]
    public float scaleSize = 1.2f; // 커졌을 때의 크기 배율 (1.2배)
    
    private Vector3 defaultScale;  // 원래 크기를 기억할 변수

    void Start()
    {
        // 게임 시작할 때 버튼의 원래 크기를 저장해둠
        defaultScale = transform.localScale;
    }

    // 마우스가 버튼 위에 올라갔을 때 (OnPointerEnter)
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = defaultScale * scaleSize; // 크기를 키운다
    }

    // 마우스가 버튼 밖으로 나갔을 때 (OnPointerExit)
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale; // 원래 크기로 되돌린다
    }
}