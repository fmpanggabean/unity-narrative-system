# Modul 5: Quest & Game State Integration

Dialog pada game RPG atau Adventure sering kali perlu membaca kondisi game state saat ini (contoh: *Apakah player membawa pedang emas?*, *Apakah boss sudah dikalahkan?*).

---

## Blackboard / Story State Manager

State manager sederhana berbentuk key-value yang mencatat boolean flags dan integer variables:

```csharp
using System.Collections.Generic;
using UnityEngine;

public class StoryStateManager : MonoBehaviour
{
    public static StoryStateManager Instance { get; private set; }

    private Dictionary<string, bool> _boolFlags = new Dictionary<string, bool>();
    private Dictionary<string, int> _intState = new Dictionary<string, int>();

    private void Awake() => Instance = this;

    public void SetFlag(string key, bool value) => _boolFlags[key] = value;
    public bool GetFlag(string key) => _boolFlags.TryGetValue(key, out bool val) && val;

    public void SetInt(string key, int value) => _intState[key] = value;
    public int GetInt(string key) => _intState.TryGetValue(key, out int val) ? val : 0;
}
```

---

## Evaluasi Kondisi pada Dialogue Node

Sebelum menampilkan node atau pilihan tertentu kepada player, kita lakukan evaluasi condition flag:

```csharp
public bool IsChoiceAvailable(DialogueChoice choice)
{
    if (string.IsNullOrEmpty(choice.requiredConditionFlag)) return true;
    return StoryStateManager.Instance.GetFlag(choice.requiredConditionFlag);
}
```

---

## Referensi File Script pada Unity Project
Seluruh implementasi koding pada modul ini dapat diakses langsung pada direktori proyek Unity:
- [StoryStateManager.cs](file:///c:/Workspaces/course/unity-narrative-system/unity-narrative-project/Assets/NarrativeSystem/Scripts/Core/StoryStateManager.cs)
```
