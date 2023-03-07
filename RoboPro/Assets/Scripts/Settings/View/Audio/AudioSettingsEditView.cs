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

        private event Func<IGetSystemSettingsData> GetSettingsData;
        private event Action<float> OnSetMasterVolume;
        private event Action<float> OnSetBGMVolume;
        private event Action<float> OnSetSEVolume;

        event Func<IGetSystemSettingsData> IAudioSettingsEditView.GetSettingsData
        {
            add => GetSettingsData += value;
            remove => GetSettingsData -= value;
        }

        event Action<float> IAudioSettingsEditView.OnSetMasterVolume
        {
            add => OnSetMasterVolume += value;
            remove => OnSetMasterVolume -= value;
        }

        event Action<float> IAudioSettingsEditView.OnSetBGMVolume
        {
            add => OnSetBGMVolume += value;
            remove => OnSetBGMVolume -= value;
        }

        event Action<float> IAudioSettingsEditView.OnSetSEVolume
        {
            add => OnSetSEVolume += value;
            remove => OnSetSEVolume -= value;
        }

        private void Start()
        {
            //スライダーが変更されたとき、音量を変更
            master_volume_slider.onValueChanged.AddListener(volume => SetAudioMixerVolume(AudioSettingsType.Master, volume));
            bgm_volume_slider.onValueChanged.AddListener(volume => SetAudioMixerVolume(AudioSettingsType.BGM, volume));
            se_volume_slider.onValueChanged.AddListener(volume => SetAudioMixerVolume(AudioSettingsType.SE, volume));

            //スライダーの値を、現在の設定と同じにする
            master_volume_slider.SetValueWithoutNotify(GetSettingsData().MasterVolume);
            bgm_volume_slider.SetValueWithoutNotify(GetSettingsData().BGMVolume);
            se_volume_slider.SetValueWithoutNotify(GetSettingsData().SEVolume);
        }

        private void SetAudioMixerVolume(AudioSettingsType type, float volume)
        {
            switch (type)
            {
                case AudioSettingsType.Master:
                    OnSetMasterVolume?.Invoke(volume);
                    break;
                case AudioSettingsType.BGM:
                    OnSetBGMVolume?.Invoke(volume);
                    break;
                case AudioSettingsType.SE:
                    OnSetSEVolume?.Invoke(volume);
                    break;
            }
        }
    }
}
