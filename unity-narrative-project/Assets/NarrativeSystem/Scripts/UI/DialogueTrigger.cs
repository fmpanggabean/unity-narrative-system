using NarrativeSystem.Core;
using NarrativeSystem.Data;
using UnityEngine;

namespace NarrativeSystem.UI
{
    /// <summary>
    /// Utility MonoBehaviour untuk memicu dialog saat Player masuk ke area Trigger Collider / menekan tombol Interaksi.
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Trigger Data")]
        [SerializeField] private DialogueNodeSO startingDialogueNode;
        [SerializeField] private bool triggerOnEnter = true;
        [SerializeField] private string requiredPlayerTag = "Player";

        private bool _isPlayerInside = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(requiredPlayerTag))
            {
                _isPlayerInside = true;
                if (triggerOnEnter)
                {
                    TriggerDialogue();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(requiredPlayerTag))
            {
                _isPlayerInside = false;
            }
        }

        private void Update()
        {
            if (!_isPlayerInside || triggerOnEnter) return;

            // Jika mode tombol manual (misal tekan E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }

        public void TriggerDialogue()
        {
            if (DialogueManager.Instance != null && startingDialogueNode != null)
            {
                DialogueManager.Instance.StartDialogue(startingDialogueNode);
            }
        }
    }
}
