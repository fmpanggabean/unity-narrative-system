# Modul 6: Custom Visual Node Editor (GraphView)

Modul tingkat lanjut ini membahas pembuatan visual tool (**Custom Node Editor**) di dalam Unity Editor menggunakan `UnityEditor.Experimental.GraphView` API.

---

## Mengapa Membangun Custom Node Editor?

Mengelola branching dialogue yang kompleks hanya melalui ScriptableObject Inspector bawaan bisa menjadi sangat rumit seiring berkembangnya dialogue tree. Dengan **GraphView Editor**:
- Alur dialog ditampilkan secara visual sebagai flowchart.
- Menghubungkan antar node dilakukan dengan mudah melalui drag-and-drop edge antat port.

---

## Struktur Kode Basic Editor Window

```csharp
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphWindow : EditorWindow
{
    [MenuItem("Tools/Narrative/Dialogue Graph Editor")]
    public static void OpenWindow()
    {
        var window = GetWindow<DialogueGraphWindow>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
    }

    private void ConstructGraphView()
    {
        // Inisialisasi GraphView Element
        DialogueGraphView graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }
}
#endif
```

---

## Kesimpulan Course

Selamat! Anda telah menyelesaikan modul dasar hingga tingkat lanjut untuk membangun **Narrative System di Unity Engine**. Dengan arsitektur yang modular ini, project game Anda siap untuk mendukung cerita yang kaya dan bercabang.

---

## Referensi File Script pada Unity Project
Seluruh implementasi koding custom editor pada modul ini dapat diakses langsung pada direktori proyek Unity:
- [DialogueGraphView.cs](file:///c:/Workspaces/course/unity-narrative-system/unity-narrative-project/Assets/NarrativeSystem/Editor/DialogueGraphView.cs)
- [DialogueNodeView.cs](file:///c:/Workspaces/course/unity-narrative-system/unity-narrative-project/Assets/NarrativeSystem/Editor/DialogueNodeView.cs)
- [DialogueGraphWindow.cs](file:///c:/Workspaces/course/unity-narrative-system/unity-narrative-project/Assets/NarrativeSystem/Editor/DialogueGraphWindow.cs)
