using UnityEngine;

namespace Command
{
    /// <summary>
    /// コマンド生成時に利用する構造体
    /// </summary>
    [System.Serializable]
    public struct CommandStruct
    {
        [SerializeField,Tooltip("このコマンドがどの種類であるか")]
        private MainCommandType CommandType;
        [SerializeField,Tooltip("コマンドを移動可能であるか")]
        private bool LockCommand;
        [SerializeField, Tooltip("コマンドで使用する数値")]
        private int Value;
        [SerializeField,Tooltip("コマンド内の数値を移動可能であるか")]
        private bool LockNumber;
        [SerializeField, Tooltip("コマンドで使用する軸")]
        private CoordinateAxis Axis;
        [SerializeField,Tooltip("コマンド内の軸を移動可能であるか")]
        private bool LockCoordinateAxis;

        // 各種ゲットプロパティ
        public MainCommandType commandType { get => CommandType; } 
        public bool lockCommand { get => LockCommand; }
        public bool lockNumber { get => LockNumber; }
        public bool lockCoordinateAxis { get => LockCoordinateAxis; }
        public int value { get => Value; }
        public CoordinateAxis axis { get => Axis; }

        /// <summary>
        /// コンストラクタ(コンストラクタによる引数でのみ変数を変更できます)
        /// </summary>
        /// <param name="commandType">メインコマンドタイプ</param>
        /// <param name="lockCommand">コマンドが変更可能かどうか</param>
        /// <param name="lockNumber">数値が変更可能かどうか</param>
        /// <param name="lockCoordinateAxis">軸を変更可能かどうか</param>
        /// <param name="num">用いる数値</param>
        /// <param name="axis">用いる軸</param>
        public CommandStruct(MainCommandType commandType,
            bool lockCommand,bool lockNumber,bool lockCoordinateAxis,
            int num,CoordinateAxis axis,int capacity)
        {
            CommandType = commandType;
            LockCommand = lockCommand;
            LockNumber = lockNumber;
            LockCoordinateAxis = lockCoordinateAxis;
            Value = num;
            Axis = axis;
        }
    }

}