using System;
using UnityEngine;
using UnityEngine.UI;

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
        }

        private void SetAudioMixerVolume(AudioType type, float volume)
        {
            switch (type)
            {
                case AudioType.Master:
                    OnSetMasterVolume?.Invoke(volume);
                    break;
                case AudioType.BGM:
                    OnSetBGMVolume?.Invoke(volume);
                    break;
                case AudioType.SE:
                    OnSetSEVolume?.Invoke(volume);
                    break;
            }
        }
    }
}
