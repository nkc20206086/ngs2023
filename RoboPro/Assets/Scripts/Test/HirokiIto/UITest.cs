using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// �R�}���h����ւ��̍ۂ̃{�^�����Ǘ�����N���X
    /// </summary>
    public class UITest : MonoBehaviour
    {
        [SerializeField, Tooltip("�{�^�����i�[���ꂽ�e�I�u�W�F�N�g")]
        private GameObject canvas;

        [SerializeField, Tooltip("�I�u�W�F�N�g�̃��C���R�}���h��\������{�^��")]
        private Button[] mainCommandButtons;

        [SerializeField, Tooltip("�X�g���[�W�̃R�}���h��\������{�^��")]
        private Button[] strageCommandButtons;

        private Button[,] useMainCommandButtons;    // ���C���R�}���h�Ŏg�p����{�^��

        private Button[,] useStrageCommandButtons;

        private const int COMMAND_COUNT = 3;        // �R�}���h�̐�

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sw">����ւ������s����A�N�V����</param>
        /// <param name="ma">���C���R�}���h�����ւ���ێ��s����A�N�V����</param>
        /// <param name="st">�X�g���[�W�R�}���h�����ւ���ێ��s����A�N�V����</param>
        public void Intialize(Action<int> sw, Action<int> ma, Action<int> st)
        {

            Debug.Log("Ini");
            useMainCommandButtons = new Button[mainCommandButtons.Length, COMMAND_COUNT];   // ���C���R�}���h�g�p�{�^���ϐ���������
            useStrageCommandButtons = new Button[strageCommandButtons.Length, COMMAND_COUNT];   // �X�g���[�W�R�}���h�g�p�{�^���ϐ���������

            for (int i = 0; i < mainCommandButtons.Length; i++)                             // ���C���R�}���h�̐��������s
            {
                // !�C���f�b�N�X�����[�J���ϐ��ɓ���Ȃ����Ă�����̂�����܂��B�����AddLisner�Ŏg�p����ϐ����ς����ہA���̕ύX�����f����Ă��܂����߂ł��B
                int mainIndex = i;                                                          // ���C���C���f�b�N�X�����[�J���ŕۑ�
                Button[] button = mainCommandButtons[i].GetComponentsInChildren<Button>();  // �{�^���̎q�I�u�W�F�N�g�ɕt�^���ꂽ�{�^�����܂ޑS�Ẵ{�^���R���|�[�l���g���擾

                for (int j = 0; j < button.Length; j++)                                     // �擾�ł������������s
                {
                    int subIndex = j;                                                       // �T�u�C���f�b�N�X�����[�J���ŕۑ�
                    useMainCommandButtons[i, j] = button[j];                                // �擾�{�^����2�����z��ɍĊi�[����
                    useMainCommandButtons[i, j].onClick.AddListener(() => sw(subIndex));    // ocClick��o�^(�ǂ̃R�}���h�^�C�v�ł��邩���󂯎��A�N�V����)
                    useMainCommandButtons[i, j].onClick.AddListener(() => ma(mainIndex));   // onClick��o�^(���C���R�}���h�̃C���f�b�N�X���󂯎��A�N�V����)
                }
            }

            for (int i = 0; i < strageCommandButtons.Length; i++)                             // �X�g���[�W�R�}���h�̐��������s
            {
                // !�C���f�b�N�X�����[�J���ϐ��ɓ���Ȃ����Ă�����̂�����܂��B�����AddLisner�Ŏg�p����ϐ����ς����ہA���̕ύX�����f����Ă��܂����߂ł��B
                int mainIndex = i;                                                          // ���C���C���f�b�N�X�����[�J���ŕۑ�
                Button[] button = strageCommandButtons[i].GetComponentsInChildren<Button>();  // �{�^���̎q�I�u�W�F�N�g�ɕt�^���ꂽ�{�^�����܂ޑS�Ẵ{�^���R���|�[�l���g���擾

                for (int j = 0; j < button.Length; j++)                                     // �擾�ł������������s
                {
                    int subIndex = j;                                                       // �T�u�C���f�b�N�X�����[�J���ŕۑ�
                    useStrageCommandButtons[i, j] = button[j];                                // �擾�{�^����2�����z��ɍĊi�[����
                    useStrageCommandButtons[i, j].onClick.AddListener(() => sw(subIndex));    // ocClick��o�^(�ǂ̃R�}���h�^�C�v�ł��邩���󂯎��A�N�V����)
                    useStrageCommandButtons[i, j].onClick.AddListener(() => st(mainIndex));   // onClick��o�^(���C���R�}���h�̃C���f�b�N�X���󂯎��A�N�V����)
                }
            }

            CanvasHide();                                                                   // �L�����o�X���\���ɂ���
        }

        /// <summary>
        /// �L�����o�X��\������
        /// </summary>
        public void CanvasDisplay()
        {
            canvas.SetActive(true);
        }

        /// <summary>
        /// �L�����o�X���\���ɂ���
        /// </summary>
        public void CanvasHide()
        {
            canvas.SetActive(false);
        }

        /// <summary>
        /// ���C���R�}���h�{�^���̃e�L�X�g���󂯎�����R�}���h�̂��̂ɍ����ւ��鏈��
        /// </summary>
        /// <param name="commands">�Ώۂ̃R�}���h</param>
        public void MainButtonTextRewriting(MainCommand[] commands)
        {
            for (int i = 0; i < mainCommandButtons.Length; i++) // �R�}���h�̐��������s
            {
                if (commands != null)
                {
                    // ���C���R�}���h�{�^���ɂ��ꂼ��̏��𔽉f
                    useMainCommandButtons[i, 0].GetComponentInChildren<TextMeshProUGUI>().text = commands[i]?.GetString();
                    useMainCommandButtons[i, 1].GetComponentInChildren<TextMeshProUGUI>().text = commands[i]?.GetAxisText();
                    useMainCommandButtons[i, 2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i]?.GetValueText();
                }
                else
                {
                    // �R�}���h��񂪂Ȃ���΃e�L�X�g������������
                    useMainCommandButtons[i, 0].GetComponentInChildren<TextMeshProUGUI>().text = default;
                    useMainCommandButtons[i, 1].GetComponentInChildren<TextMeshProUGUI>().text = default;
                    useMainCommandButtons[i, 2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = default;
                }
            }
        }

        /// <summary>
        /// �X�g���[�W�R�}���h�{�^���̃e�L�X�g���󂯎�����R�}���h�̂��̂ɍ����ւ��鏈��
        /// </summary>
        /// <param name="commands">�Ώۂ̃R�}���h</param>
        public void StrageButtonTextRewriting(CommandBase[] commands)
        {
            for (int i = 0; i < strageCommandButtons.Length; i++) // �R�}���h�̐��������s
            {
                // �R�}���h�̕��͂��X�g���[�W�R�}���h������Ȃ甽�f
                useStrageCommandButtons[i, 0].GetComponentInChildren<TextMeshProUGUI>().text = commands[i] != null ? commands[i].GetString() : default;
                useStrageCommandButtons[i, 1].GetComponentInChildren<TextMeshProUGUI>().text = commands[i] != null ? commands[i].GetString() : default;
                useStrageCommandButtons[i, 2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = commands[i] != null ? commands[i].GetString() : default;
            }
        }
    }
}
