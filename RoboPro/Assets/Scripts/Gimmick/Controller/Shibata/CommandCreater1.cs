using Command.Entity;

namespace Command
{
    /// <summary>
    /// コマンド生成クラス
    /// </summary>
    public class CommandCreater1
    {
        /// <summary>
        /// コマンド構造体を元にコマンドクラスを生成します
        /// </summary>
        /// <param name="status">生成元のコマンド構造体</param>
        /// <returns>生成したコマンド構造体</returns>
        public static MainCommand CreateCommand(CommandContainer status)
        {
            MainCommand command = new MainCommand();  // メインコマンドのローカル変数を作成

            // コマンドタイプを元にコマンドを作成
            switch (status.commandType)
            {
                case MainCommandType.Move: command = new MoveCommand(status); break;
                case MainCommandType.Rotate: command = new RotateCommand(status); break;
                case MainCommandType.Scale: command = new ScaleCommand(status); break;
            }

            return command; // 作成したコマンドを返す
        }
    }
}