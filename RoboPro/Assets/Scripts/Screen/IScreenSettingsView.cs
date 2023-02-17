using System;
using UnityEngine;

namespace Robo
{
    public interface IScreenSettingsView
    {
        /// <summary>このゲームで設定できる解像度を取得</summary>
        event Func<int, Resolution> GetResolution;
        /// <summary>解像度を設定</summary>
        void SetResolution(int id);
        /// <summary>フルスクリーンかどうかを設定</summary>
        void SetIsFullScreen(bool isFullScreen);
    }
}
