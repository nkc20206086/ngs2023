using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// 数値コマンドクラス
    /// </summary>
    public class ValueCommand : CommandBase
    {
        private int value;    // このクラスの持つ数値

        /// <summary>
        /// コンストラクタ(変数はコンストラクタでのみ設定可能です)
        /// </summary>
        /// <param name="value">設定する数値</param>
        public ValueCommand(int value)
        {
            this.value = value;
        }

        public override string GetString()
        {
            return value.ToString();
        }

        public override CommandType ConfirmCommandType()
        {
            return CommandType.Value;
        }

        /// <summary>
        /// このクラスの持つ数値を返す
        /// </summary>
        public int valueGet
        {
            get => value;
        }
    }
}