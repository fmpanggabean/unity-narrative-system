# Modul 6: Custom Visual Node Editor (GraphView)

Ketika alur cerita game berkembang semakin luas, mengelola puluhan node dialog bercabang lewat Inspector standar bisa terasa melelahkan. Di modul tingkat lanjut ini, kita membuat **Custom Node Editor** berbasis visual menggunakan `UnityEditor.Experimental.GraphView` API.

---

## Mengapa Perlu Custom Visual Tool?

Dengan jendela editor visual:
- Alur dialog bercabang ditampilkan secara jelas sebagai diagram alir (flowchart).
- Menghubungkan satu kalimat dengan opsi pilihan cukup dilakukan lewat perintah drag-and-drop antar garis penghubung (edge/port).
- Penulis cerita dapat melihat gambaran besar percakapan tanpa risiko salah menghubungkan file aset.

---

## Struktur Utama Editor Window (`DialogueGraphWindow.cs`)

Skrip berikut bertugas membuat jendela editor khusus yang dapat diakses langsung dari menu atas Unity Editor.

```csharp
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NarrativeSystem.Editor
{
    public class DialogueGraphWindow : EditorWindow
    {
        private DialogueGraphView _graphView;

        [MenuItem("Tools/Narrative System/Dialogue Graph Editor")]
        public static void OpenWindow()
        {
            DialogueGraphWindow window = GetWindow<DialogueGraphWindow>();
            window.titleContent = new GUIContent("Dialogue Graph Editor");
        }

        private void OnEnable()
        {
            ConstructGraphView();
        }

        private void OnDisable()
        {
            if (_graphView != null)
            {
                rootVisualElement.Remove(_graphView);
            }
        }

        private void ConstructGraphView()
        {
            _graphView = new DialogueGraphView
            {
                name = "Dialogue Graph View"
            };
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }
    }
}
#endif
```

---

## Panduan Menggunakan Tool Editor di Unity

1. **Lokasi File Skrip**:
   - Pastikan skrip `DialogueGraphWindow.cs`, `DialogueGraphView.cs`, dan `DialogueNodeView.cs` disimpan di dalam folder `Assets/NarrativeSystem/Editor/`. Folder khusus ini memastikan kode editor tidak ikut terkompilasi ke dalam build akhir game.

2. **Membuka Jendela Editor Visual**:
   - Pada bilah menu paling atas di Unity Editor, klik **Tools > Narrative System > Dialogue Graph Editor**.
   - Jendela editor visual akan muncul. Anda bisa menggeser dan menempelkan (docking) jendela ini di samping tab Scene atau Inspector.

3. **Navigasi Canvas Visual**:
   - **Zoom**: Gunakan roda scroll mouse untuk membesar-kecilkan tampilan canvas.
   - **Geser Canvas (Pan)**: Tahan tombol tengah mouse (atau Alt + Klik Kiri) untuk menggeser area canvas.
   - **Pilih Banyak Node**: Tahan Klik Kiri dan drag untuk membuat kotak seleksi.

---

## Kesimpulan

Selamat! Anda telah menyelesaikan seluruh rangkaian materi pembuatan **Narrative System di Unity Engine**—mulai dari perancangan arsitektur data ScriptableObject, pembuatan engine pengetikan teks, penanganan cabang pilihan, integrasi status game, hingga pembuatan editor visual GraphView.

Dengan arsitektur yang modular dan ramah pengembang ini, proyek game Anda kini siap mendukung cerita interaktif yang kaya dan mendalam!
