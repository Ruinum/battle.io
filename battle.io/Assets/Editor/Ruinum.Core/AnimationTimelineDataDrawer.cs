using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ruinum.Core.Editor
{
    [CustomPropertyDrawer(typeof(AnimationTimelineData), true)]
    public class AnimationTimelineDataDrawer : PropertyDrawer
    {
        private VisualElement _contentPanel;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement inspector = new VisualElement();
            inspector.AddToClassList("panel");

            return BuildPropertyUI(inspector, property);
        }

        private VisualElement BuildPropertyUI(VisualElement rootElement, SerializedProperty property)
        {
            Label label = new Label("123");
            rootElement.Add(label);
            return rootElement;
        }
    }
}