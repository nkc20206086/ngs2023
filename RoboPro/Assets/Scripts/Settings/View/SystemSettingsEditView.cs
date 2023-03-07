using System;
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    public class SystemSettingsEditView : MonoBehaviour, ISystemSettingsEditView
    {
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;

        private event Action OnSave;
        private event Action OnLoad;

        event Action ISystemSettingsEditView.OnSave
        {
            add => OnSave += value;
            remove => OnSave -= value;
        }

        event Action ISystemSettingsEditView.OnLoad
        {
            add => OnLoad += value;
            remove => OnLoad -= value;
        }

        private void Start()
        {
            saveButton.onClick.AddListener(() => OnSave());
            loadButton.onClick.AddListener(() => OnLoad());
        }
    }
}