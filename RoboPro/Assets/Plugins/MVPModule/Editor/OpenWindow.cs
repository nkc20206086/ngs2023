#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MVPModule
{
    public class OpenWindow
    {
        [MenuItem("Tools/Koyo")]
        public static void Open()
        {
            var temp = EditorWindow.GetWindow<KoyoWindow>();

            int num = Random.Range(0, 10);

            if (num == 0)
            {
                temp.titleContent = new GUIContent("MARIJUAUA", Resources.Load("Marijuna") as Texture);
            }
            else
            {
                temp.titleContent = new GUIContent("KOYO", Resources.Load("Kaede") as Texture);
            }
        }
    }
}

#endif