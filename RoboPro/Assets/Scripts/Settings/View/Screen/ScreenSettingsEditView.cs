using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    public class ScreenSettingsEditView : MonoBehaviour, IScreenSettingsEditView
    {
        [SerializeField]
        private Toggle isFullScreenToggle;
        
        [SerializeField]
        private Dropdown screenResolutionsDropdown;

        private event Func<IGetSettingsData> GetSettingsData;
        private event Func<Resolution[]> GetResolutions;
        private event Action<bool> OnSetIsFullScreen;
        private event Action<int> OnSetScreenResolution;

        event Func<IGetSettingsData> IScreenSettingsEditView.GetSettingsData
        {
            add => GetSettingsData += value;
            remove => GetSettingsData -= value;
        }

        event Func<Resolution[]> IScreenSettingsEditView.GetResolutions
        {
            add => GetResolutions += value;
            remove => GetResolutions -= value;
        }

        event Action<bool> IScreenSettingsEditView.OnSetIsFullScreen
        {
            add => OnSetIsFullScreen += value;
            remove => OnSetIsFullScreen -= value;
        }

        event Action<int> IScreenSettingsEditView.OnSetScreenResolution
        {
            add => OnSetScreenResolution += value;
            remove => OnSetScreenResolution -= value;
        }

        public void Start()
        {
            isFullScreenToggle.onValueChanged.AddListener(SetIsFullScreen);
            screenResolutionsDropdown.onValueChanged.AddListener(SetScreenResolution);

            //モデルから取得した画面解像度一覧を、ドロップダウンに設定
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            foreach (Resolution resolution in GetResolutions())
            {
                //1920x1080のような、ドロップダウンのオプションを作成
                string text = string.Format("{0}x{1}", resolution.width, resolution.height);
                options.Add(new Dropdown.OptionData(text));
            }
            //オプションを反映
            screenResolutionsDropdown.AddOptions(options);

            //元々の設定データを反映
            isFullScreenToggle.SetIsOnWithoutNotify(GetSettingsData().IsFullScreen);
            screenResolutionsDropdown.SetValueWithoutNotify(GetSettingsData().ScreenResolutionID);
        }

        private void SetIsFullScreen(bool isFullScreen)
        {
            OnSetIsFullScreen?.Invoke(isFullScreen);
        }

        private void SetScreenResolution(int id)
        {
            OnSetScreenResolution?.Invoke(id);
        }
    }
}
