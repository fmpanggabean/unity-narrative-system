#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NarrativeSystem.Editor
{
    /// <summary>
    /// Visual Node Element untuk mewakili DialogueNode di dalam DialogueGraphView window.
    /// </summary>
    public class DialogueNodeView : Node
    {
        public string GUID { get; set; }
        public string DialogueText { get; set; }

        public DialogueNodeView()
        {
            title = "Dialogue Node";
        }
    }
}
#endif
