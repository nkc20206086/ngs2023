using System;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// 移動を行うコマンドクラス
    /// </summary>
    public class MoveCommand : MainCommand
    {
        private Vector3 basePos;    // 移動前の座標

        /// <summary>
        /// コンストラクタ　構造体設定用
        /// </summary>
        /// <param name="status">設定用構造体</param>
        public MoveCommand(CommandContainer status) : base(status) { }

        public override object InitCommand(object target, Action completeAction)
        {
            // 各項目を現在の状態で再設定
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            basePos = (Vector3)target;                          // 引数から座標を取得し、移動前座標に保存する
            return GetDirection() * Mathf.Abs(value.getValue);  // 移動量を返す
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                             // 無効化ステートを送信されているなら早期リターンする

            if (state == CommandState.MOVE_ON)
            {
                if (Vector3.Distance(basePos, targetTransform.position) > Mathf.Abs(usableValue))   // 原点からの移動距離が設定数値を超えているなら
                {
                    targetTransform.position = basePos + (GetDirection() * Mathf.Abs(usableValue)); // 対象の位置を対象の座標に変更
                    completeAction?.Invoke();                                                       // コマンド完了時処理を実行
                }
                else                                                                                // まだ移動距離が設定数値を超えていないなら
                {
                    targetTransform.position += GetDirection();                                     // 座標値を倍率を反映した数値分移動する
                }
            }
            else
            {
                if (Vector3.Distance(basePos, targetTransform.position) < 1)                        // 原点からの移動距離が設定数値を超えているなら
                {
                    targetTransform.position = basePos;                                             // 対象の位置を対象の座標に変更
                    completeAction?.Invoke();                                                       // コマンド完了時処理を実行
                }
                else                                                                                // まだ移動距離が設定数値を超えていないなら
                {
                    targetTransform.position += GetDirection() * -1;                                // 座標値を倍率を反映した数値分移動する
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Move;
        }
    }
}

