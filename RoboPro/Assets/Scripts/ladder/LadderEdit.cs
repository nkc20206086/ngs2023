using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Ladder
{
    [CustomEditor(typeof(LadderController), true)]
    public class LadderEdit : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LadderController script = target as LadderController;

            if (GUILayout.Button("ÇÕÇµÇ≤ê∂ê¨"))
            {
                script.OnButtonClickInInspector();
            }
        }

    }
}
