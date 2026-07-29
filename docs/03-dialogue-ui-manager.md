# Modul 3: Dialogue Manager & UI Text Typewriter

Setelah data architecture siap dari Modul 2, kita sekarang akan membangun **DialogueManager** untuk mengontrol alur eksekusi dialog dan merender teks menggunakan typewriter effect.

---

## Typewriter Effect (Karakter per Karakter)

Menggunakan C# Coroutine untuk menampilkan teks dialog karakter per karakter pada komponen TextMeshProUGUI.

```csharp
using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textContainer;
    [SerializeField] private float typingSpeed = 0.03f;

    private Coroutine _typingCoroutine;

    public void Run(string textToType, System.Action onComplete = null)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeText(textToType, onComplete));
    }

    private IEnumerator TypeText(string textToType, System.Action onComplete)
    {
        textContainer.text = "";
        foreach (char charToken in textToType)
        {
            textContainer.text += charToken;
            yield return new WaitForSeconds(typingSpeed);
        }
        onComplete?.Invoke();
    }
}
```

---

## Implementasi Dialogue Manager

```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Components")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private TypewriterEffect typewriter;

    private DialogueNodeSO _currentNode;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueNodeSO startingNode)
    {
        dialoguePanel.SetActive(true);
        DisplayNode(startingNode);
    }

    public void DisplayNode(DialogueNodeSO node)
    {
        _currentNode = node;
        speakerNameText.text = node.speaker != null ? node.speaker.characterName : "";
        if (portraitImage != null && node.speaker != null)
        {
            portraitImage.sprite = node.customPortrait != null ? node.customPortrait : node.speaker.defaultPortrait;
        }

        typewriter.Run(node.dialogueText);
    }
}
```

---

## Langkah Setup UI Canvas & Dialogue Manager di Unity Editor

Berikut adalah panduan langkah demi langkah menyusun UI Canvas di Unity Scene dari nol:

1. **Membuat UI Canvas & Dialogue Panel**:
   - Di tab **Hierarchy Window**, klik kanan -> **UI > Canvas** (Beri nama `DialogueCanvas`).
   - Ubah `Canvas Scaler` pada Inspector ke **Scale With Screen Size** (misal resolusi 1920x1080).
   - Klik kanan pada `DialogueCanvas` -> **UI > Panel** (Beri nama `DialoguePanel`). Ubah posisinya ke bagian bawah layar (Anchor: Bottom-Center).

2. **Menambahkan Komponen Teks & Gambar**:
   - Klik kanan pada `DialoguePanel` -> **UI > Text - TextMeshPro** (Beri nama `SpeakerNameText`). Posisikan di bagian kiri atas panel dialog.
   - Klik kanan pada `DialoguePanel` -> **UI > Image** (Beri nama `PortraitImage`). Posisikan di samping panel dialog untuk menampilkan foto ekspresi karakter.
   - Klik kanan pada `DialoguePanel` -> **UI > Text - TextMeshPro** (Beri nama `DialogueText`). Atur posisinya di tengah panel untuk menampilkan isi percakapan.

3. **Membuat GameObject Manager & Attach Components**:
   - Klik kanan di Hierarchy -> **Create Empty** (Beri nama `DialogueManager`).
   - Attach script `DialogueManager.cs` dan `TypewriterEffect.cs` ke GameObject `DialogueManager` tersebut.
   - Attach komponen `AudioSource` jika Anda ingin menggunakan efek suara pengetikan.

4. **Menghubungkan Reference pada Inspector**:
   - Pilih GameObject `DialogueManager` di Hierarchy.
   - Pada Inspector `Typewriter Effect`:
     - Drag & Drop `DialogueText` ke slot **Text Container**.
   - Pada Inspector `Dialogue Manager`:
     - Drag `DialoguePanel` ke slot **Dialogue Panel**.
     - Drag `SpeakerNameText` ke slot **Speaker Name Text**.
     - Drag `PortraitImage` ke slot **Portrait Image**.
     - Drag GameObject `DialogueManager` sendiri ke slot **Typewriter**.
