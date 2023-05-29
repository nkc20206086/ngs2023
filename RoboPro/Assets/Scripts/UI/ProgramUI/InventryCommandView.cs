using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Command.Entity;
using Command;
using System;

namespace CommandUI
{
    public class InventryCommandView : MonoBehaviour
    {
        private int InventryLength = 3;
        [SerializeField] private Image[] programPanelIcon = new Image[3];  //アイコン
        [SerializeField] private Sprite[] sprites; //使う画像
        [SerializeField] private GameObject[] inventryBehavior = new GameObject[3];//コマンド
        [SerializeField] private GameObject[] inventryAxis = new GameObject[3];//軸
        [SerializeField] private Image[] inventryAxisColor = new Image[3]; //軸カラー
        [SerializeField] private GameObject[] inventryValue = new GameObject[3];//数値
        [SerializeField] private GameObject[] inventryValuesign = new GameObject[3];//数値

        public event Action<int, int> InventryCommandIndexes;

        public void InventryTextChange(CommandBase[] commands)
        {
            for (int i = 0; i < InventryLength; i++) // コマンドの数だけ実行
            {
                if (commands[i] != null)
                {
                    inventryBehavior[i].SetActive(true);
                    inventryAxis[i].SetActive(true);
                    inventryValue[i].SetActive(true);

                    switch (commands[i].GetCommandType())
                    {
                        case CommandType.Command:
                            MainCommand command = (MainCommand)commands[i];
                            //アイコン判別
                            switch (command.GetMainCommandType())
                            {
                                case MainCommandType.Move:
                                    programPanelIcon[i].sprite = sprites[0]; //Moveのアイコン表示
                                    break;
                                case MainCommandType.Rotate:
                                    programPanelIcon[i].sprite = sprites[1];//Rotateのアイコン表示
                                    break;
                                case MainCommandType.Scale:
                                    programPanelIcon[i].sprite = sprites[2];//Scaleのアイコン表示
                                    break;
                            }
                            inventryBehavior[i].GetComponentInChildren<TextMeshProUGUI>().text = command.GetName();

                            //軸イメージの色変更
                            switch (command.GetAxisText())
                            {
                                case "X":
                                    inventryAxisColor[i].color = Color.red;
                                    break;
                                case "Y":
                                    inventryAxisColor[i].color = Color.green;
                                    break;
                                case "Z":
                                    inventryAxisColor[i].color = Color.blue;
                                    break;
                            }
                            inventryAxis[i].GetComponentInChildren<TextMeshProUGUI>().text = command.GetAxisText();

                            //数値の+-変更
                            if (command.GetValue() < 0)
                            {
                                inventryValuesign[i].SetActive(false);
                                inventryValue[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = command.GetValueText();
                            }
                            else
                            {
                                inventryValuesign[i].SetActive(true);
                                inventryValue[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = command.GetValueText();
                            }
                            break;

                        case CommandType.Axis:
                            inventryBehavior[i].gameObject.SetActive(false);
                            switch (commands[i].GetString())
                            {
                                case "X":
                                    inventryAxisColor[i].color = Color.red;
                                    break;
                                case "Y":
                                    inventryAxisColor[i].color = Color.green;
                                    break;
                                case "Z":
                                    inventryAxisColor[i].color = Color.blue;
                                    break;
                            }
                            inventryAxis[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetString();
                            inventryValue[i].gameObject.SetActive(false);
                            break;

                        case CommandType.Value:
                            inventryBehavior[i].gameObject.SetActive(false);
                            inventryAxis[i].gameObject.SetActive(false);
                            if (int.Parse(commands[i].GetString()) < 0)
                            {
                                inventryValuesign[i].SetActive(false);
                                inventryValue[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = commands[i].GetString();
                            }
                            else
                            {
                                inventryValuesign[i].SetActive(true);
                                inventryValue[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i].GetString();
                            }
                            break;
                    }
                }
                else
                {
                    inventryBehavior[i].gameObject.SetActive(false);
                    inventryAxis[i].gameObject.SetActive(false);
                    inventryValue[i].gameObject.SetActive(false);
                }
            }
        }

        public void ButtonIndexget(int mainIndex, int subIndex)
        {
            InventryCommandIndexes(mainIndex, subIndex);
        }
    }
}
