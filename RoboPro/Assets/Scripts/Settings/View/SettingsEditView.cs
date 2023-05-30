using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    public class SettingsEditView : MonoBehaviour, ISettingsEditView
    {
        [SerializeField] private Button saveButton;

        [Inject]
        private IAudioPlayer audioPlayer;

        public event Action OnSave;
        public event Action OnLoad;

        private void Start()
        {
            saveButton.onClick.AddListener(Save);
            OnLoad?.Invoke();
        }

        private void Save()
        {
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_PlayGimmick");
            OnSave?.Invoke();
        }
    }
}