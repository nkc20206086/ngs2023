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
        [SerializeField] private GameObject[] ProgramCommand = new GameObject[3]; //�R�}���h
        [SerializeField] private GameObject[] programPanelAxis = new GameObject[3]; //��
        [SerializeField] private Image[] programPanelAxisColor = new Image[3]; //���J���[
        [SerializeField] private GameObject[] programPanelValue = new GameObject[3]; //���l
        [SerializeField] private GameObject[] programPanelValuesign = new GameObject[3];//���l

        public event Action<int, int> ProgramCommandIndexes;

        public void ProgramCommandTextChange(MainCommand[] commands)
        {
            for (int i = 0; i < ProgramPanelLength; i++) // �R�}���h�̐��������s
            {
                if (commands[i] != null)
                {
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


                    ProgramCommand[i].GetComponentInChildren<TextMeshProUGUI>().text = commands[i].GetName(); //�R�}���h�\��

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
                        if (int.Parse(commands[i].GetValueText()) < 0)
                        {
                            programPanelValuesign[i].SetActive(false);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = commands[i].GetValueText(); //�l��\��
                        }
                        else
                        {
                            programPanelValuesign[i].SetActive(true);
                            programPanelValue[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i].GetValueText(); //�l��\��
                        }
                    }
                    else
                    {
                        programPanelValue[i].SetActive(false); //�l���\��
                    }
                }
                else
                {
                    //�S��\��
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
