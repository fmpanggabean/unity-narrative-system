# Panduan Lengkap Penggunaan Scripts Narrative System

Dokumen ini menyediakan panduan terperinci penggunaan seluruh C# script yang terdapat pada proyek **Narrative System** di folder `Assets/NarrativeSystem/Scripts/` dan `Assets/NarrativeSystem/Editor/`.

---

## 📁 Structure Directory C# Scripts

```text
unity-narrative-project/Assets/NarrativeSystem/
├── Scripts/
│   ├── Data/
│   │   ├── CharacterProfileSO.cs
│   │   ├── DialogueChoice.cs
│   │   └── DialogueNodeSO.cs
│   ├── Core/
│   │   ├── StoryStateManager.cs
│   │   ├── TypewriterEffect.cs
│   │   └── DialogueManager.cs
│   └── UI/
│       ├── ChoiceUIHandler.cs
│       └── DialogueTrigger.cs
└── Editor/
    ├── DialogueGraphView.cs
    ├── DialogueNodeView.cs
    └── DialogueGraphWindow.cs
```

---

## Petunjuk Penggunaan Per Komponen

### 1. Data Layer (`Scripts/Data/`)

#### A. `CharacterProfileSO.cs`
- **Fungsi**: Membuat profil karakter dialog yang dapat didaur ulang.
- **Cara Membuat Asset**: Klik kanan di Project Window -> `Create > Narrative System > Character Profile`.
- **Property**:
  - `characterName`: Nama karakter yang tampil di UI header.
  - `nameColor`: Warna teks nama karakter.
  - `defaultPortrait`: Gambar ekspresi bawaan karakter.
  - `typingSound`: (Opsional) Audio clip blip suara pengetikan.

#### B. `DialogueNodeSO.cs`
- **Fungsi**: Atomic unit dari percakapan dialog.
- **Cara Membuat Asset**: Klik kanan di Project Window -> `Create > Narrative System > Dialogue Node`.
- **Property**:
  - `speaker`: Assign `CharacterProfileSO`.
  - `dialogueText`: Teks dialog percakapan.
  - `choices`: List opsi pilihan bercabang.
  - `defaultNextNode`: Node rujukan jika dialog berbentuk linier (tanpa pilihan).
  - `setStoryFlagOnReach`: Nama flag boolean yang akan bernilai `true` secara otomatis di `StoryStateManager` saat node dimasuki.

---

### 2. Core Logic (`Scripts/Core/`)

#### A. `DialogueManager.cs`
- **Fungsi**: Singleton pengelola UI dialog & alur alokasi node.
- **Setup di Scene**:
  1. Buat GameObject kosong bernama `DialogueManager`.
  2. Attach script `DialogueManager.cs` dan `TypewriterEffect.cs`.
  3. Hubungkan komponen UI Canvas (Panel, TextMeshProUGUI nama, Image portrait, dll.) ke slot Inspector `DialogueManager`.

#### B. `StoryStateManager.cs`
- **Fungsi**: Penyimpanan global variabel alur cerita (Blackboard pattern).
- **Setup**: Attach pada GameObject di scene utama. Memiliki fitur `DontDestroyOnLoad`.

---

### 3. UI & Trigger (`Scripts/UI/`)

#### A. `ChoiceUIHandler.cs`
- **Fungsi**: Instansiasi otomatis tombol-tombol pilihan bercabang.
- **Setup**: Attach pada GameObject `ChoiceContainer` di bawah Canvas UI dan beri referensi `ChoiceButtonPrefab`.

#### B. `DialogueTrigger.cs`
- **Fungsi**: Memicu dimulainya dialog saat player memasuki Collider Trigger.
- **Setup**:
  1. Attach pada NPC / GameObject dengan `BoxCollider2D` / `BoxCollider` (centang `Is Trigger`).
  2. Masukkan `startingDialogueNode` yang akan diputar pertama kali.

---

### 4. Custom GraphView Editor (`Editor/`)

- **Cara Mengakses Tool Editor Window**:
  Di Top Menu Bar Unity, buka: **`Tools > Narrative System > Dialogue Graph Editor`**.
