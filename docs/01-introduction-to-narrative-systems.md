# Modul 1: Pengantar Narrative System di Unity

Saat membangun game berbasis cerita—seperti RPG, Adventure, atau Visual Novel—narrative system menjadi pondasi utama yang menghubungkan pemain dengan dunia game. Sebelum langsung menulis kode C#, penting untuk memahami gambaran besar arsitektur sistem dialog agar kode yang disusun tetap rapi dan mudah dikembangkan.

---

## Apa itu Narrative System?

Dalam pengembangan game, sistem naratif tidak sekadar menampilkan kotak teks sederhana di layar. Sistem ini merupakan gabungan beberapa komponen yang saling bekerja sama:

1. **Dialogue Engine**: Mengatur giliran dan alur percakapan yang dimunculkan.
2. **Character & Audio System**: Menampilkan identitas pembicara, foto profil (portrait), serta memicu efek suara/voiceover.
3. **Choice & Branching System**: Membuka cabang pilihan bagi pemain yang dapat mengubah alur cerita.
4. **State Engine / Condition Checker**: Memeriksa kondisi game (misalnya apakah pemain sudah memiliki kunci atau pernah mengalahkan boss tertentu).

---

## Memilih Arsitektur yang Tepat

Secara garis besar, ada dua pendekatan utama yang sering digunakan di Unity:

### 1. Sequential / Linear List Pattern
Semua baris dialog disimpan berurutan dalam sebuah daftar (`List<DialogueLine>`). Pendekatan ini sangat simpel dan cocok untuk dialog linier tanpa cabang pilihan.

```text
[Line 1] -> [Line 2] -> [Line 3] -> [Selesai]
```

### 2. Graph / Tree Pattern (Node-Based)
Dialog dipecah menjadi beberapa **Node** independen yang saling terhubung. Pendekatan ini adalah standar industri untuk RPG bercabang karena memudahkan pengaturan pilihan dan percabangan cerita yang kompleks.

```text
          +-> [Pilihan A] -> [Node A1]
[Node 1] -|
          +-> [Pilihan B] -> [Node B1]
```

---

## Pendekatan yang Kita Gunakan

Sepanjang materi ini, kita fokus membangun **Node-Based Data Architecture** memanfaatkan **ScriptableObject** dan **Unity GraphView**. Pendekatan ini dipilih karena membuat data cerita terpisah dari logika game, sehingga mudah diatur oleh narrative designer tanpa perlu mengedit skrip C#.

---

## Sample Assets & Credits

Untuk mempermudah latihan visual UI selama mengikuti materi ini, kita menggunakan aset UI gratis:
- **Kenney UI Pack (Pixel Adventure)**: [https://kenney.nl/assets/ui-pack-pixel-adventure](https://kenney.nl/assets/ui-pack-pixel-adventure) (Lisensi Public Domain CC0 oleh Kenney).
