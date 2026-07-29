# Modul 2: ScriptableObject Data Architecture

Salah satu kesalahan paling umum saat membuat dialog game adalah menuliskan teks percakapan langsung di dalam script C# (hardcoding). Cara ini membuat kode berantakan dan sulit diedit oleh tim writer. 

Solusi terbaik di Unity adalah memanfaatkan **ScriptableObject**. Dengan arsitektur ini, data cerita disimpan sebagai file aset mandiri di dalam proyek, sementara skrip C# fokus menangani logika permainan.

---

## Membangun Struktur Data Utama

Kita membagi data cerita menjadi tiga kelas utama:

### 1. Character Profile (`CharacterProfileSO.cs`)
File profil ini menyimpan identitas karakter seperti nama, foto profil default, dan warna nama. Dengan cara ini, Anda tidak perlu mengetik ulang nama karakter di setiap baris dialog.

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Narrative System/Character Profile")]
public class CharacterProfileSO : ScriptableObject
{
    public string characterName;
    public Sprite defaultPortrait;
    public Color nameColor = Color.white;
    public AudioClip typingSound;
}
```

---

### 2. Dialogue Choice Option (`DialogueChoice.cs`)
Kelas data sederhana untuk menampung teks pilihan bercabang beserta node tujuan saat pemain memilih opsi tersebut.

```csharp
using System;

[Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueNodeSO targetNode;
    public string requiredConditionFlag;
}
```

---

### 3. Dialogue Node (`DialogueNodeSO.cs`)
Ini adalah unit inti dari percakapan kita. Satu node mewakili satu potongan dialog yang diucapkan oleh karakter.

```csharp
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Narrative System/Dialogue Node")]
public class DialogueNodeSO : ScriptableObject
{
    public CharacterProfileSO speaker;
    public Sprite customPortrait;
    [TextArea(3, 5)] public string dialogueText;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public DialogueNodeSO defaultNextNode;
}
```

---

## Mengapa Memakai ScriptableObject?

- **Bisa Digunakan Berulang Kali**: Satu file profil karakter atau node dialog bisa dipasang di berbagai scene tanpa duplikasi data.
- **Ramah untuk Narrative Designer**: Penulis cerita dapat langsung membuat dan mengedit percakapan lewat Unity Inspector tanpa menyentuh kode C#.

---

## Panduan Langkah Demi Langkah di Unity Editor

Setelah semua skrip C# di atas tersimpan tanpa error di proyek Unity Anda, ikuti langkah berikut untuk membuat aset dialog pertama Anda:

1. **Membuat Profil Karakter**:
   - Buka tab **Project Window**, masuk ke folder `Assets/NarrativeSystem/Data/`.
   - Klik kanan pada area kosong -> pilih **Create > Narrative System > Character Profile**.
   - Beri nama file aset tersebut, misalnya `Profile_Hero` atau `Profile_NPC_Shopkeeper`.
   - Klik file aset tersebut, lalu di **Inspector Window**, isi nama karakter dan pilih gambar profil bawaan.

2. **Membuat Node Percakapan**:
   - Klik kanan di Project Window -> pilih **Create > Narrative System > Dialogue Node**.
   - Beri nama file aset, misalnya `Node_Greeting` atau `Node_OfferQuest`.
   - Pada **Inspector Window**:
     - Geser (drag & drop) file `Profile_Hero` ke dalam slot **Speaker**.
     - Tuliskan pesan dialog pada area **Dialogue Text**.
     - Untuk dialog berurutan biasa, masukkan node berikutnya ke slot **Default Next Node**.
     - Untuk dialog pilihan bercabang, tambah opsi pada **Choices**, isi teks opsi, dan tentukan **Target Node** tujuannya.
