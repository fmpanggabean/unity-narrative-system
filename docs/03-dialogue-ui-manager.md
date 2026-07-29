# Modul 3: Dialogue Manager & UI Text Typewriter

Setelah struktur data ScriptableObject siap, langkah selanjutnya adalah menampilkan percakapan tersebut ke layar. Di modul ini, kita akan membuat dua komponen utama: **TypewriterEffect** untuk efek animasi teks mengetik huruf demi huruf, dan **DialogueManager** sebagai pengontrol utama antarmuka dialog.

---

## Membuat Typewriter Effect (Efek Mengetik)

Agar percakapan terasa hidup seperti pada game RPG klasik, teks tidak langsung dimunculkan seketika melainkan diketik per karakter menggunakan C# Coroutine.

```csharp
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textContainer;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private AudioSource audioSource;

    private Coroutine _typingCoroutine;
    public bool IsTyping { get; private set; }

    public void Run(string textToType, AudioClip typingSound = null, Action onComplete = null)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeText(textToType, typingSound, onComplete));
    }

    private IEnumerator TypeText(string textToType, AudioClip typingSound, Action onComplete)
    {
        IsTyping = true;
        textContainer.text = "";

        foreach (char charToken in textToType)
        {
            textContainer.text += charToken;
            if (typingSound != null && audioSource != null && charToken != ' ')
            {
                audioSource.PlayOneShot(typingSound);
            }
            yield return new WaitForSeconds(typingSpeed);
        }

        IsTyping = false;
        onComplete?.Invoke();
    }

    public void QuickComplete(string fullText)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        textContainer.text = fullText;
        IsTyping = false;
    }
}
```

---

## Implementasi Dialogue Manager (Singleton Engine)

`DialogueManager` bertugas menerima node dialog yang sedang aktif, mengekstrak informasi pembicara (nama & profil), lalu memerintahkan `TypewriterEffect` untuk menampilkan teksnya.

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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
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

        if (speakerNameText != null)
        {
            speakerNameText.text = node.speaker != null ? node.speaker.characterName : "";
            if (node.speaker != null) speakerNameText.color = node.speaker.nameColor;
        }

        if (portraitImage != null)
        {
            Sprite targetPortrait = node.customPortrait != null ? node.customPortrait : (node.speaker != null ? node.speaker.defaultPortrait : null);
            portraitImage.gameObject.SetActive(targetPortrait != null);
            portraitImage.sprite = targetPortrait;
        }

        AudioClip sound = node.speaker != null ? node.speaker.typingSound : null;
        typewriter.Run(node.dialogueText, sound);
    }
}
```

---

## Langkah Menyusun UI Canvas di Unity Editor

Mari kita rangkai komponen UI di scene Unity agar skrip di atas bekerja sempurna:

1. **Menyiapkan Canvas & Dialogue Panel**:
   - Di **Hierarchy Window**, klik kanan -> **UI > Canvas** (Beri nama `DialogueCanvas`).
   - Ubah `Canvas Scaler` pada Inspector menjadi **Scale With Screen Size** (atur resolusi ke 1920x1080).
   - Klik kanan pada `DialogueCanvas` -> **UI > Panel** (Beri nama `DialoguePanel`). Posisikan kotak panel ini di bagian bawah layar.

2. **Menambahkan Komponen Teks & Foto Profil**:
   - Klik kanan pada `DialoguePanel` -> **UI > Text - TextMeshPro** (Beri nama `SpeakerNameText`). Atur di pojok atas panel dialog.
   - Klik kanan pada `DialoguePanel` -> **UI > Image** (Beri nama `PortraitImage`). Posisikan di samping panel untuk gambar karakter.
   - Klik kanan pada `DialoguePanel` -> **UI > Text - TextMeshPro** (Beri nama `DialogueText`). Atur posisinya di tengah panel tempat pesan dialog diketik.

3. **Menghubungkan Skrip ke Manager GameObject**:
   - Klik kanan di Hierarchy -> **Create Empty** (Beri nama `DialogueManager`).
   - Pasang (attach) komponen `DialogueManager.cs` dan `TypewriterEffect.cs` ke GameObject tersebut.
   - Pilih GameObject `DialogueManager`, lalu geser (drag & drop) komponen `DialoguePanel`, `SpeakerNameText`, `PortraitImage`, dan `DialogueText` dari Hierarchy ke kolom slot Inspector yang sesuai.
