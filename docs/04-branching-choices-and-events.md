# Modul 4: Branching Dialogue & Event System

Game yang menarik sering memberi kebebasan pada pemain untuk menentukan pilihan cerita. Di modul ini, kita akan menambahkan sistem pilihan percabangan yang dibuat secara dinamis di layar, serta mekanisme memicu event dalam game (seperti memulai quest, bertarung, atau memberikan item) langsung dari dialog.

---

## Membuat Tombol Pilihan Secara Dinamis (`ChoiceUIHandler.cs`)

Jumlah pilihan pada setiap node dialog bisa berbeda-beda (ada yang memiliki 2 pilihan, 3 pilihan, atau tanpa pilihan sama sekali). Oleh karena itu, tombol UI pilihan di-instantiate secara dinamis sesuai kebutuhan node yang sedang aktif.

```csharp
using System;
using System.Collections.Generic;
using NarrativeSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NarrativeSystem.UI
{
    public class ChoiceUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceButtonContainer;

        public void RenderChoices(List<DialogueChoice> choices, Action<DialogueChoice> onChoiceSelected)
        {
            ClearChoices();

            foreach (var choice in choices)
            {
                GameObject btnObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
                TextMeshProUGUI label = btnObj.GetComponentInChildren<TextMeshProUGUI>();
                if (label != null) label.text = choice.choiceText;

                Button btn = btnObj.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => {
                        onChoiceSelected?.Invoke(choice);
                    });
                }
            }
        }

        public void ClearChoices()
        {
            if (choiceButtonContainer == null) return;
            foreach (Transform child in choiceButtonContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
```

---

## Memicu Game Event dari Node Dialog

Selain memunculkan teks, node dialog juga bisa memicu `UnityEvent` saat percakapan dicapai. Hal ini berguna untuk memicu reaksi di dunia game seperti mengubah animasi karakter, memainkan cutscene, atau menambah quest.

```csharp
using UnityEngine.Events;

[System.Serializable]
public class DialogueNodeWithEvents : DialogueNodeSO
{
    public UnityEvent onNodeReached;
}
```

---

## Panduan Langkah Demi Langkah di Unity Editor

Mari kita pasang sistem tombol pilihan dan pemicu dialog di scene:

1. **Membuat Choice Button Prefab**:
   - Di **Hierarchy Window**, klik kanan -> **UI > Button - TextMeshPro** (Beri nama `ChoiceButtonPrefab`).
   - Atur tampilan tombol (ukuran, gambar tombol, dan ukuran teks).
   - Geser (drag) GameObject `ChoiceButtonPrefab` dari Hierarchy ke folder `Assets/NarrativeSystem/Prefabs/` di Project Window untuk menjadikannya Prefab.
   - Hapus GameObject `ChoiceButtonPrefab` yang ada di Hierarchy.

2. **Menyiapkan Choice Container**:
   - Klik kanan pada `DialoguePanel` -> **UI > Panel** (Beri nama `ChoiceContainer`). Tambahkan komponen `Vertical Layout Group` agar tombol tersusun rapi secara vertikal.
   - Pasang skrip `ChoiceUIHandler.cs` ke GameObject `ChoiceContainer`.
   - Geser file `ChoiceButtonPrefab` dari Project Window ke kolom slot **Choice Button Prefab**.
   - Pada komponen `DialogueManager`, hubungkan GameObject `ChoiceContainer` ke slot **Choice Handler**.

3. **Membuat Trigger Dialog di Scene (NPC)**:
   - Klik kanan di Hierarchy -> **Create Empty** atau **2D Object > Sprites > Square** (Beri nama `NPC_Merchant`).
   - Tambahkan komponen `BoxCollider2D` (atau `BoxCollider`), lalu centang opsi **Is Trigger**.
   - Pasang skrip `DialogueTrigger.cs` ke GameObject NPC tersebut.
   - Pada Inspector `DialogueTrigger`, masukkan aset `DialogueNodeSO` pembuka yang ingin diputar saat pemain mendekat.
