using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ruinum.Core.Editor
{
    [CustomPropertyDrawer(typeof(AnimationTimelineKey))]
    public class AnimationTimelineKeyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keyName = property.FindPropertyRelative("Name");
            var keyType = property.FindPropertyRelative("Type");
            var keyTime = property.FindPropertyRelative("Time");

            position.width = 45;
            EditorGUI.LabelField(position, "Name:");          
            
            position.x += Space(position, 5);
            position.width = 120;
            
            keyName.stringValue = EditorGUI.TextField(position, keyName.stringValue);

            position.x += Space(position, 5);
            position.width = 45;
            EditorGUI.LabelField(position, "Time:");

            position.x += Space(position);
            position.width = 120;

            keyTime.floatValue = float.Parse(EditorGUI.TextField(position, keyTime.floatValue.ToString()));

            position.x += Space(position, 10);
            position.width = 100;

            Dictionary<AnimationTimelineKeyType, int> typeEnums = new Dictionary<AnimationTimelineKeyType, int>();
            AnimationTimelineKeyType[] values = (AnimationTimelineKeyType[])Enum.GetValues(typeof(AnimationTimelineKeyType));
            for (int i = 0; i < values.Length; i++)
            {
                typeEnums.Add(values[i], i);
            }

            if (keyType.enumValueIndex >= values.Length) keyType.enumValueIndex = 0;
            else
            {
                AnimationTimelineKeyType type = (AnimationTimelineKeyType)EditorGUI.EnumPopup(position, values[keyType.enumValueIndex]);
                if (typeEnums.TryGetValue(type, out int index)) keyType.enumValueIndex = index;
            }
        }

        private float Space(Rect position, int addSpace = 0)
        {
            return position.width + addSpace;
        }
    }
}