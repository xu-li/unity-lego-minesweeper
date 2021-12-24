using UnityEditor;
using UnityEngine;
using Unity.LEGO.Behaviours.Triggers;
using Unity.LEGO.EditorExt;

[CustomEditor(typeof(CheckMineTrigger), true)]
    public class CheckMineTriggerEditor: SensoryTriggerEditor
    {
        protected override void CreateGUI()
        {
        }
    }