using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        private CommandStrage strage;

        [SerializeField,Tooltip("�R�}���h�Ǘ��N���X")]
        private CommandDirector commandDirector;

        [Header("�f�o�b�O�p�@�{���ł���ΐ����ς݂̂��̂͗��p���Ȃ�")]
        [SerializeField]
        private List<GimmckController> instanceGimmick;
        [SerializeField]
        private List<AccessPoint> accessPoints;

        private List<SaveArchive> saveArchives = new List<SaveArchive>();   // �ύX���e���Z�[�u���ꂽ�N���X�̃��X�g

        private int saveArchiveIndex = -1;                                  // �Z�[�u���N���X���X�g�̉��Ԗڂ����s���Ă��邩�̃C���f�b�N�X(�ȉ��Z�[�u�Q�ƃC���f�b�N�X�ƌĂ�)

        private bool playSwitch = false;                                    // �R�}���h����ւ����s���ł��邩���Ǘ�����ϐ� 

        // �e�z��̗v�f�����E��萔�Ƃ�������
        private const int COMMANDARRAY_COUNT = 3;
        private const int STRAGECOMMANDARRAY_COUNT = 3;

        /// <summary>
        /// �M�~�b�N�C���X�^���X����
        /// </summary>
        public void GimmickInstance()
        {
            // �e�v�f�ɓ���ւ��̊J�n�����ƏI��������a���A�����C���f�b�N�X��o�^����
            for (int i = 0;i <  instanceGimmick.Count;i++)
            {
                Subject<int> opneAct = new Subject<int>();
                opneAct.Subscribe(Switch);
                accessPoints[i].openAct = opneAct;
                Subject<Unit> closeAct = new Subject<Unit>();
                closeAct.Subscribe(Close);
                accessPoints[i].closeAct = closeAct;

                accessPoints[i].index = i;
            }
        }

        /// <summary>
        /// �R�}���h����ւ��������s
        /// </summary>
        /// <param name="index">���s�C���f�b�N�X</param>
        private void Switch(int index)
        {
            if (playSwitch) return;         // ����ւ����s���ł���Ȃ瑁�����^�[������
            playSwitch = true;              // ����ւ����s���ɕύX

            // �Z�[�u���N���X���쐬
            SaveArchive saveArchive = new SaveArchive(index, instanceGimmick[index].gameObject.transform);
            // �R�}���h�Ǘ��N���X�̓���ւ��L�����֐������s
            commandDirector.CommandActivate(instanceGimmick[index].controlCommand,saveArchive);

            saveArchives.Add(saveArchive);  // �Z�[�u���N���X�����X�g�ɒǉ�
            saveArchiveIndex++;             // �Z�[�u�Q�ƃC���f�b�N�X�����Z

            Debug.Log($"�l��ǉ����܂��� {saveArchives.Count}");
        }

        /// <summary>
        /// �R�}���h����ւ������I��
        /// </summary>
        private void Close(Unit unit)
        {
            if (!playSwitch) return;                            // ����ւ����s���łȂ��Ȃ瑁�����^�[������
            playSwitch = false;                                 // ����ւ��I���ɕύX

            bool isSwitch = commandDirector.CommandInactive();  // �R�}���h�Ǘ��N���X�ɏ����̏I�����˗����A����ւ��̗L�������[�J���ϐ��ɕۑ�����

            if (!isSwitch)                                      // �R�}���h����ւ����s���Ă��Ȃ��Ȃ�
            {
                saveArchives.RemoveAt(saveArchives.Count - 1);  // �Z�[�u���N���X���X�g�̐擪�v�f���폜����
                saveArchiveIndex--;                             // �Z�[�u�Q�ƃC���f�b�N�X�����Z
                Debug.Log($"�l���폜���܂��� {saveArchives.Count}");
            }
            else�@                                              // ����ւ������s����Ă���Ȃ�
            {
                if (saveArchiveIndex < saveArchives.Count - 1)  // ���݂̃Z�[�u�Q�ƃC���f�b�N�X���Z�[�u���Ǘ����������Ȃ��Ȃ�
                {
                    // �Q�ƃC���f�b�N�X���������v�f�����ׂč폜����
                    for (int i = saveArchives.Count - 1; i >= saveArchiveIndex; i--)
                    {
                        saveArchives.RemoveAt(i);
                        Debug.Log($"�l���폜���܂��� {saveArchives.Count}");
                    }
                }

            }

            // �v�f�m�F�p�f�o�b�O����
            for (int i = 0; i < saveArchives.Count; i++)
            {
                for (int j = 0; j < saveArchives[i].saveMainCommand.Length; j++)
                {
                    Debug.Log($"SaveIndex {i} MainCommand({j}) commandType is {saveArchives[i].saveMainCommand[j]}");
                }
            }
        }

        /// <summary>
        /// ���߂�
        /// </summary>
        public void Undo()
        {
            if (saveArchiveIndex <= 0 || playSwitch) return;    // �Z�[�u�Q�ƃC���f�b�N�X��0�������������A����ւ����s���ł���Α������^�[������

            saveArchiveIndex--;                                 // �Z�[�u�Q�ƃC���f�b�N�X�����Z����

            Debug.Log($"undo {saveArchiveIndex}");

            // ���Z�����Z�[�u���Ɋi�[����Ă����R�}���h���𔽉f
            Array.Copy(saveArchives[saveArchiveIndex].saveMainCommand, instanceGimmick[saveArchives[saveArchiveIndex].index].controlCommand, COMMANDARRAY_COUNT);
            Array.Copy(saveArchives[saveArchiveIndex].saveStrageCommand, strage.bases, STRAGECOMMANDARRAY_COUNT);
        }

        /// <summary>
        /// ���i��
        /// </summary>
        public void Redo()
        {
            if (saveArchiveIndex >= saveArchives.Count - 1 || playSwitch) return;   // �Z�[�u�Q�ƃC���f�b�N�X���v�f�����E���A����ւ����s���ł���Α������^�[������

            saveArchiveIndex++;                                                     // �Z�[�u�Q�ƃC���f�b�N�X�����Z����

            Debug.Log($"redo {saveArchiveIndex}");

            // ���Z�����Z�[�u���Ɋi�[����Ă����R�}���h���𔽉f
            Array.Copy(saveArchives[saveArchiveIndex].saveMainCommand, instanceGimmick[saveArchives[saveArchiveIndex].index].controlCommand, COMMANDARRAY_COUNT);
            Array.Copy(saveArchives[saveArchiveIndex].saveStrageCommand, strage.bases, STRAGECOMMANDARRAY_COUNT);
        }
    }
}