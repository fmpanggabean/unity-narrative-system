using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace NarrativeSystem.Core
{
    /// <summary>
    /// Utility komponen untuk menampilkan teks dialog huruf demi huruf (Typewriter Effect).
    /// </summary>
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textContainer;
        [SerializeField] private float typingSpeed = 0.03f;
        [SerializeField] private AudioSource audioSource;

        private Coroutine _typingCoroutine;

        public bool IsTyping { get; private set; }

        public void Run(string textToType, AudioClip typingSound = null, Action onComplete = null)
        {
            if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
            _typingCoroutine = StartCoroutine(TypeText(textToType, typingSound, onComplete));
        }

        private IEnumerator TypeText(string textToType, AudioClip typingSound, Action onComplete)
        {
            IsTyping = true;
            textContainer.text = "";

            foreach (char charToken in textToType)
            {
                textContainer.text += charToken;

                if (typingSound != null && audioSource != null && charToken != ' ')
                {
                    audioSource.PlayOneShot(typingSound);
                }

                yield return new WaitForSeconds(typingSpeed);
            }

            IsTyping = false;
            onComplete?.Invoke();
        }

        /// <summary>
        /// Menghentikan efek typewriter dan langsung mengisikan teks penuh secara instant.
        /// </summary>
        public void QuickComplete(string fullText)
        {
            if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
            textContainer.text = fullText;
            IsTyping = false;
        }
    }
}
