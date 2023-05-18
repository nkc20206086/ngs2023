using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Command;

namespace Command.Entity
{
    /// <summary>
    /// コマンドのベースクラス
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// コマンドを文字列に変換したものを返す
        /// </summary>
        /// <returns>変換後の文字列</returns>
        public abstract string GetString();

        /// <summary>
        /// コマンドタイプを取得する関数
        /// </summary>
        /// <returns></returns>
        public abstract CommandType GetCommandType();

        /// <summary>
        /// コマンドベース型としてのコピーを返すクラス
        /// </summary>
        /// <returns></returns>
        public CommandBase BaseClone()
        {
            return (CommandBase)MemberwiseClone();
        }
    }
}