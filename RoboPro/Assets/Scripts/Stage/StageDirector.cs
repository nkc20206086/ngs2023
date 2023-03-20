using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmick;
using Command;

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
            List<CommandStruct[]> setCommands = new List<CommandStruct[]>();

            for (int i = 0;i < 2;i++)
            {
                // 初期コマンド設定
                setCommands.Add(new CommandStruct[3]);
                setCommands[i][0] = new CommandStruct(MainCommandType.Move,false,false,false,30,CoordinateAxis.X,1);
                setCommands[i][1] = new CommandStruct(MainCommandType.Rotate,false,false,false,90,CoordinateAxis.Z,1);
                setCommands[i][2] = new CommandStruct(MainCommandType.Move,false,false,false,30,CoordinateAxis.Y,1);
            }

            gimmickDirector.GimmickInstance(setCommands);  // ギミック管理クラスにギミック生成を依頼
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