using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// コマンド入れ替えの際のボタンを管理するクラス
    /// </summary>
    public class UITest : MonoBehaviour
    {
        [SerializeField, Tooltip("ボタンが格納された親オブジェクト")]
        private GameObject canvas;

        [SerializeField, Tooltip("オブジェクトのメインコマンドを表示するボタン")]
        private Button[] mainCommandButtons;

        [SerializeField, Tooltip("ストレージのコマンドを表示するボタン")]
        private Button[] strageCommandButtons;

        private Button[,] useMainCommandButtons;    // メインコマンドで使用するボタン

        private Button[,] useStrageCommandButtons;

        private const int COMMAND_COUNT = 3;        // コマンドの数

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sw">入れ替え時実行するアクション</param>
        /// <param name="ma">メインコマンドを入れ替える際実行するアクション</param>
        /// <param name="st">ストレージコマンドを入れ替える際実行するアクション</param>
        public void Intialize(Action<int> sw, Action<int> ma, Action<int> st)
        {

            Debug.Log("Ini");
            useMainCommandButtons = new Button[mainCommandButtons.Length, COMMAND_COUNT];   // メインコマンド使用ボタン変数を初期化
            useStrageCommandButtons = new Button[strageCommandButtons.Length, COMMAND_COUNT];   // ストレージコマンド使用ボタン変数を初期化

            for (int i = 0; i < mainCommandButtons.Length; i++)                             // メインコマンドの数だけ実行
            {
                // !インデックスをローカル変数に入れなおしているものがあります。これはAddLisnerで使用する変数が可変した際、その変更が反映されてしまうためです。
                int mainIndex = i;                                                          // メインインデックスをローカルで保存
                Button[] button = mainCommandButtons[i].GetComponentsInChildren<Button>();  // ボタンの子オブジェクトに付与されたボタンを含む全てのボタンコンポーネントを取得

                for (int j = 0; j < button.Length; j++)                                     // 取得できた数だけ実行
                {
                    int subIndex = j;                                                       // サブインデックスをローカルで保存
                    useMainCommandButtons[i, j] = button[j];                                // 取得ボタンを2次元配列に再格納する
                    useMainCommandButtons[i, j].onClick.AddListener(() => sw(subIndex));    // ocClickを登録(どのコマンドタイプであるかを受け取るアクション)
                    useMainCommandButtons[i, j].onClick.AddListener(() => ma(mainIndex));   // onClickを登録(メインコマンドのインデックスを受け取るアクション)
                }
            }

            for (int i = 0; i < strageCommandButtons.Length; i++)                             // ストレージコマンドの数だけ実行
            {
                // !インデックスをローカル変数に入れなおしているものがあります。これはAddLisnerで使用する変数が可変した際、その変更が反映されてしまうためです。
                int mainIndex = i;                                                          // メインインデックスをローカルで保存
                Button[] button = strageCommandButtons[i].GetComponentsInChildren<Button>();  // ボタンの子オブジェクトに付与されたボタンを含む全てのボタンコンポーネントを取得

                for (int j = 0; j < button.Length; j++)                                     // 取得できた数だけ実行
                {
                    int subIndex = j;                                                       // サブインデックスをローカルで保存
                    useStrageCommandButtons[i, j] = button[j];                                // 取得ボタンを2次元配列に再格納する
                    useStrageCommandButtons[i, j].onClick.AddListener(() => sw(subIndex));    // ocClickを登録(どのコマンドタイプであるかを受け取るアクション)
                    useStrageCommandButtons[i, j].onClick.AddListener(() => st(mainIndex));   // onClickを登録(メインコマンドのインデックスを受け取るアクション)
                }
            }

            CanvasHide();                                                                   // キャンバスを非表示にする
        }

        /// <summary>
        /// キャンバスを表示する
        /// </summary>
        public void CanvasDisplay()
        {
            canvas.SetActive(true);
        }

        /// <summary>
        /// キャンバスを非表示にする
        /// </summary>
        public void CanvasHide()
        {
            canvas.SetActive(false);
        }

        /// <summary>
        /// メインコマンドボタンのテキストを受け取ったコマンドのものに差し替える処理
        /// </summary>
        /// <param name="commands">対象のコマンド</param>
        public void MainButtonTextRewriting(MainCommand[] commands)
        {
            for (int i = 0; i < mainCommandButtons.Length; i++) // コマンドの数だけ実行
            {
                if (commands != null)
                {
                    // メインコマンドボタンにそれぞれの情報を反映
                    useMainCommandButtons[i, 0].GetComponentInChildren<TextMeshProUGUI>().text = commands[i]?.GetString();
                    useMainCommandButtons[i, 1].GetComponentInChildren<TextMeshProUGUI>().text = commands[i]?.GetAxisText();
                    useMainCommandButtons[i, 2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i]?.GetValueText();
                }
                else
                {
                    // コマンド情報がなければテキストを初期化する
                    useMainCommandButtons[i, 0].GetComponentInChildren<TextMeshProUGUI>().text = default;
                    useMainCommandButtons[i, 1].GetComponentInChildren<TextMeshProUGUI>().text = default;
                    useMainCommandButtons[i, 2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = default;
                }
            }
        }

        /// <summary>
        /// ストレージコマンドボタンのテキストを受け取ったコマンドのものに差し替える処理
        /// </summary>
        /// <param name="commands">対象のコマンド</param>
        public void StrageButtonTextRewriting(CommandBase[] commands)
        {
            for (int i = 0; i < strageCommandButtons.Length; i++) // コマンドの数だけ実行
            {
                // コマンドの文章をストレージコマンドがあるなら反映
                useStrageCommandButtons[i, 0].GetComponentInChildren<TextMeshProUGUI>().text = commands[i] != null ? commands[i].GetString() : default;
                useStrageCommandButtons[i, 1].GetComponentInChildren<TextMeshProUGUI>().text = commands[i] != null ? commands[i].GetString() : default;
                useStrageCommandButtons[i, 2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i] != null ? commands[i].GetString() : default;
            }
        }
    }
}
