using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// 軸コマンドクラス
    /// </summary>
    public class AxisCommand : CommandBase
    {
        private CoordinateAxis axis;    // このクラスの持つ軸

        /// <summary>
        /// コンストラクタ(変数はコンストラクタでのみ設定可能です)
        /// </summary>
        /// <param name="axis">設定する軸</param>
        public AxisCommand(CoordinateAxis axis)
        {
            this.axis = axis;
        }

        public override string GetString()
        {
            return axis.ToString(); // enumを文字列化したものを返す
        }

        public override CommandType ConfirmCommandType()
        {
            return CommandType.Axis;
        }

        /// <summary>
        /// このクラスの持つ軸を返す
        /// </summary>
        public CoordinateAxis getAxis
        {
            get => axis;
        }
    }
}