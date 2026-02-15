using UnityEngine;
using UnityEngine.UI; // 이미지(UI)를 다뤄야 하니까 꼭 필요해요!

public class SoundManager : MonoBehaviour
{
    [Header("UI 설정")]
    public Image targetImage;      // 그림을 바꿀 대상 (버튼에 있는 Image)
    public Sprite soundOnSprite;   // 소리 켜짐 그림 (🔊)
    public Sprite soundOffSprite;  // 소리 꺼짐 그림 (🔇)

    private bool isMuted = false;  // 현재 소리가 꺼져있는지 기억하는 변수

    void Start()
    {
        // 게임 시작할 때 상태 초기화 (처음엔 소리 켜짐 상태)
        isMuted = false;
        AudioListener.volume = 1; // 소리 크기 100%
        targetImage.sprite = soundOnSprite;
    }

    // 버튼을 누르면 이 함수를 실행하세요
    public void ToggleSound()
    {
        // 1. 상태 뒤집기 (켜져 있으면 끄고, 꺼져 있으면 켜라)
        isMuted = !isMuted;

        // 2. 상태에 따라 소리와 그림 바꾸기
        if (isMuted == true)
        {
            // 소리 끄기
            AudioListener.volume = 0; // 전체 볼륨 0
            targetImage.sprite = soundOffSprite; // 🔇 그림으로 교체
        }
        else
        {
            // 소리 켜기
            AudioListener.volume = 1; // 전체 볼륨 1 (최대)
            targetImage.sprite = soundOnSprite; // 🔊 그림으로 교체
        }
    }
}