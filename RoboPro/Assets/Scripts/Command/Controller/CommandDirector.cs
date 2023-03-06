using UnityEngine;
using Command.Entity;

namespace Command
{
    // コマンド関連の処理を管理するクラス
    public class CommandDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("コマンド入れ替えクラス")]
        private CommandSwitchManager commandSwitchManager;

        /// <summary>
        /// コマンド入れ替えを有効化する
        /// </summary>
        /// <param name="mainCommands">入れ替え対象のコマンド配列</param>
        /// <param name="item">セーブ情報クラス</param>
        public void CommandActivate(MainCommand[] mainCommands, SaveArchive item)
        {
            // 入れ替えクラスを有効化する
            commandSwitchManager.SwitchActivate(mainCommands,item);
        }

        /// <summary>
        /// コマンド入れ替えを無効化する
        /// </summary>
        /// <returns>変更が行われたか</returns>
        public bool CommandInactive()
        {
            // 入れ替えクラスを無効化し、変更の有無を受け取り送信
            bool retValue = commandSwitchManager.TextInactive();
            return retValue;
        }
    }

}