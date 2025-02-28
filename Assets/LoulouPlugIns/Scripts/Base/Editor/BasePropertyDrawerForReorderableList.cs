using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace LouLouStarterContent.Editor
{

    //LCeleyron with help of W4ldschr31n 

    public abstract class BasePropertyDrawerForReorderableList : BasePropertyDrawer
    {
    // Ajouter le bail du nombre d'array    
        private Dictionary <string,ReorderableListProperties> _reorderableListProperties;
        protected SerializedProperty _p_List;
        //protected int _arraySize;
        protected GUIStyle _style;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ((EditorGUIUtility.singleLineHeight + 1) * GetReorderableListProperty(property).numberOfLine )+ (GetReorderableListProperty(property).foldout?GetOverridenPropertyHeight(property,label):0f);
        }

        /// <summary>
        /// Initialize the reorderable list and the dictionary used if your property is nested
        /// </summary>
        /// <param name="property"></param>
        public void Initializeproperties(SerializedProperty property)
        {
            _p_List = GetListProperty(property); //On dirait que c'est pas opti de le mettre la mais j'ai pas le choix he :Shrug:
            if (_reorderableListProperties == null)
            {
                _reorderableListProperties = new Dictionary<string, ReorderableListProperties>();
            }

            if (_reorderableListProperties.GetValueOrDefault(property.propertyPath, null) == null)
            {
                ReorderableList.ElementCallbackDelegate bridge = (Rect rect, int index, bool isActive, bool isFocused) => { DrawElement(property, rect, index, isActive, isFocused); };
                _reorderableListProperties[property.propertyPath] = new ReorderableListProperties();
                GetReorderableListProperty(property, false).list = new ReorderableList(property.serializedObject, _p_List, true, false, true, true);
                GetReorderableListProperty(property, false).list.drawElementCallback = bridge;
            }
            InitializeBonus(property);
        }

        protected virtual void InitializeBonus(SerializedProperty property)
        {

        }
        internal override void AtStartOfGUI(SerializedProperty property)
        {
            GetReorderableListProperty(property).numberOfLine = 1;

            _p_List = GetListProperty(property);
         
            AtStartOfGUIReorderableDrawer(property);

            //_arraySize = _p_List.arraySize;

            _style = new GUIStyle(EditorStyles.label);
            _style.alignment = TextAnchor.MiddleCenter;
        }
        internal override void OnGUIEffect(Rect position, SerializedProperty property)
        {
            var _rectFoldout = MakeRectForDrawer(0, .8f, 1, .0f);
            //var _rectArraySize = MakeRectForDrawer(0, .2f, 1, .0f);

            var tempFoldout = EditorGUI.Foldout(_rectFoldout, GetReorderableListProperty(property).foldout, GetPropertyName(property), true);
            //_arraySize = EditorGUI.IntField(_rectArraySize, _arraySize);
            //_p_List.arraySize = _arraySize;
            if (tempFoldout)
            {
                DrawList(property,_rectFoldout);
            }
            OnGUIEffectReorderableDrawer(position,property);


            GetReorderableListProperty(property).foldout = tempFoldout;
        }
        /// <summary>
        /// Draw the reorderable list on a rect
        /// </summary>
        /// <param name="property"></param>
        /// <param name="rectFoldout"></param>
        protected void DrawList(SerializedProperty property, Rect rectFoldout)
        {
            var _rect = new Rect(rectFoldout.x, rectFoldout.y + rectFoldout.height , usableSpace, GetReorderableListProperty(property).list.GetHeight());
            GetReorderableListProperty(property).list.DoList(_rect);
            property.serializedObject.ApplyModifiedProperties();
        }

#region To Override
        /// <summary>
        /// Override this to add code to the OnGUIEffect
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        protected virtual void OnGUIEffectReorderableDrawer(Rect position, SerializedProperty property)
        {

        }
        /// <summary>
        /// Override this to add code to the AtStartOfGUI
        /// </summary>
        /// <param name="property"></param>
        protected virtual void AtStartOfGUIReorderableDrawer(SerializedProperty property)
        {
        }
        /// <summary>
        /// Override this to change the Property Height of the unfolded list
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        protected virtual float GetOverridenPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _reorderableListProperties[property.propertyPath].list.GetHeight();
        }
        /// <summary>
        /// Override this to set up how each element of your list is displayed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="rect"></param>
        /// <param name="index"></param>
        /// <param name="isActive"></param>
        /// <param name="isFocused"></param>
        protected virtual void DrawElement(SerializedProperty property, Rect rect, int index, bool isActive, bool isFocused)
        {

        }
        /// <summary>
        /// Override this to change the displayed name of the list 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual string GetPropertyName(SerializedProperty property)
        {
            return property.displayName;
        }
        /// <summary>
        /// Override this to get the serialized property of the list to put on the reorderable list /!\ YOU HAVE TO OVERRIDE THIS FUNCTION FOR YOUR DRAWER TO WORK
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual SerializedProperty GetListProperty(SerializedProperty property)
        {
            return null;
        }

        #endregion

        #region Getter

        protected ReorderableListProperties GetReorderableListProperty(SerializedProperty property, bool withCheck = true)
        {
            if(!IsPropertyInitialized(property) && withCheck)
            {
                Initializeproperties(property);
            }
            return _reorderableListProperties[property.propertyPath];
        }
        protected bool IsPropertyInitialized(SerializedProperty property)
        {
            if(_reorderableListProperties ==null)
            {
                return false;
            }
            if (_reorderableListProperties.GetValueOrDefault(property.propertyPath,null)==null)
            {
                return false;
            }
            return true;
        }
        #endregion
    }

    [System.Serializable]
    public class ReorderableListProperties
    {
        public ReorderableList list;
        public bool foldout;
        public int numberOfLine;
    }

}