using UnityEngine;

namespace Robo
{
    [System.Serializable]
    public class SettingsData : ISetSystemSettingsData, IGetSystemSettingsData
    {
        [SerializeField]
        private float master_volume;
        [SerializeField]
        private float bgm_volume;
        [SerializeField]
        private float se_volume;
        [SerializeField]
        private int screen_resolution_id;
        [SerializeField]
        private bool isFullScreen;

        /// <summary>マスター音量</summary>
        public float MasterVolume { get { return master_volume; } set { master_volume = value; } }
        /// <summary>BGM音量</summary>
        public float BGMVolume { get { return bgm_volume; } set { bgm_volume = value; } }
        /// <summary>SE音量</summary>
        public float SEVolume { get { return se_volume; } set { se_volume = value; } }
        /// <summary>解像度のID</summary>
        public int ScreenResolutionID { get { return screen_resolution_id; } set { screen_resolution_id = value; } }
        /// <summary>フルスクリーンかどうか</summary>
        public bool IsFullScreen { get { return isFullScreen; } set { isFullScreen = value; } }

        public SettingsData()
        {
            master_volume = 0.5f;
            bgm_volume = 1;
            se_volume = 1;
            screen_resolution_id = 0;
            isFullScreen = true;
        }

        public SettingsData(int master_volume, int bgm_volume, int se_volume, int screen_resolution_id, bool isFullScreen)
        {
            this.master_volume = master_volume;
            this.bgm_volume = bgm_volume;
            this.se_volume = se_volume;
            this.screen_resolution_id = screen_resolution_id;
            this.isFullScreen = isFullScreen;
        }
    }
}