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

            //�V�[����ŉ��y��炷���ǂ�����ݒ�
            imp.isSceneLoadToPlayBGM = EditorGUILayout.Toggle("IsSceneLoadToPlayBGM", imp.isSceneLoadToPlayBGM);

            //�V�[����ŉ��y��炷�悤�ł���΁A�炷���y�̏ڍׂ�ݒ�ł���悤�ɂ���
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