using UnityEditor;
using UnityEngine;


public static class CustomEditorGUILayout
{

    public static void PropertyDrawerWithEditButton(Rect rect, SerializedProperty property)
    {
        PropertyDrawerWithEditButton(rect, property, new GUIContent(property.displayName));
    }
    public static void PropertyDrawerWithEditButton(Rect rect, SerializedProperty property, GUIContent windowLabel)
    {
        var isString = (property.type == "string");
        var isVector3 = (property.type == "Vector3");
        var isVector3int = (property.type == "Vector3Int");
        var isVector2 = (property.type == "Vector2");
        var isVector2int = (property.type == "Vector2Int");
        var isUnityObjectReference = property.type.Contains("PPtr");
        if (property.hasChildren && !isString && !isUnityObjectReference && !isVector3 && !isVector3int && !isVector2 && !isVector2int)
        {
            if (GUI.Button(rect, "Edit"))
            {
                ChildPropertiesEditor.ShowWindowEditorProperty(property, windowLabel);
            }
        }
        else
        {
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        }
    }

}
