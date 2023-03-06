using UnityEngine;
using System;
using Zenject;
using UniRx;

namespace Command
{
    public class AccessPoint : MonoBehaviour
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

        // Update is called once per frame
        void Update()
        {
            if (!updatePlay) return;

            // これらの処理はデバッグ用なので、実際に用いる場合は変更すること

            if (Input.GetKeyDown(openKeyCode))
            {
                openAct.OnNext(index);
            }

            if (Input.GetKeyDown(closeKeyCode))
            {
                closeAct.OnNext(default);
            }
        }
    }
}