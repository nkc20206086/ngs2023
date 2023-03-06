using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        private CommandStrage strage;

        [SerializeField,Tooltip("コマンド管理クラス")]
        private CommandDirector commandDirector;

        [Header("デバッグ用　本来であれば生成済みのものは利用しない")]
        [SerializeField]
        private List<GimmckController> instanceGimmick;
        [SerializeField]
        private List<AccessPoint> accessPoints;

        private List<SaveArchive> saveArchives = new List<SaveArchive>();   // 変更内容がセーブされたクラスのリスト

        private int saveArchiveIndex = -1;                                  // セーブ情報クラスリストの何番目を実行しているかのインデックス(以下セーブ参照インデックスと呼ぶ)

        private bool playSwitch = false;                                    // コマンド入れ替え実行中であるかを管理する変数 

        // 各配列の要素数限界を定数としたもの
        private const int COMMANDARRAY_COUNT = 3;
        private const int STRAGECOMMANDARRAY_COUNT = 3;

        /// <summary>
        /// ギミックインスタンス処理
        /// </summary>
        public void GimmickInstance()
        {
            // 各要素に入れ替えの開始処理と終了処理を預け、生成インデックスを登録する
            for (int i = 0;i <  instanceGimmick.Count;i++)
            {
                Subject<int> opneAct = new Subject<int>();
                opneAct.Subscribe(Switch);
                accessPoints[i].openAct = opneAct;
                Subject<Unit> closeAct = new Subject<Unit>();
                closeAct.Subscribe(Close);
                accessPoints[i].closeAct = closeAct;

                accessPoints[i].index = i;
            }
        }

        /// <summary>
        /// コマンド入れ替え処理実行
        /// </summary>
        /// <param name="index">実行インデックス</param>
        private void Switch(int index)
        {
            if (playSwitch) return;         // 入れ替え実行中であるなら早期リターンする
            playSwitch = true;              // 入れ替え実行中に変更

            // セーブ情報クラスを作成
            SaveArchive saveArchive = new SaveArchive(index, instanceGimmick[index].gameObject.transform);
            // コマンド管理クラスの入れ替え有効化関数を実行
            commandDirector.CommandActivate(instanceGimmick[index].controlCommand,saveArchive);

            saveArchives.Add(saveArchive);  // セーブ情報クラスをリストに追加
            saveArchiveIndex++;             // セーブ参照インデックスを加算

            Debug.Log($"値を追加しました {saveArchives.Count}");
        }

        /// <summary>
        /// コマンド入れ替え処理終了
        /// </summary>
        private void Close(Unit unit)
        {
            if (!playSwitch) return;                            // 入れ替え実行中でないなら早期リターンする
            playSwitch = false;                                 // 入れ替え終了に変更

            bool isSwitch = commandDirector.CommandInactive();  // コマンド管理クラスに処理の終了を依頼し、入れ替えの有無をローカル変数に保存する

            if (!isSwitch)                                      // コマンド入れ替えが行われていないなら
            {
                saveArchives.RemoveAt(saveArchives.Count - 1);  // セーブ情報クラスリストの先頭要素を削除する
                saveArchiveIndex--;                             // セーブ参照インデックスを減算
                Debug.Log($"値を削除しました {saveArchives.Count}");
            }
            else　                                              // 入れ替えが実行されているなら
            {
                if (saveArchiveIndex < saveArchives.Count - 1)  // 現在のセーブ参照インデックスがセーブ情報管理数よりも少ないなら
                {
                    // 参照インデックスよりも多い要素をすべて削除する
                    for (int i = saveArchives.Count - 1; i >= saveArchiveIndex; i--)
                    {
                        saveArchives.RemoveAt(i);
                        Debug.Log($"値を削除しました {saveArchives.Count}");
                    }
                }

            }

            // 要素確認用デバッグ処理
            for (int i = 0; i < saveArchives.Count; i++)
            {
                for (int j = 0; j < saveArchives[i].saveMainCommand.Length; j++)
                {
                    Debug.Log($"SaveIndex {i} MainCommand({j}) commandType is {saveArchives[i].saveMainCommand[j]}");
                }
            }
        }

        /// <summary>
        /// 一手戻る
        /// </summary>
        public void Undo()
        {
            if (saveArchiveIndex <= 0 || playSwitch) return;    // セーブ参照インデックスが0よりも小さいか、入れ替え実行中であれば早期リターンする

            saveArchiveIndex--;                                 // セーブ参照インデックスを減算する

            Debug.Log($"undo {saveArchiveIndex}");

            // 減算したセーブ情報に格納されていたコマンド情報を反映
            Array.Copy(saveArchives[saveArchiveIndex].saveMainCommand, instanceGimmick[saveArchives[saveArchiveIndex].index].controlCommand, COMMANDARRAY_COUNT);
            Array.Copy(saveArchives[saveArchiveIndex].saveStrageCommand, strage.bases, STRAGECOMMANDARRAY_COUNT);
        }

        /// <summary>
        /// 一手進む
        /// </summary>
        public void Redo()
        {
            if (saveArchiveIndex >= saveArchives.Count - 1 || playSwitch) return;   // セーブ参照インデックスが要素数限界か、入れ替え実行中であれば早期リターンする

            saveArchiveIndex++;                                                     // セーブ参照インデックスを加算する

            Debug.Log($"redo {saveArchiveIndex}");

            // 加算したセーブ情報に格納されていたコマンド情報を反映
            Array.Copy(saveArchives[saveArchiveIndex].saveMainCommand, instanceGimmick[saveArchives[saveArchiveIndex].index].controlCommand, COMMANDARRAY_COUNT);
            Array.Copy(saveArchives[saveArchiveIndex].saveStrageCommand, strage.bases, STRAGECOMMANDARRAY_COUNT);
        }
    }
}