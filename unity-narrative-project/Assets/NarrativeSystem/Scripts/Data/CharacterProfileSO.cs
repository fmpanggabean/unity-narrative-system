using UnityEngine;

namespace NarrativeSystem.Data
{
    /// <summary>
    /// ScriptableObject untuk menyimpan profil karakter/NPC yang berbicara.
    /// Memungkinkan reusability data seperti Nama, Foto Profil Default, dan Warna Nama.
    /// </summary>
    [CreateAssetMenu(fileName = "New Character Profile", menuName = "Narrative System/Character Profile")]
    public class CharacterProfileSO : ScriptableObject
    {
        [Header("Character Identity")]
        [Tooltip("Nama karakter yang akan ditampilkan di UI Header Dialogue.")]
        public string characterName;

        [Tooltip("Warna teks nama karakter di UI.")]
        public Color nameColor = Color.white;

        [Header("Visuals")]
        [Tooltip("Portrait default karakter jika DialogueNode tidak menentukan custom portrait.")]
        public Sprite defaultPortrait;

        [Header("Audio (Optional)")]
        [Tooltip("Audio clip / blip sound yang diputar saat teks sedang diketik.")]
        public AudioClip typingSound;
    }
}
