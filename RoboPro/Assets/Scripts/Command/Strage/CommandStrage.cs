using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Command.Entity
{
    /// <summary>
    /// ストレージコマンドを管理するクラス
    /// </summary>
    public class CommandStrage : MonoBehaviour
    {
        [SerializeField,Tooltip("ストレージコマンドの数")]
        private int strageCount = 0;

        public CommandBase[] bases; // 管理しているストレージコマンド

        private void Awake()
        {
            bases = new CommandBase[strageCount];   // 設定された数でストレージコマンド配列を初期化
        }
    }
}