using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//LCeleyron with help of W4ldschr31n 

namespace LouLouStarterContent.Editor
{

    [CustomPropertyDrawer(typeof(BaseDictionary), true)]
    public class BaseDictionaryDrawer : BasePropertyDrawerForReorderableList
    {
        private BaseDictionary _dictionaryTarget;
        private const int _warningLine = 2;
        private float DictionaryHeight(SerializedProperty property) => (GetReorderableListProperty(property).foldout? GetReorderableListProperty(property).list.GetHeight() + 10 : 0);

        protected override void AtStartOfGUIReorderableDrawer(SerializedProperty property)
        {
            _dictionaryTarget = GetTargetAs<BaseDictionary>();
            _dictionaryTarget.InititializeIfNull();

            //Aucune idée on verra demain je vais me coucher 
            //Essaye d'ajouter un bonus d'override dans le initialize list ???? ou alors jsp directement dans la classe d'ajouter un bonus ptetre ? A voir aller zoubi
        }


        protected override float GetOverridenPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return DictionaryHeight(property) ;
        }

        protected override SerializedProperty GetListProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative("_dictionaryEntries");
        }


        protected override void DrawElement(SerializedProperty property, Rect rect, int index, bool isActive, bool isFocused)
        {
            var currentSerializedKey = GetReorderableListProperty(property).list.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("key");
            var currentSerializedValue = GetReorderableListProperty(property).list.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("value");

            var keyRect = new Rect(rect.x, rect.y, rect.width * 7 / 10, rect.height);
            var valueLabelRect = new Rect(rect.x + rect.width * 7 / 10, rect.y, rect.width * 1 / 10, rect.height);
            var valueRect = new Rect(rect.x + rect.width * 8 / 10, rect.y, rect.width * 2 / 10, rect.height);

            //if the element has subsection (List/pure class etc...) it will not be displayed. Maybe you can check if the property has subproperty and add an Edit button that would open a window to edit the said property

            EditorGUI.PropertyField(keyRect, currentSerializedKey, GUIContent.none);
            EditorGUI.LabelField(valueLabelRect, "Value", _style);
            CustomEditorGUILayout.PropertyDrawerWithEditButton(valueRect, currentSerializedValue, new GUIContent("Value n°" + index));
        }

        protected override void OnGUIEffectReorderableDrawer(Rect position, SerializedProperty property)
        {
            
            GetReorderableListProperty(property).numberOfLine = _dictionaryTarget.HasTheSameKeyTwice?_warningLine+1:1;

            if (_dictionaryTarget.HasTheSameKeyTwice)
            {
                var _rectWarning = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + DictionaryHeight(property), usableSpace, EditorGUIUtility.singleLineHeight * _warningLine);
                EditorGUI.HelpBox(_rectWarning, "You have more than one key with the same value, you might have some trouble for calling this key.", MessageType.Warning);
            }
        }

        
    }

}