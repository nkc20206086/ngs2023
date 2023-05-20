using UnityEngine;
using System;
using Zenject;
using UniRx;
using Command;

namespace Gimmick
{
    public class AccessPoint : MonoBehaviour
    {
        public const float RADIUS = 1.5f;   // 当たり判定の有効範囲の半径

        [SerializeField, Tooltip("紐づけるギミック")]
        private GimmckController gimmckController;

        [Tooltip("APとギミックを繋ぐラインの色")]
        public Color color;

        [SerializeField, Tooltip("使用コマンド1")]
        private CommandStruct usableCommand1;
        [SerializeField, Tooltip("使用コマンド2")]
        private CommandStruct usableCommand2;
        [SerializeField, Tooltip("使用コマンド3")]
        private CommandStruct usableCommand3;
        [SerializeField, Tooltip("スペシャルコマンド")]
        private CommandStruct specialCommand;

        public GimmckController controlGimmick
        {
            get => gimmckController;
        }

        /// <summary>
        /// ギミックを有効化する関数
        /// </summary>
        public void GimmickActivate()
        {
            // ギミックに渡すための配列を作成する
            CommandStruct[] usableCommands = new CommandStruct[CommandUtility.commandCount + 1];
            usableCommands[0] = usableCommand1;
            usableCommands[1] = usableCommand2;
            usableCommands[2] = usableCommand3;
            usableCommands[3] = specialCommand;

            gimmckController.StartUp(usableCommands);   // ギミック管理クラスの開始関数を実行
        }
    }
}