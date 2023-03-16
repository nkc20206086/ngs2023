using UnityEngine;
using System;
using Zenject;
using UniRx;

namespace Command
{
    public class AccessPoint : MonoBehaviour,IGimmickAccess
    {
        [Header("デバッグ用")]
        [SerializeField]
        private KeyCode openKeyCode = KeyCode.Return;
        [SerializeField]
        private KeyCode closeKeyCode = KeyCode.Escape;

        [Header("値確認用　数値変更非推奨")]
        public int index;
        public bool updatePlay = true;

        public IObserver<int> openAct;      // コマンド入れ替え実行用アクション
        public IObserver<Unit> closeAct;    // コマンド入れ替え終了用アクション

        void IGimmickAccess.GimmickAccess()
        {
            openAct.OnNext(index);
        }

        void IGimmickAccess.RemoveAccess()
        {
            closeAct.OnNext(default);
        }
    }
}