using UnityEngine;
using System.IO;
using System.Collections.Generic; // List<T>를 위해 추가
using System.Linq; // LINQ 확장 메서드를 위해 추가 (예: FirstOrDefault)

// 각 저장 슬롯의 메타데이터를 담는 클래스
[System.Serializable]
public class SaveSlotInfo
{
    public string fileName;      // 저장 파일의 실제 이름 (예: "save_001.json")
    public string displayName;   // UI에 표시될 이름 (예: "슬롯 1")
    public string saveTimestamp; // 저장 시간 (yyyy-MM-dd hh:mm:ss)

    public SaveSlotInfo(string file, string display, string timestamp)
    {
        fileName = file;
        displayName = display;
        saveTimestamp = timestamp;
    }
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴되지 않도록
        }
    }

    private SaveData _currentSaveData;
    private string _currentSlotName;

    // 현재 로드된 게임 데이터를 가져옵니다.
    public SaveData CurrentSaveData
    {
        get { return _currentSaveData; }
    }

    // 현재 활성화된 슬롯 이름을 가져옵니다.
    public string CurrentSlotName
    {
        get { return _currentSlotName; }
    }

    // 게임을 로드하고 현재 슬롯으로 설정합니다.
    public SaveData LoadGameAndSetCurrent(string slotName)
    {
        SaveData loadedData = LoadGame(slotName);
        if (loadedData != null)
        {
            _currentSaveData = loadedData;
            _currentSlotName = slotName;
        } else {
            // 새 게임 시작 등의 경우를 대비해 currentSaveData를 null로 초기화
            _currentSaveData = null;
            _currentSlotName = null;
        }
        return loadedData;
    }

    // 현재 로드된 데이터를 저장합니다.
    public void SaveCurrentGame()
    {
        if (_currentSaveData != null && !string.IsNullOrEmpty(_currentSlotName))
        {
            SaveGame(_currentSlotName, _currentSaveData);
        }
        else
        {
            Debug.LogWarning("SaveManager: No current game data or slot name to save.");
        }
    }

    // 재화를 추가하는 함수
    public void AddCurrency(int amount)
    {
        if (_currentSaveData != null)
        {
            _currentSaveData.currency += amount;
            Debug.Log($"Added {amount} currency. Current currency: {_currentSaveData.currency}");
            SaveCurrentGame(); // 변경사항 저장
        }
        else
        {
            Debug.LogWarning("SaveManager: No current game data to add currency to.");
        }
    }

    // 재화를 차감하는 함수
    public bool SubtractCurrency(int amount)
    {
        if (_currentSaveData != null)
        {
            if (_currentSaveData.currency >= amount)
            {
                _currentSaveData.currency -= amount;
                Debug.Log($"Subtracted {amount} currency. Current currency: {_currentSaveData.currency}");
                SaveCurrentGame(); // 변경사항 저장
                return true;
            }
            else
            {
                Debug.LogWarning($"SaveManager: Not enough currency to subtract {amount}. Current: {_currentSaveData.currency}");
                return false; // 재화가 부족하여 차감 실패
            }
        }
        else
        {
            Debug.LogWarning("SaveManager: No current game data to subtract currency from.");
            return false;
        }
    }

    // 현재 게임 데이터를 SaveData 객체로 저장 (슬롯 이름 지정)
    public void SaveGame(string slotName, SaveData dataToSave)
    {
        if (string.IsNullOrEmpty(slotName))
        {
            Debug.LogError("SaveManager: slotName이 비어있습니다. 저장할 수 없습니다.");
            return;
        }
        if (dataToSave == null)
        {
            Debug.LogWarning($"SaveManager: dataToSave({slotName})가 null입니다. 저장할 수 없습니다.");
            return;
        }

        string filePath = GetSaveFilePath(slotName);
        dataToSave.saveTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 현재 시간 기록

        string json = JsonUtility.ToJson(dataToSave, true); // true는 가독성을 위해 포맷팅
        File.WriteAllText(filePath, json);
        Debug.Log($"Game Saved to slot '{slotName}' at: {filePath}");
    }

    // 지정된 슬롯 이름의 데이터를 파일에서 불러오기
    public SaveData LoadGame(string slotName)
    {
        if (string.IsNullOrEmpty(slotName))
        {
            Debug.LogError("SaveManager: slotName이 비어있습니다. 불러올 수 없습니다.");
            return null;
        }

        string filePath = GetSaveFilePath(slotName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"Game Loaded from slot '{slotName}' at: {filePath}");
            return loadedData;
        }
        else
        {
            Debug.LogWarning($"SaveManager: 슬롯 '{slotName}'에 대한 저장 파일을 찾을 수 없습니다. 새로운 데이터를 반환합니다.");
            return null; // 파일이 없으면 null 반환 (호출하는 쪽에서 새 게임 데이터 생성 처리)
        }
    }

    // 모든 저장 슬롯 정보를 가져오기
    public List<SaveSlotInfo> GetAllSaveSlotInfos()
    {
        List<SaveSlotInfo> saveSlots = new List<SaveSlotInfo>();
        string persistentPath = Application.persistentDataPath;

        // "save_"로 시작하고 ".json"으로 끝나는 모든 파일 찾기
        string[] files = Directory.GetFiles(persistentPath, "save_*.json");

        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file); // 예: "save_Slot1.json"
            string slotIdentifier = fileName.Replace("save_", "").Replace(".json", ""); // 예: "Slot1"

            try
            {
                string json = File.ReadAllText(file);
                // SaveData를 완전히 로드하여 saveTimestamp만 가져옴
                SaveData tempSaveData = JsonUtility.FromJson<SaveData>(json);
                if (tempSaveData != null)
                {
                    // displayName은 파일 이름과 타임스탬프를 조합하여 생성
                    string displayName = $"슬롯 {slotIdentifier} ({tempSaveData.saveTimestamp})";
                    saveSlots.Add(new SaveSlotInfo(fileName, displayName, tempSaveData.saveTimestamp));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"저장 파일 '{fileName}' 로드 중 오류 발생: {e.Message}");
            }
        }

        // 최신 저장 파일부터 정렬 (타임스탬프 기준으로)
        return saveSlots.OrderByDescending(s => s.saveTimestamp).ToList();
    }

    // 저장 파일 경로 생성 헬퍼 함수
    private string GetSaveFilePath(string slotName)
    {
        // 파일 이름: "save_슬롯이름.json" 형식
        return Path.Combine(Application.persistentDataPath, $"save_{slotName}.json");
    }

    // 지정된 슬롯의 저장 파일 삭제
    public void DeleteSaveSlot(string slotName)
    {
        string filePath = GetSaveFilePath(slotName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Save file for slot '{slotName}' deleted: {filePath}");
        }
        else
        {
            Debug.LogWarning($"Save file for slot '{slotName}' not found to delete.");
        }
    }
}
