using UnityEngine;
using UnityEngine.Audio;

namespace Robo
{
    public class AudioSettingsView : MonoBehaviour, IAudioSettingsView
    {
        [SerializeField]
        private AudioMixer audioMixer;

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("Master_Volume", CalcVolume(volume));
        }

        public void SetBGMVolume(float volume)
        {
            audioMixer.SetFloat("BGM_Volume", CalcVolume(volume));
        }

        public void SetSEVolume(float volume)
        {
            audioMixer.SetFloat("SE_Volume", CalcVolume(volume));
        }

        private float CalcVolume(float value)
        {
            //-80~0に変換
            float volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            return volume;
        }
    }
}