using System;
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    public class SettingsEditView : MonoBehaviour, ISettingsEditView
    {
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;

        public event Action OnSave;
        public event Action OnLoad;

        private void Start()
        {
            saveButton.onClick.AddListener(() => OnSave());
            loadButton.onClick.AddListener(() => OnLoad());
        }
    }
}