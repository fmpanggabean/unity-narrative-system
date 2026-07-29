using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeSystem.Data
{
    /// <summary>
    /// ScriptableObject atomic unit yang mewakili satu bagian/percakapan dialog.
    /// Menyimpan teks dialog, pembicara, pilihan bercabang, serta event yang terpicu.
    /// </summary>
    [CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Narrative System/Dialogue Node")]
    public class DialogueNodeSO : ScriptableObject
    {
        [Header("Speaker Information")]
        [Tooltip("Profil karakter yang mengucapkan teks ini.")]
        public CharacterProfileSO speaker;

        [Tooltip("Custom portrait khusus node ini (opsional, meng-override default portrait karakter).")]
        public Sprite customPortrait;

        [Header("Dialogue Content")]
        [TextArea(3, 6)]
        [Tooltip("Isi percakapan yang akan ditampilkan di typewriter effect.")]
        public string dialogueText;

        [Header("Branching & Navigation")]
        [Tooltip("Daftar opsi pilihan bercabang. Jika diisi, defaultNextNode akan diabaikan.")]
        public List<DialogueChoice> choices = new List<DialogueChoice>();

        [Tooltip("Node selanjutnya jika tidak ada pilihan (linear flow).")]
        public DialogueNodeSO defaultNextNode;

        [Header("Game Events & State")]
        [Tooltip("Nama flag boolean yang akan diset ke true saat node ini dibuka (opsional).")]
        public string setStoryFlagOnReach;

        [Tooltip("UnityEvent yang diputar saat percakapan pada node ini dimulai.")]
        public UnityEvent onNodeReached;
    }
}
