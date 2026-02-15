using UnityEngine;
using System.Linq; // List.FirstOrDefault() 사용을 위해 필요

public class CharacterObjectController : MonoBehaviour
{
    // 이 캐릭터 오브젝트에 해당하는 CharacterData의 ID. Unity 에디터에서 설정해줍니다.
    public string characterId;

    [Header("Visuals")]
    public GameObject eggVisual;       // 알 상태일 때 활성화할 비주얼 오브젝트
    public GameObject hatchedVisual;   // 부화 상태일 때 활성화할 비주얼 오브젝트 (실제 캐릭터 모델/스프라이트)

    // TODO: 부화했을 때 CharacterType에 따라 다른 비주얼을 보여주려면 여기에 추가적인 필드가 필요할 수 있습니다.
    // 예: public Sprite characterSpriteA;
    // 예: public GameObject characterPrefabB;

    private CharacterData myCharacterData; // 이 오브젝트와 연결된 CharacterData
    private SaveData _parentSaveData;       // 이 캐릭터가 속한 전체 SaveData 객체
    private string _slotName;               // 이 캐릭터가 속한 저장 슬롯 이름

    void Start()
    {
        // Start에서는 직접 데이터를 로드하지 않고, 외부에서 Initialize 메서드를 통해 데이터가 설정될 것을 기대합니다.
        // 예를 들어, GameManager나 LoadSceneController에서 저장 슬롯을 로드한 후 각 캐릭터 오브젝트에 데이터를 전달합니다.
        if (myCharacterData == null)
        {
            Debug.LogWarning($"CharacterObjectController: '{characterId}'에 데이터가 초기화되지 않았습니다. 외부에서 Initialize()를 호출해야 합니다.");
        }
    }

    // 외부에서 CharacterData, 이 데이터가 속한 전체 SaveData, 그리고 슬롯 이름을 받아 초기화하는 메서드
    public void Initialize(CharacterData data, SaveData parentSaveData, string slotName)
    {
        if (data == null)
        {
            Debug.LogError($"CharacterObjectController: '{characterId}'에 유효하지 않은 CharacterData가 전달되었습니다.");
            return;
        }
        myCharacterData = data;
        _parentSaveData = parentSaveData;
        _slotName = slotName;

        ApplyVisualState(); // 초기화된 데이터에 따라 비주얼 업데이트
    }

    // myCharacterData에 따라 비주얼을 업데이트합니다.
    private void ApplyVisualState()
    {
        if (myCharacterData.state == CharacterState.Egg)
        {
            eggVisual?.SetActive(true);
            hatchedVisual?.SetActive(false);
            // TODO: 알 상태일 때 필요한 추가적인 시각적 업데이트 (예: 애니메이션, 효과)
            Debug.Log($"{characterId} 현재 상태: 알");
        }
        else // CharacterState.Hatched
        {
            eggVisual?.SetActive(false);
            hatchedVisual?.SetActive(true);
            // TODO: 부화 상태일 때 필요한 추가적인 시각적 업데이트 (예: CharacterType에 따른 스프라이트/모델 변경)
            Debug.Log($"{characterId} 현재 상태: 부화, 유형: {myCharacterData.type}");
            // 예시: 캐릭터 유형에 따라 다른 스프라이트/모델 설정
            // if (myCharacterData.type == CharacterType.TypeA) { /* TypeA 비주얼 설정 */ }
            // else if (myCharacterData.type == CharacterType.TypeB) { /* TypeB 비주얼 설정 */ }
        }
    }

    // 알을 부화시키는 예시 메서드
    public void HatchEgg()
    {
        if (myCharacterData == null)
        {
            Debug.LogError($"CharacterObjectController: '{characterId}'에 CharacterData가 초기화되지 않아 부화할 수 없습니다.");
            return;
        }
        if (_parentSaveData == null || string.IsNullOrEmpty(_slotName))
        {
            Debug.LogError($"CharacterObjectController: '{characterId}'에 _parentSaveData 또는 _slotName이 없어 저장할 수 없습니다.");
            return;
        }

        if (myCharacterData.state == CharacterState.Egg)
        {
            myCharacterData.state = CharacterState.Hatched;
            
            // 5가지 캐릭터 유형 중 랜덤으로 하나 선택
            CharacterType[] types = { CharacterType.TypeA, CharacterType.TypeB, CharacterType.TypeC, CharacterType.TypeD, CharacterType.TypeE };
            myCharacterData.type = types[Random.Range(0, types.Length)];

            ApplyVisualState(); // 비주얼 업데이트
            // 변경된 myCharacterData를 포함하는 _parentSaveData를 지정된 슬롯 이름으로 저장
            SaveManager.Instance.SaveGame(_slotName, _parentSaveData); 
            Debug.Log($"{characterId} 알이 부화했습니다! 유형: {myCharacterData.type}");
        }
        else
        {
            Debug.LogWarning($"{characterId}는 이미 부화한 상태입니다.");
        }
    }

    // 인벤토리나 먹이 창고 데이터는 이 스크립트가 아닌, 별도의 InventoryManager나 FeedManager에서 관리하는 것이 좋습니다.
    // 하지만 SaveManager를 통해 접근하는 방식은 동일합니다.
    // SaveManager.Instance.GetCurrentSaveData().inventoryItems
    // SaveManager.Instance.GetCurrentSaveData().feedStorageAmount
}
