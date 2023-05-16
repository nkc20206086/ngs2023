using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Robo
{
    //�L���[�V�[�g��ǂݍ���ŁA�V�[�����SE��BGM��点��悤�ɂ���N���X
    //�t�ɁA���̃N���X���Ȃ��V�[���ł͉���炷���Ƃ��ł��Ȃ�
    public class SceneAudioImporter : MonoBehaviour
    {
        /// <summary>�ǂݍ��ރL���[�V�[�g�ꗗ</summary>
        [SerializeField]
        private List<CueSheetType> sheetTypes;

        /// <summary>�V�[�������[�h�����Ƃ���BGM��炷���ǂ���</summary>
        [HideInInspector]
        public bool isSceneLoadToPlayBGM = false;

        /// <summary>�V�[�����[�h�����Ƃ��ɖ炷BGM</summary>
        [HideInInspector]
        public CueSheetType sceneLoadToPlayBGM;

        /// <summary>BGM�̃t�F�[�h�C���A�A�E�g�̎���(ms)</summary>
        [HideInInspector]
        public int bgmFadeMilliSecond = 1000;

        /// <summary>BGM���N���X�t�F�[�h�����邩�ǂ���</summary>
        [HideInInspector]
        public bool crossFadeBGM = true;

        [Inject]
        private IAudioPlayer audioPlayer;

        private async void Start()
        {
            //�V�[�����������ꂽ��A�L���[�V�[�g�����[�h����
            await audioPlayer.LoadSheets(sheetTypes);

            //BGM��炷
            if(isSceneLoadToPlayBGM)
            {
                audioPlayer.PlayBGMFade(sceneLoadToPlayBGM, bgmFadeMilliSecond, bgmFadeMilliSecond, crossFadeBGM);
            }
        }

        private void OnDestroy()
        {
            //�V�[�����A�����[�h�����Ƃ��A�L���[�V�[�g���A�����[�h����
            audioPlayer.UnloadSheets(sheetTypes);
        }
    }
}