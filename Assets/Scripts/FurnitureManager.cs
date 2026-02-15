using UnityEngine;
using UnityEngine.UI; // ⭐ UI(이미지)를 다루기 위해 꼭 필요해요!

public class FurnitureManager : MonoBehaviour
{
    [Header("방에 있는 실제 카페트 (SpriteRenderer)")]
    public SpriteRenderer roomCarpet;

    [Header("인벤토리 아이템 버튼의 그림 (Image)")]
    public Image inventoryItemImage;

    // 버튼을 누르면 이 함수가 실행돼요
    public void SwapCarpet()
    {
        // 1. 방에 있던(해바라기) 그림을 잠깐 '임시 변수'에 저장해둬요.
        Sprite tempSprite = roomCarpet.sprite;

        // 2. 방 바닥 그림을 -> 인벤토리 버튼에 있던(네모) 그림으로 바꿔요.
        roomCarpet.sprite = inventoryItemImage.sprite;

        // 3. 인벤토리 버튼 그림을 -> 아까 임시 저장해둔(해바라기) 그림으로 바꿔요.
        inventoryItemImage.sprite = tempSprite;


        Debug.Log("카페트 교환 완료!");
    }
}