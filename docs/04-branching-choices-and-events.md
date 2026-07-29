# Modul 4: Branching Dialogue & Event System

Pada modul ini, kita akan menambahkan dukungan narrative branching melalui pilihan tombol (Dialogue Choices UI) dan memicu external game events.

---

## Dynamic Choice Button Instantiation

Ketika sebuah dialogue node memiliki opsi pilihan (`choices.Count > 0`), choice buttons akan di-instantiate secara dinamis.

```csharp
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private Transform choiceButtonContainer;

    public void RenderChoices(List<DialogueChoice> choices, System.Action<DialogueChoice> onChoiceSelected)
    {
        // Hapus button sebelumnya
        foreach (Transform child in choiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Spawn choice button baru
        foreach (var choice in choices)
        {
            GameObject btnObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;
            
            btnObj.GetComponent<Button>().onClick.AddListener(() => {
                onChoiceSelected?.Invoke(choice);
            });
        }
    }
}
```

---

## Memicu Game Events dari Dialogue Node

Kita dapat memicu `UnityEvent` atau `C# Action` ketika mencapai dialogue node tertentu (misalnya: memicu cutscene, memulai combat, atau mengubah status reputasi).

```csharp
using UnityEngine.Events;

[System.Serializable]
public class DialogueNodeWithEvents : DialogueNodeSO
{
    public UnityEvent onNodeReached;
}
```

---

## Langkah Setup Choice UI & Dialogue Trigger di Unity Editor

1. **Membuat Choice Button Prefab**:
   - Di Hierarchy, klik kanan -> **UI > Button - TextMeshPro** (Beri nama `ChoiceButtonPrefab`).
   - Sesuaikan ukuran tombol, background sprite (misal menggunakan sprite dari Kenney UI Pack), dan font size pada anak komponen TextMeshPro.
   - Drag GameObject `ChoiceButtonPrefab` tersebut dari Hierarchy ke Project Window (folder `Assets/NarrativeSystem/Prefabs/`) untuk menjadikannya Asset Prefab. Hapus GameObject tersebut dari Hierarchy setelah prefab terbentuk.

2. **Membuat Choice Container & Attach Component**:
   - Klik kanan pada `DialoguePanel` di Hierarchy -> **UI > Panel / Vertical Layout Group** (Beri nama `ChoiceContainer`).
   - Attach script `ChoiceUIHandler.cs` ke GameObject `ChoiceContainer`.
   - Pada Inspector `ChoiceUIHandler`, masukkan `ChoiceButtonPrefab` dari Project Window ke slot **Choice Button Prefab**, dan masukkan `ChoiceContainer` sendiri ke slot **Choice Button Container**.
   - Hubungkan GameObject `ChoiceContainer` ke slot **Choice Handler** pada `DialogueManager`.

3. **Membuat NPC Dialogue Trigger di Scene**:
   - Klik kanan di Hierarchy -> **Create Empty** / **2D Object > Sprites > Square** (Beri nama `NPC_Merchant`).
   - Tambahkan komponen `BoxCollider2D` atau `BoxCollider`. Centang opsi **Is Trigger**.
   - Attach script `DialogueTrigger.cs` ke GameObject NPC.
   - Pada Inspector `DialogueTrigger`, tarik file `DialogueNodeSO` awal yang ingin diputar pertama kali ke slot **Starting Dialogue Node**.
