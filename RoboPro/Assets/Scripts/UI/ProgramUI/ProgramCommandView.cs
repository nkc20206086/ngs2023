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
        [SerializeField] private GameObject[] LockObject; //コマンド枠ロック
        [SerializeField] private GameObject[] programCommand = new GameObject[3]; //コマンド
        [SerializeField] private GameObject[] programCommandLock = new GameObject[3]; //コマンドロック
        [SerializeField] private GameObject[] programPanelAxis = new GameObject[3]; //軸
        [SerializeField] private GameObject[] programPanelAxisLock = new GameObject[3]; //軸ロック
        [SerializeField] private Image[] programPanelAxisColor = new Image[3]; //軸カラー
        [SerializeField] private GameObject[] programPanelValue = new GameObject[3]; //数値
        [SerializeField] private GameObject[] programPanelValueLock = new GameObject[3];//数値ロック
        [SerializeField] private GameObject[] programPanelValuesign = new GameObject[3];//数値

        public event Action<int, int> ProgramCommandIndexes;

        public void ProgramCommandTextChange(MainCommand[] commands)
        {
            for (int i = 0; i < ProgramPanelLength; i++) // コマンドの数だけ実行
            {
                if (commands[i] == null)
                {
                    programCommand[i].SetActive(false);
                    programPanelAxis[i].SetActive(false);
                    programPanelValue[i].SetActive(false);
                    programCommandLock[i].SetActive(false);
                    programPanelValueLock[i].SetActive(false);
                    continue;
                }

                if (commands[i].GetMainCommandType() != MainCommandType.None)
                {
                    programCommand[i].SetActive(true);
                    programPanelAxis[i].SetActive(true);
                    programPanelValue[i].SetActive(true);

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


                    programCommand[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetName(); //コマンド表示

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
                        int showValue = Mathf.Abs(commands[i].GetValue());

                        if (commands[i].GetValue() < 0)
                        {
                            programPanelValuesign[i].SetActive(false);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = showValue.ToString(); //値を表示
                        }
                        else
                        {
                            programPanelValuesign[i].SetActive(true);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = showValue.ToString(); //値を表示
                        }
                    }
                    else
                    {
                        programPanelValue[i].SetActive(false); //値を非表示
                    }

                        programCommandLock[i].SetActive(commands[i].lockMenber);
                        programPanelAxisLock[i].SetActive(commands[i].lockCoordinateAxis);
                        programPanelValueLock[i].SetActive(commands[i].lockValue);
                }
                else
                {
                    //全非表示
                    programCommand[i].SetActive(false);
                    programPanelAxis[i].SetActive(false);
                    programPanelValue[i].SetActive(false);
                    if (commands[i].lockMenber&&commands[i].lockCoordinateAxis&&commands[i].lockValue)
                    {
                        LockObject[i].SetActive(true);
                    }
                    else
                    {
                        LockObject[i].SetActive(false);
                    }
                }
            }
        }

        public void ButtonIndexget(int mainIndex,int subIndex)
        {
            ProgramCommandIndexes(mainIndex, subIndex);
        }
    }
}
