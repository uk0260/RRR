using System;
using System.Collections.Generic;



[System.Serializable]
public class SaveData
{
    // 5개의 캐릭터 데이터를 담을 리스트
    public List<CharacterData> characters = new List<CharacterData>();

    // 저장 시간 (yyyy-MM-dd hh:mm:ss 형식)
    public string saveTimestamp;

    // 인벤토리 아이템 목록 (예: 아이템 ID와 수량)
    public List<string> inventoryItems = new List<string>();

    // 먹이 창고의 현재 먹이 수량
    public int feedStorageAmount;

    // 기타 저장하고 싶은 데이터를 추가하세요.
    // 예: 현재 씬 이름, 플레이어 이름 등
    // public string currentSceneName;
    // public string playerName;
    public int currency;
    
}

