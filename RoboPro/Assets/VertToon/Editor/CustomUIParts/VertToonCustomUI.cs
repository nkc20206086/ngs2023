using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AyahaShader.VertToon
{
    /// <summary>
    /// マテリアルのインスペクターのUIを拡張する機能をまとめたクラス
    /// </summary>
    public static class VertToonCustomUI
    {
        /// <summary>
        /// 開閉可能な見出し
        /// </summary>
        /// <param name="label">見出し名</param>
        /// <param name="value">開閉</param>
        public static bool Foldout(string label, bool value)
        {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.font = new GUIStyle(EditorStyles.label).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.fixedHeight = 22;
            style.contentOffset = new Vector2(20f, -2f);

            var rect = GUILayoutUtility.GetRect(16f, 22f, style);
            GUI.Box(rect, label, style);

            var e = Event.current;

            var foldoutRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint)
            {
                EditorStyles.foldout.Draw(foldoutRect, false, false, value, false);
            }

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                value = !value;
                e.Use();
            }

            return value;
        }

        /// <summary>
        /// チェックボックス付きの見出し
        /// </summary>
        /// <param name="label">見出し名</param>
        /// <param name="value">開閉</param>
        public static bool ToggleFoldout(string label, bool value)
        {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.font = new GUIStyle(EditorStyles.label).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.fixedHeight = 22;
            style.contentOffset = new Vector2(20f, -2f);

            var rect = GUILayoutUtility.GetRect(16f, 22f, style);
            GUI.Box(rect, label, style);

            var e = Event.current;

            var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint)
            {
                EditorStyles.toggle.Draw(toggleRect, false, false, value, false);
            }

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                value = !value;
                e.Use();
            }

            return value;
        }

        /// <summary>
        /// 見出しを付ける
        /// </summary>
        /// <param name="label">見出し名</param>
        public static void Title(string label)
        {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.font = new GUIStyle(EditorStyles.label).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.fixedHeight = 22;
            style.contentOffset = new Vector2(20f, -2f);

            var rect = GUILayoutUtility.GetRect(16f, 22f, style);
            GUI.Box(rect, label, style);
        }

        /// <summary>
        /// 横線を描画する
        /// </summary>
        public static void GUIPartition()
        {
            GUI.color = Color.gray;
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            GUI.color = Color.white;
        }

        /// <summary>
        /// 基本情報を表示する
        /// </summary>
        public static void Information()
        {
            Title("Info");
            EditorGUI.indentLevel++;
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Version");
                    EditorGUILayout.LabelField("Version " + VertToonInfo.GetVersion());
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("How to use (Japanese)");
                    if (GUILayout.Button("How to use (Japanese)"))
                    {
                        System.Diagnostics.Process.Start(VertToonInfo.GetRepositoryLink());
                    }
                }
            }
            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Tiling と OffsetをFoldoutして見せる
        /// </summary>
        /// <param name="label">見出し名</param>
        /// <param name="materialEditor">MaterialEditor</param>
        /// <param name="prop">テクスチャProperty</param>
        /// <param name="display">開閉のbool</param>
        public static void TextureFoldout(string label, MaterialEditor materialEditor, MaterialProperty prop, ref bool display)
        {
            var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
            var e = Event.current;

            materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), prop, label, "");

            var toggle = new Rect(rect.x + 2.0f + EditorGUI.indentLevel, rect.y + 3.0f, 16.0f, 16.0f);
            if (e.type == EventType.Repaint)
            {
                EditorStyles.foldout.Draw(toggle, false, false, display, false);
            }

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }

            if (display)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    materialEditor.TextureScaleOffsetProperty(prop);
                }
            }
        }
    }
}
