using UnityEngine;

namespace Gimmick.Interface
{
    public interface IGimmickAccess
    {
        /// <summary>
        /// 有効範囲にいるアクセスポイントのインデックスを返します
        /// </summary>
        /// <param name="position">対象の位置</param>
        /// <returns>実行結果(-1の場合は有効なものはありません)</returns>
        public int GetAccessPointIndex(Vector3 position);

        /// <summary>
        /// コマンド入れ替え処理実行
        /// </summary>
        /// <param name="index">実行インデックス</param>
        public void Access(int index);
    }
}