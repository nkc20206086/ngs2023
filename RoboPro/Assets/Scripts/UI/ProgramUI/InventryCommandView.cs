using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Command.Entity;
using Command;

namespace CommandUI
{
    public class InventryCommandView : MonoBehaviour
    {
        private int InventryLength = 3;
        [SerializeField] private Image[] programPanelIcon = new Image[3];  //�A�C�R��
        [SerializeField] private Sprite[] sprites; //�g���摜
        [SerializeField] private GameObject[] inventryBehavior = new GameObject[3];//�R�}���h
        [SerializeField] private GameObject[] inventryAxis = new GameObject[3];//��
        [SerializeField] private Image[] inventryAxisColor = new Image[3]; //���J���[
        [SerializeField] private GameObject[] inventryValue = new GameObject[3];//���l
        [SerializeField] private GameObject[] inventryValuesign = new GameObject[3];//���l

        public void InventryTextChange(CommandBase[] commands)
        {
            for (int i = 0; i < InventryLength; i++) // �R�}���h�̐��������s
            {

                if (commands[i] != null)
                {
                    switch (commands[i].GetCommandType())
                    {
                        case CommandType.Command:
                            MainCommand command = (MainCommand)commands[i];
                            //�A�C�R������
                            switch (command.GetName())
                            {
                                case "Move":
                                    programPanelIcon[i].sprite = sprites[0]; //Move�̃A�C�R���\��
                                    break;
                                case "Rotate":
                                    programPanelIcon[i].sprite = sprites[1];//Rotate�̃A�C�R���\��
                                    break;
                                case "Scale":
                                    programPanelIcon[i].sprite = sprites[2];//Scale�̃A�C�R���\��
                                    break;
                            }
                            inventryBehavior[i].GetComponentInChildren<TextMeshProUGUI>().text = command.GetName();

                            //���C���[�W�̐F�ύX
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

                            //���l��+-�ύX
                            if (int.Parse(command.GetValueText()) < 0)
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
    }
}