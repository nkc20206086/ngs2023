using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Command.Entity
{
    /// <summary>
    /// コマンドの基底クラス
    /// </summary>
    public class MainCommand : CommandBase
    {
        // 各種値が変更可能であるか
        protected bool lockMenber = false;
        protected bool lockValue = false;
        protected bool lockCoordinateAxis = false;

        protected string commandName;            // コマンドの名称
        public ValueCommand value;       // 数値を持ったコマンドクラス
        public AxisCommand axis;     // 軸を持ったコマンドクラス

        protected int usableValue;                // コマンドが用いる数値
        protected CoordinateAxis usableAxis;    // コマンドが用いる軸

        protected int capacity;                 // コマンドの容量

        protected Action completeAction;        // コマンド完了時に実行するアクションを保存する変数

        /// <summary>
        /// コンストラクタ 数値直接設定用
        /// </summary>
        /// <param name="lockMenber">コマンドを変更可能かどうか</param>
        /// <param name="lockValue">数値を変更可能かどうか</param>
        /// <param name="lockCoordinateAxis">軸を変更可能かどうか</param>
        /// <param name="commandName">コマンドの名称</param>
        /// <param name="value">数値に用いる値</param>
        /// <param name="axis">軸に用いる値</param>
        /// <param name="capacity">このコマンドが要する容量</param>
        public MainCommand(bool lockMenber,bool lockValue,bool lockCoordinateAxis,string commandName,int value,int axis,int capacity) 
        {
            this.lockMenber = lockMenber;
            this.lockValue = lockValue;
            this.lockCoordinateAxis = lockCoordinateAxis;
            this.commandName = commandName;
            this.value = new ValueCommand(value);
            this.axis = new AxisCommand((CoordinateAxis)axis);
            this.capacity = capacity;
        }

        /// <summary>
        /// コンストラクタ　構造体設定用
        /// </summary>
        /// <param name="status">設定用構造体</param>
        public MainCommand(CommandStruct status)
        {
            lockMenber = status.lockCommand;
            lockValue = status.lockNumber;
            lockCoordinateAxis = status.lockCoordinateAxis;
            commandName = status.commandType.ToString();
            value = new ValueCommand(status.value);
            axis = new AxisCommand(status.axis);
            capacity = status.capacity;
        }

        /// <summary>
        /// メインコマンド型としてのコピーを返す関数
        /// </summary>
        /// <returns></returns>
        public MainCommand MainCommandClone()
        {
            return (MainCommand)MemberwiseClone();
        }

        /// <summary>
        /// コマンド実行関数
        /// </summary>
        /// <param name="state">どういった状況で動かすか</param>
        /// <param name="targetTransform">対象の各種値変更用transform</param>
        public virtual void CommandExecute(CommandState state, Transform targetTransform) { }

        /// <summary>
        /// 開始時処理
        /// </summary>
        public virtual object InitCommand(object target,Action completeAction)
        {
            usableValue = value.getValue;
            usableAxis = axis.getAxis;
            this.completeAction = completeAction;
            return default;
        }

        /// <summary>
        /// メインコマンドタイプ取得用関数
        /// </summary>
        /// <returns></returns>
        public virtual MainCommandType GetMainCommandType()
        {
            return MainCommandType.None;
        }

        /// <summary>
        /// 方向を取得する関数
        /// </summary>
        /// <returns>このコマンドの持つ方向</returns>
        protected Vector3 GetDirection()
        {
            Vector3 returnVec = Vector3.zero;
            switch (usableAxis)
            {
                case CoordinateAxis.X: returnVec = Vector3.right; break;
                case CoordinateAxis.Y: returnVec = Vector3.up; break;
                case CoordinateAxis.Z: returnVec = Vector3.forward; break;
            }

            if (usableValue < 0) returnVec *= -1;

            return returnVec;
        }

        /// <summary>
        /// コマンドの名称を取得する関数
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return commandName;
        }

        /// <summary>
        /// 数値のコマンドテキストを取得する関数
        /// </summary>
        /// <returns></returns>
        public string GetValueText()
        {
            return value != null ? value.GetString() : default;
        }

        /// <summary>
        /// 軸のコマンドテキストを取得する関数
        /// </summary>
        /// <returns></returns>
        public string GetAxisText()
        {
            return axis != null ? axis.GetString() : default;
        }

        /// <summary>
        /// コマンドクラスがnullでないかを確認する関数
        /// </summary>
        /// <returns></returns>
        public bool CommandNullCheck()
        {
            return value != null && axis != null;
        }

        public override string GetString()
        {
            return $"{commandName} {(axis != null ? axis.GetString() : default)} {(value != null ? value.GetString() : default)}";
        }

        public override CommandType GetCommandType()
        {
            return CommandType.Command;
        }

    }
}