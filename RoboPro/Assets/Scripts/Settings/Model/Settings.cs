﻿using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace Robo
{
    public class Settings : ISettings
    {
        public const string SAVE_DIRECTORY_PATH = "Save";
        public const string SAVE_FILE_PATH = "option.json";

        private string saveDirectoryPath => Application.dataPath + "/" + SAVE_DIRECTORY_PATH;
        private string savePath => Application.dataPath + "/" + SAVE_DIRECTORY_PATH + "/" + SAVE_FILE_PATH;

        private SettingsData data;

        public event Action<IGetSettingsData> OnLoad;

        public IGetSettingsData GetData() => data;

        [Inject]
        public Settings(IAudioSettings audio, IScreenSettings screen)
        {
            audio.OnSetMasterVolume += volume => data.MasterVolume = volume;
            audio.OnSetBGMVolume += volume => data.BGMVolume = volume;
            audio.OnSetSEVolume += volume => data.SEVolume = volume;
            audio.OnGetSettingsData += GetData;

            screen.OnSetResolution += (id) => data.ScreenResolutionID = id;
            screen.OnSetIsFullScreen += (isFullScreen) => data.IsFullScreen = isFullScreen;
            screen.OnGetSettingsData += GetData;

            Load();
        }

        //セーブファイルまでのディレクトリ、またはファイルがなければ生成する
        private void CheckExistsFile()
        {
            if (!Directory.Exists(saveDirectoryPath))
            {
                Directory.CreateDirectory(saveDirectoryPath);
            }
            if (!File.Exists(savePath))
            {
                File.Create(savePath);
            }
        }

        public void Save()
        {
            CheckExistsFile();
            string json = JsonUtility.ToJson(data);
            try
            {
                using (StreamWriter writer = new StreamWriter(savePath))
                {
                    writer.Write(json);
                }
            }
            catch
            {
                Debug.LogError("設定ファイルを保存できませんでした");
            }
        }

        public void Load()
        {
            CheckExistsFile();
            try 
            {
                using (StreamReader reader = new StreamReader(savePath))
                {
                    //Jsonファイルを最後まで読み込む
                    string json = reader.ReadToEnd();

                    SettingsData data = null;
                    //Jsonデータがなければnewする
                    if (string.IsNullOrEmpty(json))
                    {
                        data = new SettingsData();
                    }
                    else
                    {
                        data = JsonUtility.FromJson<SettingsData>(json);
                    }

                    this.data = data;
                }
                OnLoad?.Invoke(data);
            }
            catch
            {
                Debug.LogError("設定ファイルを読み込めませんでした");
            }
        }
    }
}
