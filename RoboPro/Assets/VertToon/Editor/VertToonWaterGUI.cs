using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AyahaShader.VertToon
{
    public class VertToonWaterGUI : ShaderGUI
    {
        private MaterialProperty waterColor;
        private MaterialProperty foamColor;
        private MaterialProperty height;


        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] prop)
        {
            var material = (Material)materialEditor.target;
            FindProperties(prop);

            // äÓñ{èÓïÒÇï`âÊ
            VertToonCustomUI.Information();

            // èâä˙èÛë‘ÇÃGUIÇï\é¶Ç≥ÇπÇÈ
            //base.OnGUI(materialEditor, prop);

            // Color
            VertToonCustomUI.Title("Color");
            EditorGUI.indentLevel++;
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                materialEditor.ShaderProperty(waterColor, new GUIContent("Water Color"));
                materialEditor.ShaderProperty(foamColor, new GUIContent("Foam Color"));
            }
            EditorGUI.indentLevel--;

            // Height
            VertToonCustomUI.Title("Height");
            EditorGUI.indentLevel++;
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                materialEditor.ShaderProperty(height, new GUIContent("Height"));
            }
            EditorGUI.indentLevel--;
        }

        private void FindProperties(MaterialProperty[] prop)
        {
            waterColor = FindProperty("_WaterColor", prop, false);
            foamColor = FindProperty("_FoamColor", prop, false);
            height = FindProperty("_Height", prop, false);
        }
    }
}
