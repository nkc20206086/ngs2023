using UnityEngine;
using System;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// セーブ情報クラス
    /// </summary>
    public class SaveArchive
    {
        public int index { get; private set; }                  // このクラスが持つインデックス情報
        public Transform saveTransform { get; private set; }    // 対象の座標値等

        /// <summary>
        /// コンストラクタ(インデックス、トランスフォーム設定用)
        /// </summary>
        /// <param name="index">保存インデックス</param>
        /// <param name="transform">保存トランスフォーム</param>
        public SaveArchive(int index, Transform transform)
        {
            // 各値を設定する
            this.index = index;
            saveTransform = transform;
        }
    }
}