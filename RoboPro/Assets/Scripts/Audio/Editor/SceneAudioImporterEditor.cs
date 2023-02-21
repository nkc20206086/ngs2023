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
            imp.isSceneLoadToPlayBGM = EditorGUILayout.Toggle("IsSceneLoadToPlayBGM", imp.isSceneLoadToPlayBGM);
            if(imp.isSceneLoadToPlayBGM)
            {
                imp.sceneLoadToPlayBGM = (CueSheetType)EditorGUILayout.EnumPopup("SceneLoadToPlayBGM", imp.sceneLoadToPlayBGM);
                imp.bgmFadeMilliSecond = EditorGUILayout.IntField("BGMFadeMilliSecond", imp.bgmFadeMilliSecond);
                imp.crossFadeBGM = EditorGUILayout.Toggle("IsSceneLoadToPlayBGM", imp.crossFadeBGM);
            }
        }
    }
}