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

## Panduan Menggunakan Custom Editor Window di Unity Editor

1. **Memastikan Script Berada di Folder Editor**:
   - Pastikan file `DialogueGraphWindow.cs`, `DialogueGraphView.cs`, dan `DialogueNodeView.cs` berada di bawah direktori `Assets/NarrativeSystem/Editor/` agar terkompilasi khusus untuk lingkungan Unity Editor.

2. **Membuka Window Editor**:
   - Di Unity Bar Top Menu paling atas, klik menu **Tools > Narrative System > Dialogue Graph Editor**.
   - Window editor visual baru akan terbuka (bisa di-docking di samping Scene view atau Inspector).

3. **Navigasi Visual Graph Canvas**:
   - **Zoom**: Gunakan Scroll Wheel mouse untuk zoom in / zoom out pada grid canvas.
   - **Pan Canvas**: Tahan tombol tengah Mouse (Scroll Click) / Alt + Klik Kiri untuk menggeser (panning) area canvas editor.
   - **Multi-select**: Tahan Klik Kiri dan drag untuk membuat kotak seleksi area (Rectangle Selection).

---

## Kesimpulan Course

Selamat! Anda telah menyelesaikan modul dasar hingga tingkat lanjut untuk membangun **Narrative System di Unity Engine**. Dengan arsitektur yang modular ini, project game Anda siap untuk mendukung cerita yang kaya dan bercabang.
