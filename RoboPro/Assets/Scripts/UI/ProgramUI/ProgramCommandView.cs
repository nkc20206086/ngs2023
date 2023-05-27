using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Command.Entity;
using System;

namespace CommandUI
{
    public class ProgramCommandView : MonoBehaviour
    {
        private int ProgramPanelLength = 3;
        [SerializeField] private Image[] programPanelIcon = new Image[3];  //アイコン
        [SerializeField] private Sprite[] sprites; //使う画像
        [SerializeField] private GameObject[] ProgramCommand = new GameObject[3]; //コマンド
        [SerializeField] private GameObject[] programPanelAxis = new GameObject[3]; //軸
        [SerializeField] private Image[] programPanelAxisColor = new Image[3]; //軸カラー
        [SerializeField] private GameObject[] programPanelValue = new GameObject[3]; //数値
        [SerializeField] private GameObject[] programPanelValuesign = new GameObject[3];//数値

        public event Action<int, int> ProgramCommandIndexes;

        public void ProgramCommandTextChange(MainCommand[] commands)
        {
            for (int i = 0; i < ProgramPanelLength; i++) // コマンドの数だけ実行
            {
                if (commands[i] != null)
                {
                    switch (commands[i].GetName())
                    {
                        case "移動": 
                            programPanelIcon[i].sprite = sprites[0]; //Moveのアイコン表示
                            break;
                        case "回転":
                            programPanelIcon[i].sprite = sprites[1];//Rotateのアイコン表示
                            break;
                        case "拡大":
                            programPanelIcon[i].sprite = sprites[2];//Scaleのアイコン表示
                            break;
                    }


                    ProgramCommand[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetName(); //コマンド表示

                    if(commands[i].GetAxisText() !="NONE") //プログラム内に軸があるかどうか
                    {
                        switch (commands[i].GetAxisText())
                        {
                            case "X":
                                programPanelAxisColor[i].color = Color.red;
                                break;
                            case "Y":
                                programPanelAxisColor[i].color = Color.green;
                                break;
                            case "Z":
                                programPanelAxisColor[i].color = Color.blue;
                                break;
                        }
                        programPanelAxis[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetAxisText(); //軸を表示
                    }
                    else
                    {
                        programPanelAxis[i].SetActive(false);　//軸を非表示
                    }

                    if (commands[i].GetValueText() != "0")　//プログラム内に値があるかどうか
                    {
                        if (int.Parse(commands[i].GetValueText()) < 0)
                        {
                            programPanelValuesign[i].SetActive(false);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = commands[i].GetValueText(); //値を表示
                        }
                        else
                        {
                            programPanelValuesign[i].SetActive(true);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i].GetValueText(); //値を表示
                        }
                    }
                    else
                    {
                        programPanelValue[i].SetActive(false); //値を非表示
                    }
                }
                else
                {
                    //全非表示
                    ProgramCommand[i].SetActive(false);
                    programPanelAxis[i].SetActive(false);
                    programPanelValue[i].SetActive(false);
                }
            }
        }

        public void ButtonIndexget(int mainIndex,int subIndex)
        {
            ProgramCommandIndexes(mainIndex, subIndex);
        }
    }
}
