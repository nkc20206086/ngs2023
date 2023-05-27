#if UNITY_EDITOR

using UnityEditor;

namespace Robo
{
    [CustomEditor(typeof(SceneAudioImporter))]
    public class SceneAudioImporterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            SceneAudioImporter imp = target as SceneAudioImporter;

            //シーン上で音楽を鳴らすかどうかを設定
            imp.isSceneLoadToPlayBGM = EditorGUILayout.Toggle("IsSceneLoadToPlayBGM", imp.isSceneLoadToPlayBGM);

            //シーン上で音楽を鳴らすようであれば、鳴らす音楽の詳細を設定できるようにする
            if (imp.isSceneLoadToPlayBGM)
            {
                imp.sceneLoadToPlayBGM = (CueSheetType)EditorGUILayout.EnumPopup("SceneLoadToPlayBGM", imp.sceneLoadToPlayBGM);
                imp.bgmFadeMilliSecond = EditorGUILayout.IntField("BGMFadeMilliSecond", imp.bgmFadeMilliSecond);
                imp.crossFadeBGM = EditorGUILayout.Toggle("IsSceneLoadToPlayBGM", imp.crossFadeBGM);
            }
        }
    }
}
#endif