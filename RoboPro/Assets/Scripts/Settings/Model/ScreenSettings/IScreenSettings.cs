using System;
using UnityEngine;

namespace Robo
{
    public interface IScreenSettings
    {
        event Func<IGetSystemSettingsData> OnGetSettingsData;
        event Action<int> OnSetResolution;
        event Action<bool> OnSetIsFullScreen;

        /// <summary>解像度を設定</summary>
        void SetResolution(int id);
        /// <summary>フルスクリーンかどうかを設定</summary>
        void SetIsFullScreen(bool isFullScreen);

        /// <summary>設定データを取得</summary>
        IGetSystemSettingsData GetSettingsData();
        /// <summary>この端末で設定できる解像度を取得</summary>
        Resolution GetResolution(int id);
        /// <summary>この端末で設定できる解像度をすべて取得</summary>
        Resolution[] GetResolutions();
        /// <summary>現在の解像度を取得</summary>
        Resolution GetNowResolution();
    }
}
