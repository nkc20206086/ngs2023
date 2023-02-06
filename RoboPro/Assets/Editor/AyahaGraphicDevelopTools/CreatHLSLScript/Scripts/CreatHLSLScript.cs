//====================================================================================================
// v1.1.0
// Twitter : @ayaha__401
//====================================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreatHLSLScript : EditorWindow
{
    static string path;

    [MenuItem("Assets/Create/Shader/HLSLScript")]
    private static void CreatHLSL()
    {
        path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        path = EditorUtility.SaveFilePanelInProject("CreatHLSLScript", "NewShader", "hlsl", "", path);
        if (!string.IsNullOrEmpty(path))
        {
            path = AssetDatabase.GenerateUniqueAssetPath(path);
            
            File.WriteAllText(path,"");
            AssetDatabase.Refresh();
        }
    }
}
