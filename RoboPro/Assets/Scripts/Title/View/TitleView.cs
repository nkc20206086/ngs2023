using System;
using UnityEngine;

namespace Robo
{
    public class TitleView : MonoBehaviour, ITitleView
    {
        [SerializeField] private AnimatedButton startButton;   //ゲーム開始ボタン
        [SerializeField] private AnimatedButton settingsButton;//設定ボタン
        [SerializeField] private AnimatedButton exitButton;    //ゲーム終了ボタン

        private event Action OnClickStartButton;
        private event Action OnClickSettingsButton;
        private event Action OnClickExitButton;

        event Action ITitleView.OnClickStartButton
        {
            add => OnClickStartButton += value;
            remove => OnClickStartButton -= value;
        }

        event Action ITitleView.OnClickSettingsButton
        {
            add => OnClickSettingsButton += value;
            remove => OnClickSettingsButton -= value;
        }

        event Action ITitleView.OnClickExitButton
        {
            add => OnClickExitButton += value;
            remove => OnClickExitButton -= value;
        }

        private void Start()
        {
            //ボタンがクリックされたときに呼ばれるイベントをボタンに追加
            startButton.OnClick += OnClickStartButton;
            settingsButton.OnClick += OnClickSettingsButton;
            exitButton.OnClick += OnClickExitButton;
        }
    }
}