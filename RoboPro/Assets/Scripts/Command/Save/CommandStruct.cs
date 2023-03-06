namespace Command
{
    /// <summary>
    /// コマンド生成時に利用する構造体
    /// </summary>
    public struct CommandStruct
    {
        public MainCommandType type { get; private set; }       // メインコマンドタイプ

        public bool lockCommand { get; private set; }           // コマンドが変更可能かどうか
        public bool lockNumber { get; private set; }            // 数値が変更可能かどうか
        public bool lockCoordinateAxis { get; private set; }    // 軸を変更可能かどうか
        public int num { get; private set; }                    // 用いる数値
        public CoordinateAxis axis { get; private set; }        // 用いる軸
        public int capacity { get; private set; }               // このコマンドの容量

        /// <summary>
        /// コンストラクタ(コンストラクタによる引数でのみ変数を変更できます)
        /// </summary>
        /// <param name="type">メインコマンドタイプ</param>
        /// <param name="lockCommand">コマンドが変更可能かどうか</param>
        /// <param name="lockNumber">数値が変更可能かどうか</param>
        /// <param name="lockCoordinateAxis">軸を変更可能かどうか</param>
        /// <param name="num">用いる数値</param>
        /// <param name="axis">用いる軸</param>
        /// <param name="capacity">このコマンドの容量</param>
        public CommandStruct(MainCommandType type,
            bool lockCommand,bool lockNumber,bool lockCoordinateAxis,
            int num,CoordinateAxis axis,int capacity)
        {
            this.type = type;
            this.lockCommand = lockCommand;
            this.lockNumber = lockNumber;
            this.lockCoordinateAxis = lockCoordinateAxis;
            this.num = num;
            this.axis = axis;
            this.capacity = capacity;
        }
    }

}