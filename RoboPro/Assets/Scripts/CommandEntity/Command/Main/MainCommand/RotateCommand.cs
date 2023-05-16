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
        private Quaternion baseQuat;    // 実行前の回転量

        /// <summary>
        /// コンストラクタ　構造体設定用
        /// </summary>
        /// <param name="status">設定用構造体</param>
        public RotateCommand(CommandStruct status) : base(status) { }

        public override object StartUp(object target,Action completeAction)
        {
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            Quaternion quaternion = (Quaternion)target;
            baseQuat = quaternion;

            return quaternion * Quaternion.Euler(GetDirection() * value.getValue);
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                        // 状況が無効化であれば実行しない

            if (state == CommandState.MOVE_ON)
            {
                if (Mathf.Abs(Quaternion.Angle(baseQuat, targetTransform.rotation)) > Mathf.Abs(usableValue))  // 指定された角度分回転していれば
                {
                    targetTransform.rotation = baseQuat * Quaternion.Euler(GetDirection() * value.getValue);   // eulerAngleを初期回転値に回転を反映した値にする
                    completeAction?.Invoke();                                                                  // コマンド完了時アクションを実行する
                }
                else
                {
                    targetTransform.Rotate(GetDirection());
                }
            }
            else if (state == CommandState.RETURN)
            {
                if (Mathf.Abs(Quaternion.Angle(baseQuat, targetTransform.rotation)) < 1)
                {
                    targetTransform.rotation = baseQuat;                                                        // eulerAngleを初期回転値に回転を反映した値にする
                    completeAction?.Invoke();                                                                   // コマンド完了時アクションを実行する
                }
                else
                {
                    targetTransform.Rotate(GetDirection() * -1);
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Rotate;
        }
    }
}