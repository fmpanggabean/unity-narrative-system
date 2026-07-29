# Modul 1: Pengantar Narrative System di Unity

Narrative System adalah salah satu core pillar dalam modern game development, khususnya pada genre seperti RPG, Adventure, Interactive Fiction, dan Visual Novel. Pada modul ini, kita akan mempelajari konsep dasar architectural pattern dari narrative system sebelum masuk ke implementasi teknis C#.

---

## Apa itu Narrative System?

Narrative System bukan sekadar menampilkan teks di layar. Ini adalah sekumpulan sistem terinterkoneksi untuk menyampaikan cerita, yang terdiri dari:

1. **Dialogue Engine**: Mengontrol alur dan urutan dialogue lines yang ditampilkan.
2. **Character & Audio System**: Menampilkan nama karakter, portrait, serta memicu voiceover atau sound effects (SFX).
3. **Choice & Branching System**: Memberikan pilihan kepada player yang menentukan arah alur cerita.
4. **State Engine / Condition Checker**: Memeriksa world state (misalnya, apakah item sudah diambil atau NPC sudah diajak bicara).

---

## Pattern Arsitektur Narrative System

Secara umum, terdapat dua pendekatan arsitektur utama di Unity:

### 1. Sequential / Linear List Pattern
Dialogue lines disimpan secara sekuensial sebagai `List<DialogueLine>`. Sangat cocok untuk game linear tanpa branching paths.

```
[Line 1] -> [Line 2] -> [Line 3] -> [End]
```

### 2. Graph / Tree Pattern (Node-Based)
Dialogue terstruktur sebagai **Node** yang terhubung melalui edge atau target reference. Ideal untuk branching RPG dan decision tree yang kompleks.

```
          +-> [Node Choice A] -> [Node A1]
[Node 1] -|
          +-> [Node Choice B] -> [Node B1]
```

---

## Kesimpulan
Sepanjang course ini, kita akan fokus pada **Node-Based Data Architecture** memanfaatkan **ScriptableObject** dan **Unity GraphView**, sehingga menghasilkan dialogue framework yang sangat fleksibel dan extensible.

---

## Sample Assets & Credits
Untuk keperluan contoh proyek dan latihan visual UI di Unity:
- **Kenney UI Pack (Pixel Adventure)**: [https://kenney.nl/assets/ui-pack-pixel-adventure](https://kenney.nl/assets/ui-pack-pixel-adventure) (Lisensi CC0 Public Domain oleh Kenney).
