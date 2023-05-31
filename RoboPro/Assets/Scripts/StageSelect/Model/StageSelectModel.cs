using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Robo
{
    public class StageSelectModel : IStageSelectModel
    {
        public const string SAVE_DIRECTORY_PATH = "Save";
        public const string SAVE_FILE_PATH = "GameData.json";

        private string saveDirectoryPath => Application.dataPath + "/" + SAVE_DIRECTORY_PATH;
        private string savePath => Application.dataPath + "/" + SAVE_DIRECTORY_PATH + "/" + SAVE_FILE_PATH;

        private event Action<StageSelectModelArgs, StageSelectSaveData> OnInitalize;
        private event Action<int> OnSelect;
        private event Action<int> OnCannotSelect;
        private event Action OnPlay;

        private int nowSelectedIndex;
        private List<StageSelectElementInfo> infos = new List<StageSelectElementInfo>();
        private StageSelectSaveData saveData;

        event Action<StageSelectModelArgs, StageSelectSaveData> IStageSelectModel.OnInitalize
        {
            add => OnInitalize += value;
            remove => OnInitalize -= value;
        }

        event Action<int> IStageSelectModel.OnSelect
        {
            add => OnSelect += value;
            remove => OnSelect -= value;
        }

        event Action<int> IStageSelectModel.OnSelectError
        {
            add => OnCannotSelect += value;
            remove => OnCannotSelect -= value;
        }

        event Action IStageSelectModel.OnPlay
        {
            add => OnPlay += value;
            remove => OnPlay -= value;
        }

        //初期化
        void IStageSelectModel.Initalize(StageSelectModelArgs args)
        {
            infos.AddRange(args.Infos);
            ((IStageSelectModel)this).LoadSaveData();
            OnInitalize?.Invoke(args, saveData);
            
            //最後にプレイしたステージを選択、始めてプレイする場合は最初のステージを選択
            if(string.IsNullOrEmpty(saveData.LastPlayedStage))
            {
                ((IStageSelectModel)this).Select(0);
            }
            else
            {
                ((IStageSelectModel)this).Select(saveData.LastPlayedStage);
            }
        }

        //ステージを選択
        void IStageSelectModel.Select(int index)
        {
            if (index >= infos.Count || index < 0)
            {
                OnCannotSelect?.Invoke(index);
                return;
            }

            nowSelectedIndex = index;
            OnSelect?.Invoke(nowSelectedIndex);
        }

        void IStageSelectModel.Select(string stageNumber)
        {
            int index = infos.FindIndex(x => x.StageNumber == stageNumber);
            ((IStageSelectModel)this).Select(index);
        }

        //次のステージを選択
        void IStageSelectModel.SelectNext()
        {
            StageSelectElementSaveData saveElement = saveData.GetSaveData(infos[nowSelectedIndex].StageNumber);
            //現在のステージをクリアしていない場合、次へ進めない
            if(saveElement != null)
            {
                if (saveElement.IsClear)
                {
                    ((IStageSelectModel)this).Select(nowSelectedIndex + 1);
                }
                else
                {
                    OnCannotSelect?.Invoke(nowSelectedIndex + 1);
                    return;
                }
            }
        }

        //前のステージを選択
        void IStageSelectModel.SelectPrevious()
        {
            ((IStageSelectModel)this).Select(nowSelectedIndex - 1);
        }

        //ステージをプレイ
        void IStageSelectModel.Play()
        {
            Debug.Log(infos[nowSelectedIndex].StageName + "をプレイ");

            saveData.OnPlayStage(infos[nowSelectedIndex].StageNumber);

            OnPlay?.Invoke();
        }

        //クリア
        void IStageSelectModel.Clear(string stageNumber)
        {
            saveData.OnClearStage(stageNumber);
        }

        //ゲームデータをセーブ
        void IStageSelectModel.Save()
        {
            CheckExistsFile();
            string json = JsonUtility.ToJson(saveData);
            try
            {
                using (StreamWriter writer = new StreamWriter(savePath))
                {
                    writer.Write(json);
                }
            }
            catch
            {
                Debug.LogError("ゲームデータを保存できませんでした");
            }
        }

        //ゲームデータをロード
        void IStageSelectModel.LoadSaveData()
        {
            CheckExistsFile();
            try
            {
                using (StreamReader reader = new StreamReader(savePath))
                {
                    //Jsonファイルを最後まで読み込む
                    string json = reader.ReadToEnd();

                    StageSelectSaveData data = null;
                    //Jsonデータがなければnewする
                    if (string.IsNullOrEmpty(json))
                    {
                        data = new StageSelectSaveData();
                    }
                    else
                    {
                        data = JsonUtility.FromJson<StageSelectSaveData>(json);
                    }

                    this.saveData = data;
                }
            }
            catch
            {
                Debug.Log("ゲームデータを読み込めませんでした");
                saveData = new StageSelectSaveData();
            }
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
    }
}