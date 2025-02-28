using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

//To check https://discussions.unity.com/t/how-to-properly-implement-reorderablelist-expanding-collapsing-in-custompropertydrawer/248502 
//https://discussions.unity.com/t/reorderablelist-do-not-resize-a-height-of-each-element-property-drawer/245554
//https://discussions.unity.com/t/custom-property-drawer-height-and-width-solved/663817/2

namespace LouLouStarterContent.Editor
{

    [CustomPropertyDrawer(typeof(SerializedWeightedListParent), true)]
    public class WeightedListEditor : BasePropertyDrawerForReorderableList
    {

        private SerializedWeightedListParent _targetWeightedList;

        protected override SerializedProperty GetListProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative("_weightedElementsList");
        }
        protected override void AtStartOfGUIReorderableDrawer(SerializedProperty property)
        {
            _targetWeightedList = GetTargetAs< SerializedWeightedListParent>();
            _targetWeightedList.InitializeIfNull();
        }

        protected override void DrawElement(SerializedProperty property, Rect rect, int index, bool isActive, bool isFocused)
        {
            var _currentSerializedElement = GetReorderableListProperty(property).list.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("Element");
            var _currentSerializedWeight = GetReorderableListProperty(property).list.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("Weight");

            var _ElementRect = new Rect(rect.x, rect.y, rect.width * 7 / 10, rect.height);
            var _WeightLabelRect = new Rect(rect.x + rect.width * 7 / 10, rect.y, rect.width * 1 / 10, rect.height);
            var _WeightRect = new Rect(rect.x + rect.width * 8 / 10, rect.y, rect.width * 2 / 10, rect.height);

            CustomEditorGUILayout.PropertyDrawerWithEditButton(_ElementRect, _currentSerializedElement, new GUIContent("Element n°" + index));
            EditorGUI.LabelField(_WeightLabelRect, "Weight", _style);
            EditorGUI.PropertyField(_WeightRect, _currentSerializedWeight, GUIContent.none);

        }
    }

}