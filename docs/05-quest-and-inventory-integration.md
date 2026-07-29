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

## Langkah Setup Story State Manager & Condition Checking di Unity Editor

1. **Membuat Manager GameObject di Scene**:
   - Di Hierarchy, klik kanan -> **Create Empty** (Beri nama `StoryStateManager`).
   - Attach script `StoryStateManager.cs` ke GameObject tersebut. Komponen ini bekerja sebagai DontDestroyOnLoad Singleton.

2. **Mengatur Condition Flag pada Dialogue Choice Asset**:
   - Buka file asset `DialogueNodeSO` yang memiliki pilihan khusus (misal: opsi berdagang yang hanya aktif jika player punya kunci/gold).
   - Pada pilihan tersebut di Inspector, isi string kolom **Required Condition Flag**, misalnya `has_village_key`.
   - Tombol pilihan tersebut hanya akan muncul di layar jika flag `has_village_key` bernilai `true` pada `StoryStateManager`.

3. **Mengubah Flag Otomatis saat Percakapan Selesai/Dimulai**:
   - Pada Inspector `DialogueNodeSO`, isi kolom **Set Story Flag On Reach** (misalnya `met_elder_once`).
   - Saat node percakapan tersebut ditampilkan kepada player, `DialogueManager` akan secara otomatis memanggil `StoryStateManager.Instance.SetFlag("met_elder_once", true)`.
