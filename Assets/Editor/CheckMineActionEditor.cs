using UnityEditor;
using UnityEngine;
using Unity.LEGO.Behaviours.Triggers;
using Unity.LEGO.EditorExt;

[CustomEditor(typeof(CheckMineAction), true)]
    public class CheckMineActionEditor : ActionEditor
    {
        
        protected override void CreateGUI()
        {
            EditorGUILayout.PropertyField(m_AudioProp);
            EditorGUILayout.PropertyField(m_AudioVolumeProp);

        }
    }