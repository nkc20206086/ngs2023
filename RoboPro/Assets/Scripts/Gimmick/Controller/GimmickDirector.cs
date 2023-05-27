using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UniRx;
using Command;
using Command.Entity;
using Gimmick.Interface;
using InteractUI;
using ScanMode;

namespace Gimmick
{
    /// <summary>
    /// �M�~�b�N�֘A�̏������Ǘ�����N���X
    /// </summary>
    public class GimmickDirector : MonoBehaviour, IGimmickAccess
    {
        [Inject]
        private IScanModeLaserManageable laserManageable;

        [Inject]
        private IInteractUIControllable uIControllable;

        [SerializeField, Tooltip("�X�g���[�W�R�}���h�Ǘ��N���X")]
        private CommandStorage storage;

        [SerializeField, Tooltip("�R�}���h�Ǘ��N���X")]
        private CommandDirector commandDirector;

        [SerializeField]
        private DisplayInteractCanvasAsset displayInteract;

        private List<AccessPoint> accessPoints;

        [Header("�l�m�F�p�@���l�ύX�񐄏�")]
        [SerializeField]
        private int archiveIndex = 0;           // ���݂̃Z�[�u�󋵂̉��Ԗڂ����s���Ă��邩�̃C���f�b�N�X(�ȉ��Z�[�u�Q�ƃC���f�b�N�X�ƌĂ�)
        [SerializeField]
        private int swappingGimmickIndex = -1;  // ����ւ����̃M�~�b�N�C���f�b�N�X
        [SerializeField]
        private int maxArchiveCount = 0;      �@// �L�^���Ă������ւ��̐�

        private int uiActive = -1;              // UI��\�����Ă��邩

        private bool isSwapping = false;        // �R�}���h����ւ����s���ł��邩���Ǘ�����ϐ� 
        private bool isExecute = false;         // ���s�\�ł��邩
        private CommandState state = CommandState.INACTIVE;

        private void Update()
        {
            if (isExecute)
            {
                if (!isSwapping)
                {
                    foreach (AccessPoint accessPoint in accessPoints)
                    {
                        accessPoint.ControlGimmicksUpdate();   // �e�M�~�b�N�̃R�}���h�����s����
                    }

                    foreach (AccessPoint accessPoint in accessPoints)
                    {
                        // !
                        foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                        {
                            if (gimmick.GetExecutionStatus) return;
                        }
                    }

                    isExecute = false;
                }
            }

            if (isSwapping && Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }

        /// <summary>
        /// �M�~�b�N�C���X�^���X����
        /// </summary>
        public void GimmickInstance(Dictionary<BlockID,List<GameObject>> dic, List<AccessPointData> datas)
        {
            accessPoints = new List<AccessPoint>();
            List<ScanModeLaserTargetInfo> laserInfoList = new List<ScanModeLaserTargetInfo>();
            Debug.Log(datas[0].Commands[0].commandType);


            for (BlockID id = BlockID.Command_Red;id <= BlockID.Command_Black;id++)
            {
                if (dic.ContainsKey(id))
                {
                    accessPoints.Add(dic[id][0].GetComponent<AccessPoint>());
                    int index = datas.FindIndex(list => list.ColorID == (ColorID)((int)id - 100));
                    accessPoints[accessPoints.Count - 1].StartUp(datas[index]);

                    if (dic.ContainsKey(id + 100))
                    {
                        accessPoints[accessPoints.Count - 1].GimmickSubscrive(dic[id + 100]);
                    }
                    
                    if (dic.ContainsKey(id + 200))
                    {
                        accessPoints[accessPoints.Count - 1].GimmickSubscrive(dic[id + 200]);
                    }
                }
            }

            // �e�v�f�ɓ���ւ��̊J�n�����ƏI��������a���A�����C���f�b�N�X��o�^����
            for (int i = 0;i <  accessPoints.Count;i++)
            {
                // !
                foreach (GimmickController gimmick in accessPoints[i].controlGimmicks)
                {
                    laserInfoList.Add(new ScanModeLaserTargetInfo(accessPoints[i].transform, gimmick.transform, accessPoints[i].color));
                }

                accessPoints[i].GimmickActivate();
            }

            laserManageable.LaserInit(laserInfoList);
        }

        /// <summary>
        /// �R�}���h����ւ������I��
        /// </summary>
        private void Close()
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
                for (int i = 0;i < accessPoints.Count;i++)                         // �M�~�b�N������
                {
                    if (i == swappingGimmickIndex)                                              // ���݂̓���ւ��C���f�b�N�X�Ɠ���̂��̂Ȃ�
                    {
                        // !
                        foreach (GimmickController gimmick in accessPoints[i].controlGimmicks)
                        {
                            gimmick.AddControlCommandToArchive(archiveIndex);  // ����������ꂽ�Ǘ��R�}���h���R�s�[���ăA�[�J�C�u�ɓo�^����
                        }
                    }
                    else
                    {
                        // !
                        foreach (GimmickController gimmick in accessPoints[i].controlGimmicks)
                        {
                            gimmick.AddNewCommandsToArchive(archiveIndex);     // �R�}���h�A�[�J�C�u�ɑO��Ɠ��l�̓��e��ǉ�����
                        }
                    }
                }

                maxArchiveCount = archiveIndex;                                                 // �L�^�����Z�[�u�Q�ƃC���f�b�N�X�Ɠ��l�̒l�ɕύX
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);                // �X�g���[�W�R�}���h�̃A�[�J�C�u��ǉ�����

                state = CommandState.INACTIVE;

                foreach (AccessPoint accessPoint in accessPoints)
                {
                    // !
                    foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                    {
                        gimmick.IntializeAction();
                    }
                }
            }
        }

        public void StartCommandAction(Unit unit)
        {
            if (isExecute) return;
            isExecute = true;
            state = state != CommandState.MOVE_ON ? CommandState.MOVE_ON : CommandState.RETURN;

            foreach (AccessPoint accessPoint in accessPoints)
            {
                // !
                foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                {
                    gimmick.StartingAction(state);
                }
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
            foreach (AccessPoint accessPoint in accessPoints)
            {
                // !
                foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                {
                    gimmick.IntializeAction();
                    gimmick.OverwriteControlCommand(archiveIndex);
                }
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
            foreach (AccessPoint accessPoint in accessPoints)
            {
                // !
                foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                {
                    gimmick.IntializeAction();
                    gimmick.OverwriteControlCommand(archiveIndex);
                }

            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        int IGimmickAccess.GetAccessPointIndex(Vector3 position)
        {
            int retIndex = -1;                           // �ԋp�C���f�b�N�X
            float minDistance = AccessPoint.RADIUS;  // �v�������ŒZ�̋���

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

            if (uiActive != retIndex)
            {
                if (retIndex >= 0)
                {
                    uIControllable.HideUI();
                    uIControllable.ShowUI(ControllerType.Keyboard, displayInteract);
                    uIControllable.SetPosition(accessPoints[retIndex].transform.position + Vector3.up);
                }
                else
                {
                    uIControllable.HideUI();
                }

                uiActive = retIndex;
            }

            return retIndex;
        }

        Vector3 IGimmickAccess.Access(int index)
        {
            if (isSwapping) return Vector3.zero;         // ����ւ����s���ł���Ȃ瑁�����^�[������
            isSwapping = true;              // ����ւ����s���ɕύX

            swappingGimmickIndex = index;   // �M�~�b�N����ւ��C���f�b�N�X��ݒ�

            // �R�}���h�Ǘ��N���X�̓���ւ��L�����֐������s
            // !
            foreach (GimmickController gimmick in accessPoints[index].controlGimmicks)
            {
                commandDirector.CommandActivation(gimmick.controlCommand);
            }

            maxArchiveCount++;              // �L�^�����Z
            archiveIndex++;                 // �Z�[�u�Q�ƃC���f�b�N�X�����Z

            uIControllable.HideUI();
            uiActive = -1;

            return accessPoints[index].transform.position;
        }

        void IGimmickAccess.SetExecute(bool isExecute)
        {
            this.isExecute = isExecute;
        }
    }
}