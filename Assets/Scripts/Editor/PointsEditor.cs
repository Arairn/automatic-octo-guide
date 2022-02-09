using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Points))]


public class PointsDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var currentRect = new Rect(position.x, position.y, position.width/2, position.height);
        var maxRect = new Rect(position.x + position.width/2, position.y, position.width, position.height);
        //var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative("current"), GUIContent.none);
        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), GUIContent.none);
        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}