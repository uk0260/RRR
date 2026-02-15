using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    // 날씨 효과들을 담을 가방 (리스트)
    public GameObject[] weatherEffects;

    void Start()
    {
        // 게임 시작하면 날씨 뽑기!
        PickRandomWeather();
    }

    void PickRandomWeather()
    {
        // 1. 일단 모든 날씨를 끕니다. (초기화)
        // 켜져 있는 거 끄고 시작해야 겹치지 않으니까요.
        for (int i = 0; i < weatherEffects.Length; i++)
        {
            weatherEffects[i].SetActive(false);
        }

        // 2. 주사위를 굴립니다! (0번 부터 개수만큼)
        // 예: 비(0), 눈(1) -> 0과 1 중에서 하나 나옴
        int randomIndex = Random.Range(0, weatherEffects.Length);

        // 3. 당첨된 날씨만 켭니다!
        weatherEffects[randomIndex].SetActive(true);

        // (테스트용) 콘솔창에 무슨 날씨인지 알려줌
        Debug.Log("오늘의 날씨 번호: " + randomIndex);
    }
}