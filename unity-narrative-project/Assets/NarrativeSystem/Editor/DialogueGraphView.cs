#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace NarrativeSystem.Editor
{
    /// <summary>
    /// Canvas GraphView visual editor untuk merender node-node dialog.
    /// </summary>
    public class DialogueGraphView : GraphView
    {
        public DialogueGraphView()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            GridBackground grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }
    }
}
#endif
