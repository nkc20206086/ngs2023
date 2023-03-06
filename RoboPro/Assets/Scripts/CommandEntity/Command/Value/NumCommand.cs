using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// 数値コマンドクラス
    /// </summary>
    public class NumCommand : CommandBase
    {
        private int num;    // このクラスの持つ数値

        /// <summary>
        /// コンストラクタ(変数はコンストラクタでのみ設定可能です)
        /// </summary>
        /// <param name="num">設定する数値</param>
        public NumCommand(int num)
        {
            this.num = num;
        }

        public override string GetString()
        {
            return num.ToString();
        }

        public override void StartUp() { }

        public override CommandType ConfirmationCommandType()
        {
            return CommandType.Num;
        }

        /// <summary>
        /// このクラスの持つ数値を返す
        /// </summary>
        public int numGet
        {
            get => num;
        }
    }
}