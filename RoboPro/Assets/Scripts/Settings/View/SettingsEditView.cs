using System;
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    public class SettingsEditView : MonoBehaviour, ISettingsEditView
    {
        [SerializeField] private Button saveButton;

        public event Action OnSave;
        public event Action OnLoad;

        private void Start()
        {
            saveButton.onClick.AddListener(() => OnSave());
            OnLoad?.Invoke();
        }
    }
}