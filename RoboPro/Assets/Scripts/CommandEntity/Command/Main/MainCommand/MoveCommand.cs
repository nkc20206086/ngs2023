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
        /// コンストラクタ 数値直接設定用
        /// </summary>
        /// <param name="lock_m">コマンドを変更可能かどうか</param>
        /// <param name="lock_n">数値を変更可能かどうか</param>
        /// <param name="lock_c">軸を変更可能かどうか</param>
        /// <param name="methodName">コマンドの名称</param>
        /// <param name="num">数値に用いる値</param>
        /// <param name="axis">軸に用いる値</param>
        /// <param name="capacity">このコマンドが要する容量</param>
        public MoveCommand(bool lock_m, bool lock_n, bool lock_c,string methodName, int num, int axis, int capacity) 
             : base(lock_m, lock_n, lock_c, methodName, num, axis, capacity) { }

        /// <summary>
        /// コンストラクタ　構造体設定用
        /// </summary>
        /// <param name="status">設定用構造体</param>
        public MoveCommand(CommandStruct status) : base(status) { }

        public override void ActionActivate(Action completeAction, GameObject obj)
        {
            base.ActionActivate(completeAction,obj);
            basePos = obj.transform.position;
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                         // 無効化ステートを送信されているなら早期リターンする

            int mag = state == CommandState.MOVE_ON ? 1 : -1;                                   // 動く方向によって倍率を設定する

            if (Vector3.Distance(basePos, targetTransform.position) > Mathf.Abs(usableValue))     // 原点からの移動距離が設定数値を超えているなら
            {
                targetTransform.position = basePos + (GetDirection() * Mathf.Abs(usableValue)) * mag;   // 対象の位置を対象の座標に変更
                completeAction?.Invoke();                                                       // コマンド完了時処理を実行
            }
            else                                                                                // まだ移動距離が設定数値を超えていないなら
            {
                targetTransform.position += GetDirection() * mag;                                     // 座標値を倍率を反映した数値分移動する
            }
        }
    }
}

