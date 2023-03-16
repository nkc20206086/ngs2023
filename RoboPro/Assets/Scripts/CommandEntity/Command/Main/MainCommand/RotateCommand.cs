using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// 回転を行うコマンドクラス
    /// </summary>
    public class RotateCommand : MainCommand
    {
        private Vector3 baseAngle;      // 実行前の回転値
        private Quaternion baseQuat;    // 実行前の回転量

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
        public RotateCommand(bool lock_m, bool lock_n, bool lock_c,
                          string methodName, int num, int axis, int capacity) : base(lock_m, lock_n, lock_c, methodName, num, axis, capacity) { }

        /// <summary>
        /// コンストラクタ　構造体設定用
        /// </summary>
        /// <param name="status">設定用構造体</param>
        public RotateCommand(CommandStruct status) : base(status) { }

        public override void ActionActivate(Action completeAction, GameObject obj)
        {
            base.ActionActivate(completeAction,obj);
            baseAngle = obj.transform.eulerAngles;
            baseQuat = obj.transform.rotation;
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                     // 状況が無効化であれば実行しない

            int mag = state == CommandState.MOVE_ON ? 1 : -1;                                               // 動かす倍率(逆再生であれば-1をかけ動かす方向を反転するため)

            if (Mathf.Abs(Quaternion.Angle(baseQuat, targetTransform.rotation)) > Mathf.Abs(usableValue))     // 指定された角度分回転していれば
            {
                targetTransform.eulerAngles = baseAngle + (GetDirection() * Mathf.Abs(usableValue) * mag) ;         // eulerAngleを初期回転値に回転を反映した値にする
                completeAction?.Invoke();                                                                   // コマンド完了時アクションを実行する
            }
            else
            {
                targetTransform.Rotate(GetDirection() * mag);                                                     // 方向に倍率を反映した値回転する
            }
        }
    }
}