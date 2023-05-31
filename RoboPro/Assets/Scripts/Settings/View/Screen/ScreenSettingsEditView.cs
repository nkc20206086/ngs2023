using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    public class ScreenSettingsEditView : MonoBehaviour, IScreenSettingsEditView
    {
        [SerializeField]
        private Toggle isFullScreenToggle;
        
        [SerializeField]
        private TMP_Dropdown screenResolutionsDropdown;

        public event Func<IGetSettingsData> GetSettingsData;
        public event Func<Resolution[]> GetResolutions;
        public event Action<bool> OnSetIsFullScreen;
        public event Action<int> OnSetScreenResolution;

        public void Start()
        {
            isFullScreenToggle.onValueChanged.AddListener(SetIsFullScreen);
            screenResolutionsDropdown.onValueChanged.AddListener(SetScreenResolution);

            //モデルから取得した画面解像度一覧を、ドロップダウンに設定
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            foreach (Resolution resolution in GetResolutions())
            {
                //1920x1080のような、ドロップダウンのオプションを作成
                string text = string.Format("{0}x{1}", resolution.width, resolution.height);
                options.Add(new TMP_Dropdown.OptionData(text));
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
