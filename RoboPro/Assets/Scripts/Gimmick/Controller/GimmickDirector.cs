using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Command;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// �M�~�b�N�֘A�̏������Ǘ�����N���X
    /// </summary>
    public class GimmickDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("�X�g���[�W�R�}���h�Ǘ��N���X")]
        private CommandStorage storage;

        [SerializeField,Tooltip("�R�}���h�Ǘ��N���X")]
        private CommandDirector commandDirector;

        [Header("�f�o�b�O�p�@�{���ł���ΐ����ς݂̂��̂͗��p���Ȃ�")]
        [SerializeField]
        private List<GimmckController> instanceGimmick;
        [SerializeField]
        private List<AccessPoint> accessPoints;

        [Header("�l�m�F�p�@���l�ύX�񐄏�")]
        [SerializeField]
        private int archiveIndex = 0;                                       // ���݂̃Z�[�u�󋵂̉��Ԗڂ����s���Ă��邩�̃C���f�b�N�X(�ȉ��Z�[�u�Q�ƃC���f�b�N�X�ƌĂ�)
        [SerializeField]
        private int swapIndex = -1;                                         // ����ւ����̃M�~�b�N�C���f�b�N�X
        [SerializeField]
        private int maxArchiveCount = 0;                                    // �L�^���Ă������ւ��̐�

        private bool isPlaySwitch = false;                                  // �R�}���h����ւ����s���ł��邩���Ǘ�����ϐ� 

        private void Update()
        {
            if (isPlaySwitch) return;                 // ����ւ����ł���Ώ������Ȃ�

            foreach (GimmckController gimmckController in instanceGimmick)
            {
                gimmckController.CommandUpdate();   // �e�M�~�b�N�̃R�}���h�����s����
            }
        }

        /// <summary>
        /// �M�~�b�N�C���X�^���X����
        /// </summary>
        public void GimmickInstance(List<CommandStruct[]> setCommandList)
        {
            // �e�v�f�ɓ���ւ��̊J�n�����ƏI��������a���A�����C���f�b�N�X��o�^����
            for (int i = 0;i <  instanceGimmick.Count;i++)
            {
                Subject<int> openAct = new Subject<int>();
                openAct.Subscribe(Swap);
                accessPoints[i].openAct = openAct;
                Subject<Unit> closeAct = new Subject<Unit>();
                closeAct.Subscribe(Close);
                accessPoints[i].closeAct = closeAct;

                accessPoints[i].index = i;

                instanceGimmick[i].StartUp(setCommandList[i]);
            }
        }

        /// <summary>
        /// �R�}���h����ւ��������s
        /// </summary>
        /// <param name="index">���s�C���f�b�N�X</param>
        private void Swap(int index)
        {
            if (isPlaySwitch) return;         // ����ւ����s���ł���Ȃ瑁�����^�[������
            isPlaySwitch = true;              // ����ւ����s���ɕύX

            swapIndex = index;                // �M�~�b�N����ւ��C���f�b�N�X��ݒ�

            // �R�}���h�Ǘ��N���X�̓���ւ��L�����֐������s
            commandDirector.CommandActivation(instanceGimmick[index].controlCommand);

            maxArchiveCount++;          // �L�^�����Z
            archiveIndex++;             // �Z�[�u�Q�ƃC���f�b�N�X�����Z
        }

        /// <summary>
        /// �R�}���h����ւ������I��
        /// </summary>
        private void Close(Unit unit)
        {
            if (!isPlaySwitch) return;                                                  // ����ւ����s���łȂ��Ȃ瑁�����^�[������
            isPlaySwitch = false;                                                       // ����ւ��I���ɕύX

            bool isSwitch = commandDirector.CommandInvalidation();                      // �R�}���h�Ǘ��N���X�ɏ����̏I�����˗����A����ւ��̗L�������[�J���ϐ��ɕۑ�����

            if (!isSwitch)                                                              // �R�}���h����ւ����s���Ă��Ȃ��Ȃ�
            {
                maxArchiveCount--;                                                      // �L�^�����Z
                archiveIndex--;                                                         // �Z�[�u�Q�ƃC���f�b�N�X�����Z
            }
            else�@                                                                      // ����ւ������s����Ă���Ȃ�
            {
                for (int i = 0;i < instanceGimmick.Count;i++)                           // �M�~�b�N������
                {
                    if (i == swapIndex)                                                 // ���݂̓���ւ��C���f�b�N�X�Ɠ���̂��̂Ȃ�
                    {
                        instanceGimmick[i].AddControlCommandToArchive(archiveIndex);    // ����������ꂽ�Ǘ��R�}���h���R�s�[���ăA�[�J�C�u�ɓo�^����
                    }
                    else
                    {
                        instanceGimmick[i].AddNewCommandsToArchive(archiveIndex);       // �R�}���h�A�[�J�C�u�ɑO��Ɠ��l�̓��e��ǉ�����
                    }
                }

                maxArchiveCount = archiveIndex;
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);        //�X�g���[�W�R�}���h�̃A�[�J�C�u��ǉ�����
            }
        }

        /// <summary>
        /// ���߂�
        /// </summary>
        public void Undo()
        {
            if (archiveIndex <= 0 || isPlaySwitch) return;    // �Z�[�u�Q�ƃC���f�b�N�X��0�������������A����ւ����s���ł���Α������^�[������

            archiveIndex--;                                   // �Z�[�u�Q�ƃC���f�b�N�X�����Z����

            // ���Z�����Z�[�u���Ɋi�[����Ă����R�}���h���𔽉f
            foreach (GimmckController gimmck in instanceGimmick)
            {
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        /// <summary>
        /// ���i��
        /// </summary>
        public void Redo()
        {
            if (archiveIndex >= maxArchiveCount - 1|| isPlaySwitch) return;   // �Z�[�u�Q�ƃC���f�b�N�X���v�f�����E���A����ւ����s���ł���Α������^�[������

            archiveIndex++;                                                       // �Z�[�u�Q�ƃC���f�b�N�X�����Z����

            // ���Z�����Z�[�u���Ɋi�[����Ă����R�}���h���𔽉f
            foreach (GimmckController gimmck in instanceGimmick)
            {
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }
    }
}