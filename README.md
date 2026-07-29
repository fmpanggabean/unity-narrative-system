# Unity Narrative System Course

Selamat datang di course komprehensif tentang **Pembuatan Narrative & Dialogue System di Unity Engine**.

Course ini dirancang untuk membimbing Anda dari konsep dasar penulisan interactive narrative hingga membangun branching dialogue system yang modular, scalable, dan terintegrasi dengan game mechanics di Unity.

---

## Learning Objectives

Setelah menyelesaikan course ini, Anda akan mampu:
- Merancang dialogue data structure yang bersih dan fleksibel menggunakan ScriptableObject.
- Membangun Dialogue Manager dan UI yang responsif dengan efek typewriter khas RPG/Visual Novel.
- Mengimplementasikan branching choices dan condition checking system.
- Menghubungkan dialogue variables dengan Quest System, Inventory, dan Game State.
- Membuat custom Visual Node Editor (GraphView) di dalam Unity Editor untuk memudahkan narrative designer.

---

## Course Structure

1. [Modul 1: Pengantar Narrative System](docs/01-introduction-to-narrative-systems.md)  
   Memahami tipe narrative system dalam game, arsitektur dasar, dan kebutuhan narrative designer.
2. [Modul 2: ScriptableObject Data Architecture](docs/02-scriptableobject-dialogue-data.md)  
   Membuat dialogue node, character profile, dan story choices tanpa hardcoding.
3. [Modul 3: Dialogue Manager & UI Text Typewriter](docs/03-dialogue-ui-manager.md)  
   Mengatur alur eksekusi dialog dan membuat typewriter effect menggunakan TextMeshPro & C# Async/Coroutine.
4. [Modul 4: Branching Dialogue & Event System](docs/04-branching-choices-and-events.md)  
   Mengimplementasikan branching narrative paths berdasarkan pilihan player dan memicu game events.
5. [Modul 5: Quest & Game State Integration](docs/05-quest-and-inventory-integration.md)  
   Menghubungkan dialogue variables dengan quest completion status dan inventory items.
6. [Modul 6: Custom Visual Node Editor (GraphView)](docs/06-advanced-custom-node-editor.md)  
   Membangun custom node-based editor di Unity Editor untuk visual dialogue tree editing.

---

## Prerequisites
- Pemahaman dasar Unity Engine (disarankan versi Unity 2021.3 LTS atau yang lebih baru).
- Pengetahuan dasar pemrograman C# (Class, Inheritance, List, Event/Delegate).
- Package TextMeshPro sudah terinstall di Unity Project Anda.
