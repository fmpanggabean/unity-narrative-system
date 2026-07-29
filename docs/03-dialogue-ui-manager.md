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
