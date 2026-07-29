using System;
using UnityEngine;

namespace NarrativeSystem.Data
{
    /// <summary>
    /// Struktur data yang merepresentasikan satu opsi pilihan pada dialog bercabang.
    /// </summary>
    [Serializable]
    public class DialogueChoice
    {
        [Tooltip("Teks pilihan yang akan muncul pada tombol UI.")]
        public string choiceText;

        [Tooltip("Node dialog berikutnya yang dituju jika pilihan ini diklik.")]
        public DialogueNodeSO targetNode;

        [Tooltip("Nama flag kondisi yang dibutuhkan dari StoryStateManager agar pilihan ini dapat muncul/aktif.")]
        public string requiredConditionFlag;
    }
}
