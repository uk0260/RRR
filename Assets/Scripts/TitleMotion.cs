using UnityEngine;
using System.Collections;

public class TitleMotion : MonoBehaviour
{
    [Header("타이틀 설정")]
    public RectTransform[] letters; 
    public float moveDistance = 100f; 
    public float moveSpeed = 5f;    
    public float delayTime = 0.2f;  

    [Header("버튼 설정 (새로 추가됨!)")]
    // 우리가 만든 ButtonScale 스크립트를 제어할 거야
    public ButtonScale[] targetButtons; 

    void Start()
    {
        // 1. 게임 시작하자마자 버튼들의 스케일 기능을 꺼버린다! (잠금)
        for (int i = 0; i < targetButtons.Length; i++)
        {
            targetButtons[i].enabled = false; 
        }

        StartCoroutine(AnimateTitle());
    }

    IEnumerator AnimateTitle()
    {
        // (기존 코드: 글자 내리기)
        Vector2[] originalPositions = new Vector2[letters.Length];
        for (int i = 0; i < letters.Length; i++)
        {
            originalPositions[i] = letters[i].anchoredPosition;
            Vector2 startPos = originalPositions[i];
            startPos.y -= moveDistance;
            letters[i].anchoredPosition = startPos;
        }

        // (기존 코드: 글자 하나씩 올리기)
        for (int i = 0; i < letters.Length; i++)
        {
            StartCoroutine(MoveUp(letters[i], originalPositions[i]));
            yield return new WaitForSeconds(delayTime);
        }

        // --- 여기가 중요! ---
        // 글자 애니메이션 루프가 다 끝나고 나서 0.5초 정도 뜸을 들임 (선택 사항)
        yield return new WaitForSeconds(0.5f);

        // 2. 이제 버튼들의 스케일 기능을 켠다! (잠금 해제)
        for (int i = 0; i < targetButtons.Length; i++)
        {
            targetButtons[i].enabled = true;
        }
    }

    IEnumerator MoveUp(RectTransform target, Vector2 dest)
    {
        while (Vector2.Distance(target.anchoredPosition, dest) > 0.1f)
        {
            target.anchoredPosition = Vector2.Lerp(target.anchoredPosition, dest, Time.deltaTime * moveSpeed);
            yield return null; 
        }
        target.anchoredPosition = dest;
    }
}