using System.Collections.Generic;
using NarrativeSystem.Data;
using NarrativeSystem.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NarrativeSystem.Core
{
    /// <summary>
    /// Engine utama pengelola antrean dialog, tampilan UI, dan alur bercabang.
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        [Header("UI Containers")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI speakerNameText;
        [SerializeField] private Image portraitImage;
        [SerializeField] private TypewriterEffect typewriter;

        [Header("Choice UI Handler")]
        [SerializeField] private ChoiceUIHandler choiceHandler;

        private DialogueNodeSO _currentNode;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }

        public void StartDialogue(DialogueNodeSO startingNode)
        {
            if (startingNode == null) return;

            dialoguePanel.SetActive(true);
            DisplayNode(startingNode);
        }

        public void DisplayNode(DialogueNodeSO node)
        {
            _currentNode = node;

            // Trigger Story Flag jika ada
            if (!string.IsNullOrEmpty(node.setStoryFlagOnReach) && StoryStateManager.Instance != null)
            {
                StoryStateManager.Instance.SetFlag(node.setStoryFlagOnReach, true);
            }

            // Trigger Unity Events
            node.onNodeReached?.Invoke();

            // Set Speaker Info
            if (speakerNameText != null)
            {
                speakerNameText.text = node.speaker != null ? node.speaker.characterName : "";
                if (node.speaker != null) speakerNameText.color = node.speaker.nameColor;
            }

            // Set Portrait
            if (portraitImage != null)
            {
                Sprite targetPortrait = node.customPortrait != null ? node.customPortrait : (node.speaker != null ? node.speaker.defaultPortrait : null);
                portraitImage.gameObject.SetActive(targetPortrait != null);
                portraitImage.sprite = targetPortrait;
            }

            // Play Typewriter
            AudioClip sound = node.speaker != null ? node.speaker.typingSound : null;
            typewriter.Run(node.dialogueText, sound, OnTextComplete);
        }

        private void OnTextComplete()
        {
            // Setelah teks selesai diketik, tampilkan pilihan jika ada
            if (_currentNode.choices != null && _currentNode.choices.Count > 0)
            {
                List<DialogueChoice> validChoices = FilterAvailableChoices(_currentNode.choices);
                if (choiceHandler != null && validChoices.Count > 0)
                {
                    choiceHandler.RenderChoices(validChoices, SelectChoice);
                }
            }
        }

        public void OnClickNext()
        {
            if (typewriter.IsTyping)
            {
                typewriter.QuickComplete(_currentNode.dialogueText);
                OnTextComplete();
                return;
            }

            // Jika ada pilihan bercabang, pergerakan dikontrol oleh klik pilihan
            if (_currentNode.choices != null && _currentNode.choices.Count > 0) return;

            if (_currentNode.defaultNextNode != null)
            {
                DisplayNode(_currentNode.defaultNextNode);
            }
            else
            {
                EndDialogue();
            }
        }

        public void SelectChoice(DialogueChoice choice)
        {
            if (choiceHandler != null) choiceHandler.ClearChoices();

            if (choice.targetNode != null)
            {
                DisplayNode(choice.targetNode);
            }
            else
            {
                EndDialogue();
            }
        }

        private List<DialogueChoice> FilterAvailableChoices(List<DialogueChoice> rawChoices)
        {
            List<DialogueChoice> available = new List<DialogueChoice>();
            foreach (var choice in rawChoices)
            {
                if (string.IsNullOrEmpty(choice.requiredConditionFlag) ||
                    (StoryStateManager.Instance != null && StoryStateManager.Instance.GetFlag(choice.requiredConditionFlag)))
                {
                    available.Add(choice);
                }
            }
            return available;
        }

        public void EndDialogue()
        {
            if (choiceHandler != null) choiceHandler.ClearChoices();
            if (dialoguePanel != null) dialoguePanel.SetActive(false);
            _currentNode = null;
        }
    }
}
