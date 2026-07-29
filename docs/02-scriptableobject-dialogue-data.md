# Modul 2: ScriptableObject Data Architecture

Pada modul ini, kita akan merancang dialogue data structure yang bersih dan fleksibel tanpa melakukan hardcoding dialogue lines di dalam MonoBehaviour script.

---

## Core Data Structure

### 1. Character Profile Data (`CharacterProfileSO.cs`)
Digunakan untuk mengelompokkan identitas NPC maupun playable character.

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Narrative/Character Profile")]
public class CharacterProfileSO : ScriptableObject
{
    public string characterName;
    public Sprite defaultPortrait;
    public Color nameColor = Color.white;
    public AudioClip voiceSound;
}
```

---

### 2. Dialogue Choice Option (`DialogueChoice.cs`)
Menyimpan teks pilihan beserta reference ke target node jika pilihan tersebut dipilih.

```csharp
using System;

[Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueNodeSO targetNode;
    public string requiredConditionFlag; // Optional condition flag
}
```

---

### 3. Dialogue Node (`DialogueNodeSO.cs`)
Berfungsi sebagai atomic building block dari konten naratif.

```csharp
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Narrative/Dialogue Node")]
public class DialogueNodeSO : ScriptableObject
{
    public CharacterProfileSO speaker;
    public Sprite customPortrait;
    [TextArea(3, 5)] public string dialogueText;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public DialogueNodeSO defaultNextNode; // Digunakan jika tidak ada pilihan
}
```

---

## Keuntungan Menggunakan ScriptableObject
- **Reusability**: Character profile dan dialogue node dapat digunakan kembali di berbagai scene.
- **Designer Friendly**: Narrative designer dapat mengedit dialog melalui Unity Inspector tanpa perlu mengubah kode C#.
