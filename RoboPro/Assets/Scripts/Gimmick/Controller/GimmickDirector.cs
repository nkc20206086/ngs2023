using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Command;
using Command.Entity;
using Gimmick.Interface;

namespace Gimmick
{
    /// <summary>
    /// �M�~�b�N�֘A�̏������Ǘ�����N���X
    /// </summary>
    public class GimmickDirector : MonoBehaviour,IGimmickAccess
    {
        [SerializeField,Tooltip("�X�g���[�W�R�}���h�Ǘ��N���X")]
        private CommandStorage storage;

        [SerializeField,Tooltip("�R�}���h�Ǘ��N���X")]
        private CommandDirector commandDirector;

        [Header("�f�o�b�O�p�@�{���ł���ΐ����ς݂̂��̂͗��p���Ȃ�")]
        [SerializeField]
        private List<GimmckController> instanceGimmickController;
        [SerializeField]
        private List<AccessPoint> accessPoints;

        [Header("�l�m�F�p�@���l�ύX�񐄏�")]
        [SerializeField]
        private int archiveIndex = 0;           // ���݂̃Z�[�u�󋵂̉��Ԗڂ����s���Ă��邩�̃C���f�b�N�X(�ȉ��Z�[�u�Q�ƃC���f�b�N�X�ƌĂ�)
        [SerializeField]
        private int swappingGimmickIndex = -1;  // ����ւ����̃M�~�b�N�C���f�b�N�X
        [SerializeField]
        private int maxArchiveCount = 0;      �@// �L�^���Ă������ւ��̐�

        private bool isSwapping = false;        // �R�}���h����ւ����s���ł��邩���Ǘ�����ϐ� 
        private bool isExecute = false;
        private CommandState state = CommandState.INACTIVE;

        private void Update()
        {
            if (isExecute)
            {
                if (!isSwapping)
                {
                    foreach (GimmckController gimmckController in instanceGimmickController)
                    {
                        gimmckController.CommandUpdate();   // �e�M�~�b�N�̃R�}���h�����s����
                    }

                    foreach (GimmckController gimmckController in instanceGimmickController)
                    {
                        if (gimmckController.GetExecutionStatus) return;
                    }

                    isExecute = false;
                }
            }
            else if (isSwapping && Input.GetKeyDown(KeyCode.Escape))
            {
                Close(default);
            }
        }

        /// <summary>
        /// �M�~�b�N�C���X�^���X����
        /// </summary>
        public void GimmickInstance(List<CommandStruct[]> setCommandList)
        {
            // �e�v�f�ɓ���ւ��̊J�n�����ƏI��������a���A�����C���f�b�N�X��o�^����
            for (int i = 0;i <  instanceGimmickController.Count;i++)
            {
                accessPoints[i].index = i;

                instanceGimmickController[i].StartUp(setCommandList[i]);
            }
        }

        /// <summary>
        /// �R�}���h����ւ������I��
        /// </summary>
        private void Close(Unit unit)
        {
            if (!isSwapping) return;                                                            // ����ւ����s���łȂ��Ȃ瑁�����^�[������
            isSwapping = false;                                                                 // ����ւ��I���ɕύX

            bool isSwap = commandDirector.CommandInvalidation();                                // �R�}���h�Ǘ��N���X�ɏ����̏I�����˗����A����ւ��̗L�������[�J���ϐ��ɕۑ�����

            if (!isSwap)                                                                        // �R�}���h����ւ����s���Ă��Ȃ��Ȃ�
            {
                maxArchiveCount--;                                                              // �L�^�����Z
                archiveIndex--;                                                                 // �Z�[�u�Q�ƃC���f�b�N�X�����Z
            }
            else�@                                                                              // ����ւ������s����Ă���Ȃ�
            {
                for (int i = 0;i < instanceGimmickController.Count;i++)                         // �M�~�b�N������
                {
                    if (i == swappingGimmickIndex)                                              // ���݂̓���ւ��C���f�b�N�X�Ɠ���̂��̂Ȃ�
                    {
                        instanceGimmickController[i].AddControlCommandToArchive(archiveIndex);  // ����������ꂽ�Ǘ��R�}���h���R�s�[���ăA�[�J�C�u�ɓo�^����
                    }
                    else
                    {
                        instanceGimmickController[i].AddNewCommandsToArchive(archiveIndex);     // �R�}���h�A�[�J�C�u�ɑO��Ɠ��l�̓��e��ǉ�����
                    }
                }

                maxArchiveCount = archiveIndex;                                                 // �L�^�����Z�[�u�Q�ƃC���f�b�N�X�Ɠ��l�̒l�ɕύX
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);                // �X�g���[�W�R�}���h�̃A�[�J�C�u��ǉ�����

                isExecute = false;
                state = CommandState.INACTIVE;

                foreach (GimmckController gimmck in instanceGimmickController)
                {
                    gimmck.IntializeAction();
                }
            }
        }

        public void StartCommandAction(Unit unit)
        {
            if (isExecute) return;
            isExecute = true;
            state = state != CommandState.MOVE_ON ? CommandState.MOVE_ON : CommandState.RETURN;

            foreach (GimmckController gimmckController in instanceGimmickController)
            {
                gimmckController.StartingAction(state);
            }
        }

        /// <summary>
        /// ���߂�
        /// </summary>
        public void Undo(Unit unit)
        {
            if (archiveIndex <= 0 || isSwapping) return;     // �Z�[�u�Q�ƃC���f�b�N�X��0�������������A����ւ����s���ł���Α������^�[������

            archiveIndex--;                                   // �Z�[�u�Q�ƃC���f�b�N�X�����Z����

            // ���Z�����Z�[�u���Ɋi�[����Ă����R�}���h���𔽉f
            foreach (GimmckController gimmck in instanceGimmickController)
            {
                gimmck.IntializeAction();
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        /// <summary>
        /// ���i��
        /// </summary>
        public void Redo(Unit unit)
        {
            if (archiveIndex >= maxArchiveCount - 1|| isSwapping) return;   // �Z�[�u�Q�ƃC���f�b�N�X���v�f�����E���A����ւ����s���ł���Α������^�[������

            archiveIndex++;                                                 // �Z�[�u�Q�ƃC���f�b�N�X�����Z����

            // ���Z�����Z�[�u���Ɋi�[����Ă����R�}���h���𔽉f
            foreach (GimmckController gimmck in instanceGimmickController)
            {
                gimmck.IntializeAction();
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        int IGimmickAccess.GetAccessPointIndex(Vector3 position)
        {
            int retIndex = -1;                           // �ԋp�C���f�b�N�X
            float minDistance = AccessPoint.MAX_RADIUS;  // �v�������ŒZ�̋���

            for (int i = 0; i < accessPoints.Count; i++)
            {
                // y���W�̂Ȃ����ʏ�̍��W���쐬
                Vector2 posA = new Vector2(position.x, position.z);
                Vector2 posB = new Vector2(accessPoints[i].transform.position.x, accessPoints[i].transform.position.z);

                float distance = Mathf.Abs(Vector2.Distance(posA, posB));   // x,z���W�̋������擾

                if (minDistance > distance)                                 // �ŒZ���������v���������Z���Ȃ�
                {
                    minDistance = distance;                                 // �ŒZ�������X�V
                    retIndex = i;                                           // �ԋp�C���f�b�N�X���X�V
                }
            }

            return retIndex;
        }

        void IGimmickAccess.Access(int index)
        {
            if (isExecute) return;
            if (isSwapping) return;         // ����ւ����s���ł���Ȃ瑁�����^�[������
            isSwapping = true;              // ����ւ����s���ɕύX

            swappingGimmickIndex = index;   // �M�~�b�N����ւ��C���f�b�N�X��ݒ�

            // �R�}���h�Ǘ��N���X�̓���ւ��L�����֐������s
            commandDirector.CommandActivation(instanceGimmickController[index].controlCommand);

            maxArchiveCount++;              // �L�^�����Z
            archiveIndex++;                 // �Z�[�u�Q�ƃC���f�b�N�X�����Z
        }
    }
}