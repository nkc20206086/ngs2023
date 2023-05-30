using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    public class AudioSettingsEditView : MonoBehaviour, IAudioSettingsEditView
    {
        [SerializeField]
        private Slider master_volume_slider;
        [SerializeField]
        private Slider bgm_volume_slider;
        [SerializeField]
        private Slider se_volume_slider;

        [SerializeField]
        private TMP_Text master_volume_text;
        [SerializeField]
        private TMP_Text bgm_volume_text;
        [SerializeField]
        private TMP_Text se_volume_text;

        [Inject]
        private IAudioPlayer audioPlayer;

        public event Func<IGetSettingsData> GetSettingsData;
        public event Action<float> OnSetMasterVolume;
        public event Action<float> OnSetBGMVolume;
        public event Action<float> OnSetSEVolume;

        public enum AudioType
        {
            Master,
            BGM,
            SE,
        }

        private void Start()
        {
            //スライダーが変更されたとき、音量を変更
            master_volume_slider.onValueChanged.AddListener(volume => SetAudioMixerVolume(AudioType.Master, volume));
            bgm_volume_slider.onValueChanged.AddListener(volume => SetAudioMixerVolume(AudioType.BGM, volume));
            se_volume_slider.onValueChanged.AddListener(volume => SetAudioMixerVolume(AudioType.SE, volume));

            //スライダーの値を、現在の設定と同じにする
            master_volume_slider.SetValueWithoutNotify(GetSettingsData().MasterVolume);
            bgm_volume_slider.SetValueWithoutNotify(GetSettingsData().BGMVolume);
            se_volume_slider.SetValueWithoutNotify(GetSettingsData().SEVolume);

            master_volume_text.text = ((int)(GetSettingsData().MasterVolume * 100)).ToString();
            bgm_volume_text.text = ((int)(GetSettingsData().BGMVolume * 100)).ToString();
            se_volume_text.text = ((int)(GetSettingsData().SEVolume * 100)).ToString();

            master_volume_slider.GetComponent<SliderHelper>().OnEndDrag += data => PlayCheckVolumeSound();
            bgm_volume_slider.GetComponent<SliderHelper>().OnEndDrag += data => PlayCheckVolumeSound();
            se_volume_slider.GetComponent<SliderHelper>().OnEndDrag += data => PlayCheckVolumeSound();

        }

        private void PlayCheckVolumeSound()
        {
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_PlayGimmick");
        }

        private void SetAudioMixerVolume(AudioType type, float volume)
        {
            switch (type)
            {
                case AudioType.Master:
                    OnSetMasterVolume?.Invoke(volume);
                    master_volume_text.text = ((int)(GetSettingsData().MasterVolume * 100)).ToString();
                    break;
                case AudioType.BGM:
                    OnSetBGMVolume?.Invoke(volume);
                    bgm_volume_text.text = ((int)(GetSettingsData().BGMVolume * 100)).ToString();
                    break;
                case AudioType.SE:
                    OnSetSEVolume?.Invoke(volume);
                    se_volume_text.text = ((int)(GetSettingsData().SEVolume * 100)).ToString();
                    break;
            }
        }
    }
}
