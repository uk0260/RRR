using System;
using System.Collections.Generic; // List<T> 사용을 위해 필요할 수 있음

// 캐릭터 유형 (5가지 중 하나)
public enum CharacterType
{
    None, // 알 상태 또는 비어있는 슬롯을 의미
    TypeA,
    TypeB,
    TypeC,
    TypeD,
    TypeE
}

// 캐릭터의 현재 상태 (알 또는 부화)
public enum CharacterState
{
    Egg,    // 알 상태
    Hatched // 부화한 캐릭터 상태
}

[System.Serializable]
public class CharacterData
{
    public string characterId;      // 각 캐릭터 슬롯의 고유 식별자 (예: "CharSlot1")
    public CharacterState state;    // 현재 상태 (알 또는 부화)
    public CharacterType type;      // 부화했을 경우의 캐릭터 유형

    // 기본 생성자 (JSON 직렬화를 위해 필요)
    public CharacterData() { }

    // 초기 알 상태로 캐릭터 데이터를 생성하는 생성자
    public CharacterData(string id)
    {
        characterId = id;
        state = CharacterState.Egg;
        type = CharacterType.None; // 부화 전이므로 유형 없음
    }
}
