using UnityEngine;
using System;
using Command.Entity;

namespace Command
{
    // コマンド関連の処理を管理するクラス
    public class CommandDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("コマンド入れ替えクラス")]
        private CommandSwapManager commandSwapManager;

        [SerializeField, Tooltip("")]
        private CommandStorage commandStorage;

        public event Action showUI;
        public event Action hideUI;

        public event Action<MainCommand[]> swapUI_MainCommand;
        public event Action<CommandBase[]> swapUI_Storage;

        /// <summary>
        /// コマンド入れ替えを有効化する
        /// </summary>
        /// <param name="mainCommands">入れ替え対象のコマンド配列</param>
        /// <param name="item">セーブ情報クラス</param>
        public void CommandActivation(MainCommand[] mainCommands)
        {
            commandSwapManager.action += SetCommandText;
            // 入れ替えクラスを有効化する
            commandSwapManager.SwapActivation(mainCommands);
            SetCommandText(mainCommands);

            showUI?.Invoke();
        }

        /// <summary>
        /// コマンド入れ替えを無効化する
        /// </summary>
        /// <returns>変更が行われたか</returns>
        public bool CommandInvalidation()
        {
            // 入れ替えクラスを無効化し、変更の有無を受け取り送信
            bool retValue = commandSwapManager.SwapInvalidation();
            hideUI?.Invoke();
            return retValue;
        }

        public Action<int,int> GetMainCommandIndexSet()
        {
            return commandSwapManager.SetMainCommandIndex;
        }

        public Action<int,int> GetStorageIndexSet()
        {
            return commandSwapManager.SetStorageIndex;
        }

        private void SetCommandText(MainCommand[] mainCommands)
        {
            swapUI_MainCommand?.Invoke(mainCommands);
            swapUI_Storage?.Invoke(commandStorage.controlCommand);
        }
    }
}