# Unity Narrative Project Setup

Folder ini diperuntukkan bagi proyek Unity Engine **Narrative System**.

## Struktur Folder Rekomendasi

Saat menambahkan atau membuat proyek Unity di folder ini, pastikan struktur folder `Assets/` diatur sebagai berikut:

```text
unity-narrative-project/
├── Assets/
│   ├── NarrativeSystem/
│   │   ├── Scripts/          # ScriptableObjects, Managers, UI Controller
│   │   ├── Editor/           # Custom GraphView & Node Editor
│   │   ├── Prefabs/          # UI Canvas & Dialogue Box Prefabs
│   │   └── Data/             # Sample Dialogue Assets (.asset)
│   └── DemoScene.unity
├── Packages/
│   └── manifest.json
└── ProjectSettings/
    └── ProjectVersion.txt
```

## Catatan Git & File Ignore

File `.gitignore` di akar repositori telah dikonfigurasi untuk mengabaikan folder cache dan temporary Unity seperti:
- `Library/`
- `Temp/`
- `Obj/`
- `Build/` / `Builds/`
- `Logs/`

---

## Asset Credits

Aset UI yang digunakan dalam proyek ini:
- **Kenney UI Pack (Pixel Adventure)**  
  URL: [https://kenney.nl/assets/ui-pack-pixel-adventure](https://kenney.nl/assets/ui-pack-pixel-adventure)  
  Lisensi: CC0 1.0 Universal (Public Domain). Karya oleh Kenney.
- `UserSettings/`
- File Project Visual Studio / Rider (`*.csproj`, `*.sln`)

Harap pastikan file `.meta` tetap dicommit bersamaan dengan aset terkait untuk menjaga GUID referensi di Unity.
