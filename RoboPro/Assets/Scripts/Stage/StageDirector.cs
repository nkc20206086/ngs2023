using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmick;

namespace Stage
{
    /// <summary>
    /// ステージ関連の処理を管理するクラス(現在は特に記述することがない)
    /// </summary>
    public class StageDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("ギミック管理クラス")]
        private GimmickDirector gimmickDirector;

        // Start is called before the first frame update
        void Start()
        {
            gimmickDirector.GimmickInstance();  // ギミック管理クラスにギミック生成を依頼
        }

        // Update is called once per frame
        void Update()
        {
            // これらの処理はデバッグ用なので、実際に用いる場合は変更すること

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gimmickDirector.Undo();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gimmickDirector.Redo();
            }
        }
    }

}