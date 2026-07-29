#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NarrativeSystem.Editor
{
    /// <summary>
    /// EditorWindow container untuk membuka Custom GraphView Visual Node Editor dari Unity Top Menu.
    /// </summary>
    public class DialogueGraphWindow : EditorWindow
    {
        private DialogueGraphView _graphView;

        [MenuItem("Tools/Narrative System/Dialogue Graph Editor")]
        public static void OpenWindow()
        {
            DialogueGraphWindow window = GetWindow<DialogueGraphWindow>();
            window.titleContent = new GUIContent("Dialogue Graph Editor");
        }

        private void OnEnable()
        {
            ConstructGraphView();
        }

        private void OnDisable()
        {
            if (_graphView != null)
            {
                rootVisualElement.Remove(_graphView);
            }
        }

        private void ConstructGraphView()
        {
            _graphView = new DialogueGraphView
            {
                name = "Dialogue Graph View"
            };
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }
    }
}
#endif
