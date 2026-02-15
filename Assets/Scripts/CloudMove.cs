using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [Header("이동 설정")]
    public float speed = 1.0f;        // 구름 이동 속도
    public float endX = -10.0f;       // 구름이 사라지는 왼쪽 끝 위치 (이제 음수여야겠지?)
    public float startX = 10.0f;      // 구름이 다시 나타날 오른쪽 시작 위치 (양수)

    [Header("흔들림 설정")]
    public float amplitude = 0.5f;    // 위아래로 움직이는 폭 (높이)
    public float frequency = 1.0f;    // 위아래로 움직이는 빠르기

    private float originalY;          // 처음에 배치했던 Y 높이를 기억하는 변수

    void Start()
    {
        // 게임 시작할 때 구름의 처음 높이(Y)를 기억해둔다
        originalY = transform.position.y;
    }

    void Update()
    {
        // 1. 왼쪽으로 이동 (Vector3.left)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 2. 위아래 흔들림 (Sin 그래프 활용)
        // 원래 높이(originalY)를 기준으로 위아래로 왔다갔다 하게 만듦
        float newY = originalY + Mathf.Sin(Time.time * frequency) * amplitude;

        // 현재 X위치, 계산된 Y위치, 현재 Z위치를 적용
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 3. 왼쪽 끝(endX)을 지나가면? 오른쪽 끝(startX)으로 보냄
        if (transform.position.x <= endX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startX;
            transform.position = newPos;
        }
    }
}