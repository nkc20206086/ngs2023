using System;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// 拡大を行うコマンドクラス
    /// </summary>
    public class ScaleCommand : MainCommand
    {
        private const float TOLERANCE = 0.3f;

        private Vector3 baseScale;      // 元々の尺度
        private Vector3 targetScale;    // 目標の尺度

        /// <summary>
        /// コンストラクタ　構造体設定用
        /// </summary>
        /// <param name="status">設定用構造体</param>
        public ScaleCommand(CommandContainer status) : base(status) { }

        public override object InitCommand(object target, Action completeAction)
        {
            // 各項目を現在の状態で再設定
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            baseScale = (Vector3)target;                                                    // 元々の尺度を引数から取得する

            targetScale = baseScale + GetDirection() * (Mathf.Abs(value.getValue) * 0.1f);  // 目標の尺度を計算して保存

            // 目標の尺度の1未満の数字を1にする
            targetScale = new Vector3(targetScale.x < 1.0f ? 1.0f : targetScale.x, targetScale.y < 1.0f ? 1.0f : targetScale.y, targetScale.z < 1.0f ? 1.0f : targetScale.z);

            return targetScale;                                                             // 目標の尺度を返す
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                     // 無効化ステートを送信されているなら早期リターンする

            if (state == CommandState.MOVE_ON)                                                              // 通常処理であるなら
            {
                if (usableValue > 0)                                                                        // コマンドの持つ値が正の数なら
                {
                    if (Vector3.Distance(targetTransform.localScale,baseScale) >= (usableValue * 0.1f))     // 拡大値分拡大しているなら
                    {
                        targetTransform.localScale = targetScale;                                           // 尺度を目標の尺度にする
                        completeAction?.Invoke();                                                           // コマンド完了時処理を実行
                    }
                    else
                    {
                        targetTransform.localScale += GetDirection() * Time.deltaTime;                      // 徐々に拡大する
                    }
                }
                else                                                                                        // コマンドの持つ値が負の数なら
                {
                    if (Mathf.Abs(Vector3.Distance(targetTransform.localScale, targetScale)) <= TOLERANCE)  // 目標の尺度に近づいたら
                    {
                        targetTransform.localScale = targetScale;                                           // 尺度を目標の尺度にする
                        completeAction?.Invoke();                                                           // コマンド完了時処理を実行
                    }
                    else
                    {
                        targetTransform.localScale += GetDirection() * Time.deltaTime;                      // 徐々に縮小する(方向がマイナスであるため加算)
                    }
                }
            }
            else　                                                                                          // 逆処理であるなら
            {
                if (usableValue > 0)                                                                        // コマンドの持つ値が正の数なら
                {
                    if (Vector3.Distance(targetTransform.localScale,baseScale) <= TOLERANCE)                // 元々の尺度に近づいたら
                    {
                        targetTransform.localScale = baseScale;                                             // 元々の尺度にする
                        completeAction?.Invoke();                                                           // コマンド完了時処理を実行
                    }
                    else
                    {
                        targetTransform.localScale -= GetDirection() * Time.deltaTime;                      // 徐々に縮小する
                    }
                }
                else
                {
                    if (Mathf.Abs(Vector3.Distance(targetTransform.localScale, baseScale)) <= TOLERANCE)    // 元々の尺度に近づいたら
                    {
                        targetTransform.localScale = baseScale;                                             // 尺度を元々の尺度にする
                        completeAction?.Invoke();                                                           // コマンド完了時処理を実行
                    }
                    else                                                                            
                    {
                        targetTransform.localScale -= GetDirection() * Time.deltaTime;                      // 徐々に拡大する(方向がマイナスであるため減算)
                    }
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Scale;
        }

        public override string GetString()
        {
            return "拡大";
        }
    }
}