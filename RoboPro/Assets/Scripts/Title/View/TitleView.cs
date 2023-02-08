using System;
using UnityEngine;

namespace Robo
{
    public class TitleView : MonoBehaviour, ITitleView
    {
        [SerializeField] private AnimatedButton startButton;
        [SerializeField] private AnimatedButton settingsButton;
        [SerializeField] private AnimatedButton exitButton;

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