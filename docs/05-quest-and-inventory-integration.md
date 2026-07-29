# Modul 5: Quest & Game State Integration

Dalam game RPG atau Adventure, respon dialog NPC sering kali bergantung pada status permainan saat ini—misalnya: *Apakah pemain sudah menemukan kunci emas?*, *Apakah boss sudah dikalahkan?*, atau *Apakah quest pertama sudah selesai?*. 

Di modul ini, kita membuat **StoryStateManager** berbasis pola Blackboard sederhana untuk menyimpan status variabel dunia game dan mengevaluasi pilihan dialog berdasarkan variabel tersebut.

---

## Story State Manager (`StoryStateManager.cs`)

Manajer ini bertindak sebagai tempat penyimpanan terpusat variabel alur cerita (berupa pasangan key-value untuk data bertipe `bool` dan `int`).

```csharp
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeSystem.Core
{
    public class StoryStateManager : MonoBehaviour
    {
        public static StoryStateManager Instance { get; private set; }

        private Dictionary<string, bool> _boolFlags = new Dictionary<string, bool>();
        private Dictionary<string, int> _intState = new Dictionary<string, int>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetFlag(string key, bool value)
        {
            if (string.IsNullOrEmpty(key)) return;
            _boolFlags[key] = value;
        }

        public bool GetFlag(string key)
        {
            if (string.IsNullOrEmpty(key)) return true;
            return _boolFlags.TryGetValue(key, out bool val) && val;
        }
    }
}
```

---

## Mengevaluasi Syarat Pilihan Dialog

Sebelum menampilkan opsi pilihan kepada pemain, `DialogueManager` memanggil fungsi filter untuk memeriksa apakah syarat `requiredConditionFlag` terpenuhi di `StoryStateManager`.

```csharp
private List<DialogueChoice> FilterAvailableChoices(List<DialogueChoice> rawChoices)
{
    List<DialogueChoice> available = new List<DialogueChoice>();
    foreach (var choice in rawChoices)
    {
        if (string.IsNullOrEmpty(choice.requiredConditionFlag) ||
            (StoryStateManager.Instance != null && StoryStateManager.Instance.GetFlag(choice.requiredConditionFlag)))
        {
            available.Add(choice);
        }
    }
    return available;
}
```

---

## Panduan Langkah Demi Langkah di Unity Editor

Berikut cara mengkonfigurasi variabel cerita dan syarat pilihan dialog:

1. **Menyiapkan State Manager di Scene**:
   - Klik kanan di **Hierarchy Window** -> **Create Empty** (Beri nama `StoryStateManager`).
   - Pasang (attach) skrip `StoryStateManager.cs` ke GameObject tersebut.

2. **Membuat Pilihan Bersyarat pada Aset Dialog**:
   - Buka file aset `DialogueNodeSO` di Project Window yang memiliki pilihan khusus.
   - Pada opsi pilihan tertentu di Inspector, isi kolom **Required Condition Flag** dengan kata kunci unik, misalnya `has_gold_key`.
   - Opsi tombol ini secara otomatis hanya muncul di layar jika flag `has_gold_key` bernilai `true`.

3. **Mencatat Status Otomatis**:
   - Pada file `DialogueNodeSO`, isi kolom **Set Story Flag On Reach** (misalnya `talked_to_guard_once`).
   - Begitu percakapan pada node tersebut muncul di layar, `DialogueManager` akan otomatis memperbarui variabel `talked_to_guard_once` menjadi `true`.
