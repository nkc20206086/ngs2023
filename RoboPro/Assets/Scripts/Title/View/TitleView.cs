using System;
using UnityEngine;

namespace Robo
{
    public class TitleView : MonoBehaviour, ITitleView
    {
        [SerializeField] private AnimatedButton startButton;   //ゲーム開始ボタン
        [SerializeField] private AnimatedButton settingsButton;//設定ボタン
        [SerializeField] private AnimatedButton exitButton;    //ゲーム終了ボタン

        public event Action OnClickStartButton;
        public event Action OnClickSettingsButton;
        public event Action OnClickExitButton;

        private void Start()
        {
            //ボタンがクリックされたときに呼ばれるイベントをボタンに追加
            startButton.OnClick += OnClickStartButton;
            settingsButton.OnClick += OnClickSettingsButton;
            exitButton.OnClick += OnClickExitButton;
        }
    }
}