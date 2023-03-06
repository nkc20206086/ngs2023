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
        protected bool lockNumber = false;
        protected bool lockCoordinateAxis = false;

        protected string commandName;            // コマンドの名称
        public NumCommand num;       // 数値を持ったコマンドクラス
        public AxisCommand axis;     // 軸を持ったコマンドクラス

        protected int num_local;                // コマンドが用いる数値
        protected CoordinateAxis axis_local;    // コマンドが用いる軸

        protected int capacity;                 // コマンドの容量

        protected Action completeAction;        // コマンド完了時に実行するアクションを保存する変数

        /// <summary>
        /// コンストラクタ 数値直接設定用
        /// </summary>
        /// <param name="lock_m">コマンドを変更可能かどうか</param>
        /// <param name="lock_n">数値を変更可能かどうか</param>
        /// <param name="lock_c">軸を変更可能かどうか</param>
        /// <param name="commandName">コマンドの名称</param>
        /// <param name="num">数値に用いる値</param>
        /// <param name="axis">軸に用いる値</param>
        /// <param name="capacity">このコマンドが要する容量</param>
        public MainCommand(bool lock_m,bool lock_n,bool lock_c,
                          string commandName,int num,int axis,int capacity) 
        {
            lockMenber = lock_m;
            lockNumber = lock_n;
            lockCoordinateAxis = lock_c;
            this.commandName = commandName;
            this.num = new NumCommand(num);
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
            lockNumber = status.lockNumber;
            lockCoordinateAxis = status.lockCoordinateAxis;
            commandName = status.type.ToString();
            num = new NumCommand(status.num);
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
        /// コマンド実行開始時に実行すべき関数
        /// </summary>
        /// <param name="completeAction">コマンド要件完了時に実行するアクション</param>
        /// <param name="obj">コマンドによる変更を反映するオブジェクト</param>
        public virtual void ActionActivate(Action completeAction,GameObject obj) 
        {
            this.completeAction = completeAction;
        }

        /// <summary>
        /// コマンド実行関数
        /// </summary>
        /// <param name="state">どういった状況で動かすか</param>
        /// <param name="targetTransform">対象の各種値変更用transform</param>
        public virtual void CommandExecute(CommandState state, Transform targetTransform) { }

        /// <summary>
        /// 方向を取得する関数
        /// </summary>
        /// <returns></returns>
        protected Vector3 GetVec()
        {
            Vector3 returnVec = Vector3.zero;
            switch (axis_local)
            {
                case CoordinateAxis.X: returnVec = Vector3.right; break;
                case CoordinateAxis.Y: returnVec = Vector3.up; break;
                case CoordinateAxis.Z: returnVec = Vector3.forward; break;
            }

            if (num_local < 0) returnVec *= -1;

            return returnVec;
        }

        /// <summary>
        /// コマンドの名称を取得する関数
        /// </summary>
        /// <returns></returns>
        public string NameGet()
        {
            return commandName;
        }

        /// <summary>
        /// 数値のコマンドテキストを取得する関数
        /// </summary>
        /// <returns></returns>
        public string NumTextGet()
        {
            return num != null ? num.GetString() : default;
        }

        /// <summary>
        /// 軸のコマンドテキストを取得する関数
        /// </summary>
        /// <returns></returns>
        public string AxisTextGet()
        {
            return axis != null ? axis.GetString() : default;
        }

        /// <summary>
        /// コマンドクラスがnullでないかを確認する関数
        /// </summary>
        /// <returns></returns>
        public bool CommandNullCheck()
        {
            return num != null && axis != null;
        }

        public override string GetString()
        {
            return $"{commandName} {(axis != null ? axis.GetString() : default)} {(num != null ? num.GetString() : default)}";
        }

        public override void StartUp()
        {
            num_local = num.numGet;
            axis_local = axis.axisGet;
        }

        public override CommandType ConfirmationCommandType()
        {
            return CommandType.Command;
        }

    }
}