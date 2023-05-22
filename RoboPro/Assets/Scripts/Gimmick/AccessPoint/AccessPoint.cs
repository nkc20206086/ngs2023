using UnityEngine;
using System;
using Zenject;
using UniRx;
using Command;

namespace Gimmick
{
    public class AccessPoint : MonoBehaviour
    {
        public const float RADIUS = 1.5f;   // �����蔻��̗L���͈͂̔��a

        [SerializeField, Tooltip("�R�Â���M�~�b�N")]
        private GimmckController gimmckController;

        [Tooltip("AP�ƃM�~�b�N���q�����C���̐F")]
        public Color color;

        [SerializeField, Tooltip("�g�p�R�}���h1")]
        private CommandStruct usableCommand1;
        [SerializeField, Tooltip("�g�p�R�}���h2")]
        private CommandStruct usableCommand2;
        [SerializeField, Tooltip("�g�p�R�}���h3")]
        private CommandStruct usableCommand3;
        [SerializeField, Tooltip("�X�y�V�����R�}���h")]
        private CommandStruct specialCommand;

        public GimmckController controlGimmick
        {
            get => gimmckController;
        }

        /// <summary>
        /// �M�~�b�N��L��������֐�
        /// </summary>
        public void GimmickActivate()
        {
            // �M�~�b�N�ɓn�����߂̔z����쐬����
            CommandStruct[] usableCommands = new CommandStruct[CommandUtility.commandCount + 1];
            usableCommands[0] = usableCommand1;
            usableCommands[1] = usableCommand2;
            usableCommands[2] = usableCommand3;
            usableCommands[3] = specialCommand;

            gimmckController.StartUp(usableCommands);   // �M�~�b�N�Ǘ��N���X�̊J�n�֐������s
        }
    }
}