using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChildPropertiesEditor : EditorWindow
{
    private SerializedProperty _property;

    public static void ShowWindowEditorProperty(SerializedProperty property)
    {
        ShowWindowEditorProperty(property, GUIContent.none);
    }
    public static void ShowWindowEditorProperty(SerializedProperty property, GUIContent label)
    {

        var wnd = EditorWindow.GetWindow<ChildPropertiesEditor>();
        wnd.Show();
        wnd.titleContent = label;
        wnd._property = property;
    }
    private void OnGUI()
    {
        if(_property ==null)
        {
            Close();
        }
        EditorGUILayout.PropertyField(_property);
        _property.serializedObject.ApplyModifiedProperties();
    }
}
