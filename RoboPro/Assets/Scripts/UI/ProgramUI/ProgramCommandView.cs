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
        [SerializeField] private Image[] programPanelIcon = new Image[3];  //�A�C�R��
        [SerializeField] private Sprite[] sprites; //�g���摜
        [SerializeField] private GameObject[] LockObject; //�R�}���h�g���b�N
        [SerializeField] private GameObject[] programCommand = new GameObject[3]; //�R�}���h
        [SerializeField] private GameObject[] programCommandLock = new GameObject[3]; //�R�}���h���b�N
        [SerializeField] private GameObject[] programPanelAxis = new GameObject[3]; //��
        [SerializeField] private GameObject[] programPanelAxisLock = new GameObject[3]; //�����b�N
        [SerializeField] private Image[] programPanelAxisColor = new Image[3]; //���J���[
        [SerializeField] private GameObject[] programPanelValue = new GameObject[3]; //���l
        [SerializeField] private GameObject[] programPanelValueLock = new GameObject[3];//���l���b�N
        [SerializeField] private GameObject[] programPanelValuesign = new GameObject[3];//���l

        public event Action<int, int> ProgramCommandIndexes;

        public void ProgramCommandTextChange(MainCommand[] commands)
        {
            for (int i = 0; i < ProgramPanelLength; i++) // �R�}���h�̐��������s
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
                        case "�ړ�": 
                            programPanelIcon[i].sprite = sprites[0]; //Move�̃A�C�R���\��
                            break;
                        case "��]":
                            programPanelIcon[i].sprite = sprites[1];//Rotate�̃A�C�R���\��
                            break;
                        case "�g��":
                            programPanelIcon[i].sprite = sprites[2];//Scale�̃A�C�R���\��
                            break;
                    }


                    programCommand[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetName(); //�R�}���h�\��

                    if(commands[i].GetAxisText() !="NONE") //�v���O�������Ɏ������邩�ǂ���
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
                        programPanelAxis[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetAxisText(); //����\��
                    }
                    else
                    {
                        programPanelAxis[i].SetActive(false);�@//�����\��
                    }

                    if (commands[i].GetValueText() != "0")�@//�v���O�������ɒl�����邩�ǂ���
                    {
                        int showValue = Mathf.Abs(commands[i].GetValue());

                        if (commands[i].GetValue() < 0)
                        {
                            programPanelValuesign[i].SetActive(false);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = showValue.ToString(); //�l��\��
                        }
                        else
                        {
                            programPanelValuesign[i].SetActive(true);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = showValue.ToString(); //�l��\��
                        }
                    }
                    else
                    {
                        programPanelValue[i].SetActive(false); //�l���\��
                    }

                        programCommandLock[i].SetActive(commands[i].lockMenber);
                        programPanelAxisLock[i].SetActive(commands[i].lockCoordinateAxis);
                        programPanelValueLock[i].SetActive(commands[i].lockValue);
                }
                else
                {
                    //�S��\��
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
