using System;
using System.Collections.Generic;
using NarrativeSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NarrativeSystem.UI
{
    /// <summary>
    /// Pengelola tampilan tombol pilihan bercabang secara dinamis di UI Canvas.
    /// </summary>
    public class ChoiceUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceButtonContainer;

        public void RenderChoices(List<DialogueChoice> choices, Action<DialogueChoice> onChoiceSelected)
        {
            ClearChoices();

            foreach (var choice in choices)
            {
                GameObject btnObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
                TextMeshProUGUI label = btnObj.GetComponentInChildren<TextMeshProUGUI>();
                if (label != null) label.text = choice.choiceText;

                Button btn = btnObj.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => {
                        onChoiceSelected?.Invoke(choice);
                    });
                }
            }
        }

        public void ClearChoices()
        {
            if (choiceButtonContainer == null) return;
            foreach (Transform child in choiceButtonContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
