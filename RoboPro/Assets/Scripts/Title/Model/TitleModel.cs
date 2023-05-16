﻿using System;
using UnityEngine;
using Zenject;

namespace Robo
{
    public class TitleModel : ITitleModel
    {
        private event Action OnStart;
        private event Action OnShowSettings;
        private event Action OnExit;

        private IMultiSceneLoader multiSceneLoader;
        private bool isLoadingStageSelect = false;

        [Inject] 
        public TitleModel(IMultiSceneLoader multiSceneLoader)
        {
            this.multiSceneLoader = multiSceneLoader;
        }

        event Action ITitleModel.OnStart
        {
            add => OnStart += value;
            remove => OnStart -= value;
        }

        event Action ITitleModel.OnShowSettings
        {
            add => OnShowSettings += value;
            remove => OnShowSettings -= value;
        }

        event Action ITitleModel.OnExit
        {
            add => OnExit += value;
            remove => OnExit -= value;
        }

        //ゲーム開始
        async void ITitleModel.Start()
        {
            //ステージセレクトシーンを既に読み込んでいる場合、早期リターン
            if (isLoadingStageSelect) return;
            isLoadingStageSelect = true;

            await multiSceneLoader.AddScene(SceneID.StageSelect, true);
            await multiSceneLoader.UnloadScene(SceneID.Title);
            OnStart?.Invoke();
            Debug.Log("Start");
        }

        //設定画面を開く
        void ITitleModel.ShowSettings()
        {
            OnShowSettings?.Invoke();
            Debug.Log("Show");
        }

        //ゲーム終了
        void ITitleModel.Exit()
        {
            OnExit?.Invoke();
            Debug.Log("Exit");
        }
    }
}