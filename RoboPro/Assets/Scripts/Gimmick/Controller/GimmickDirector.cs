using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Command;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// ギミック関連の処理を管理するクラス
    /// </summary>
    public class GimmickDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("ストレージコマンド管理クラス")]
        private CommandStorage storage;

        [SerializeField,Tooltip("コマンド管理クラス")]
        private CommandDirector commandDirector;

        [Header("デバッグ用　本来であれば生成済みのものは利用しない")]
        [SerializeField]
        private List<GimmckController> instanceGimmick;
        [SerializeField]
        private List<AccessPoint> accessPoints;

        [Header("値確認用　数値変更非推奨")]
        [SerializeField]
        private int archiveIndex = 0;                                       // 現在のセーブ状況の何番目を実行しているかのインデックス(以下セーブ参照インデックスと呼ぶ)
        [SerializeField]
        private int swapIndex = -1;                                         // 入れ替え中のギミックインデックス
        [SerializeField]
        private int maxArchiveCount = 0;                                    // 記録している入れ替えの数

        private bool isPlaySwitch = false;                                  // コマンド入れ替え実行中であるかを管理する変数 

        private void Update()
        {
            if (isPlaySwitch) return;                 // 入れ替え中であれば処理しない

            foreach (GimmckController gimmckController in instanceGimmick)
            {
                gimmckController.CommandUpdate();   // 各ギミックのコマンドを実行する
            }
        }

        /// <summary>
        /// ギミックインスタンス処理
        /// </summary>
        public void GimmickInstance(List<CommandStruct[]> setCommandList)
        {
            // 各要素に入れ替えの開始処理と終了処理を預け、生成インデックスを登録する
            for (int i = 0;i <  instanceGimmick.Count;i++)
            {
                Subject<int> openAct = new Subject<int>();
                openAct.Subscribe(Swap);
                accessPoints[i].openAct = openAct;
                Subject<Unit> closeAct = new Subject<Unit>();
                closeAct.Subscribe(Close);
                accessPoints[i].closeAct = closeAct;

                accessPoints[i].index = i;

                instanceGimmick[i].StartUp(setCommandList[i]);
            }
        }

        /// <summary>
        /// コマンド入れ替え処理実行
        /// </summary>
        /// <param name="index">実行インデックス</param>
        private void Swap(int index)
        {
            if (isPlaySwitch) return;         // 入れ替え実行中であるなら早期リターンする
            isPlaySwitch = true;              // 入れ替え実行中に変更

            swapIndex = index;                // ギミック入れ替えインデックスを設定

            // コマンド管理クラスの入れ替え有効化関数を実行
            commandDirector.CommandActivation(instanceGimmick[index].controlCommand);

            maxArchiveCount++;          // 記録数加算
            archiveIndex++;             // セーブ参照インデックスを加算
        }

        /// <summary>
        /// コマンド入れ替え処理終了
        /// </summary>
        private void Close(Unit unit)
        {
            if (!isPlaySwitch) return;                                                  // 入れ替え実行中でないなら早期リターンする
            isPlaySwitch = false;                                                       // 入れ替え終了に変更

            bool isSwitch = commandDirector.CommandInvalidation();                      // コマンド管理クラスに処理の終了を依頼し、入れ替えの有無をローカル変数に保存する

            if (!isSwitch)                                                              // コマンド入れ替えが行われていないなら
            {
                maxArchiveCount--;                                                      // 記録数減算
                archiveIndex--;                                                         // セーブ参照インデックスを減算
            }
            else　                                                                      // 入れ替えが実行されているなら
            {
                for (int i = 0;i < instanceGimmick.Count;i++)                           // ギミック数分回す
                {
                    if (i == swapIndex)                                                 // 現在の入れ替えインデックスと同一のものなら
                    {
                        instanceGimmick[i].AddControlCommandToArchive(archiveIndex);    // 書き換えられた管理コマンドをコピーしてアーカイブに登録する
                    }
                    else
                    {
                        instanceGimmick[i].AddNewCommandsToArchive(archiveIndex);       // コマンドアーカイブに前回と同様の内容を追加する
                    }
                }

                maxArchiveCount = archiveIndex;
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);        //ストレージコマンドのアーカイブを追加する
            }
        }

        /// <summary>
        /// 一手戻る
        /// </summary>
        public void Undo()
        {
            if (archiveIndex <= 0 || isPlaySwitch) return;    // セーブ参照インデックスが0よりも小さいか、入れ替え実行中であれば早期リターンする

            archiveIndex--;                                   // セーブ参照インデックスを減算する

            // 減算したセーブ情報に格納されていたコマンド情報を反映
            foreach (GimmckController gimmck in instanceGimmick)
            {
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        /// <summary>
        /// 一手進む
        /// </summary>
        public void Redo()
        {
            if (archiveIndex >= maxArchiveCount - 1|| isPlaySwitch) return;   // セーブ参照インデックスが要素数限界か、入れ替え実行中であれば早期リターンする

            archiveIndex++;                                                       // セーブ参照インデックスを加算する

            // 加算したセーブ情報に格納されていたコマンド情報を反映
            foreach (GimmckController gimmck in instanceGimmick)
            {
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }
    }
}