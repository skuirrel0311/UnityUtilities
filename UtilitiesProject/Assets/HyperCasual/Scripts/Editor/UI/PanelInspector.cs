using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

namespace HyperCasual.UI
{
    [CanEditMultipleObjects, CustomEditor (typeof(Panel), false)]
    public class PanelInspector : GraphicEditor
    {
        public override void OnInspectorGUI ()
        {
            base.serializedObject.Update ();

            GUI.enabled = false;
            EditorGUILayout.PropertyField (base.m_Script, new GUILayoutOption[0]);
            GUI.enabled = true;

            // skipping AppearanceControlsGUI
            base.RaycastControlsGUI ();
            base.serializedObject.ApplyModifiedProperties ();
        }
    }
}