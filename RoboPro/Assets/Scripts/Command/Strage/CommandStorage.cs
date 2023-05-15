using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Command.Entity
{
    /// <summary>
    /// �X�g���[�W�R�}���h���Ǘ�����N���X
    /// </summary>
    public class CommandStorage : MonoBehaviour
    {
        [SerializeField,Tooltip("�X�g���[�W�R�}���h�̐�")]
        private int storageCount = 0;

        public CommandBase[] controlCommand; // �Ǘ����Ă���X�g���[�W�R�}���h(�ȉ��Ǘ��R�}���h�ƌĂ�)

        private List<CommandBase[]> storageArchive = new List<CommandBase[]>();

        private void Awake()
        {
            controlCommand = new CommandBase[storageCount];   // �ݒ肳�ꂽ���ŃX�g���[�W�R�}���h�z���������

            CommandBase[] startCommand = new CommandBase[3];

            storageArchive.Add(startCommand);               // �����R�}���h���A�[�J�C�u�ɒǉ�
        }

        /// <summary>
        /// �R�}���h��o�^����֐�(�ǉ��z�񂪂Ȃ��ꍇ�͐擪�v�f���R�s�[���܂�)
        /// </summary>
        /// <param name="index">�o�^�C���f�b�N�X</param>
        /// <param name="newArray">�ǉ�����z��</param>
        public void AddArchiveCommand(int index, CommandBase[] newArray = null)
        {
            if (index > storageArchive.Count) return;                                   // �o�^�C���f�b�N�X���͈͊O�Ȃ瑁�����^�[������

            controlCommand = newArray;                                                  // �Ǘ��R�}���h��ǉ��z��ɕύX

            CommandBase[] copyArray = new CommandBase[newArray.Length];                 // ���X�g�ɒǉ�����p�̃��[�J���z����쐬

            for (int i = 0; i < newArray.Length; i++)                                   // �ǉ��z��̗v�f�������s
            {
                copyArray[i] = newArray[i] == null ? default : newArray[i].BaseClone(); // ���[�J���z��̃R�s�[���쐬����
            }

            if (storageArchive.Count != index)                                          // �Ǘ��R�}���h�̗v�f���ƈقȂ�̂ł����
            {                                                                           // �s�v�ƂȂ�̂ł���܂ł̗v�f�����X�g����j������
                for (int i = storageArchive.Count - 1; i >= index; i--)                 // �R�}���h�A�[�J�C�u�̉ߏ蕪���s
                {
                    storageArchive.RemoveAt(i);                                         // ���̗v�f�����X�g����j������
                }
            }

            storageArchive.Add(copyArray);                                              // �R�}���h�A�[�J�C�u�Ƀ��[�J���z���ǉ�
        }

        /// <summary>
        /// �Ǘ��R�}���h��Ώۂ̗v�f�ԍ��̃A�[�J�C�u���ɏ���������
        /// </summary>
        /// <param name="index">�Ώۂ̗v�f�ԍ�</param>
        public void OverwriteControlCommand(int index)
        {
            CommandBase[] copyArray = new CommandBase[controlCommand.Length];   // �Ǘ��R�}���h�ɏ��������邽�߂̃��[�J���z����쐬(�z��𒼂œn���ƎQ�Ƃ�n������)

            for (int i = 0; i < storageArchive[index].Length; i++)              // �Ώۂ̃R�}���h�A�[�J�C�u�̗v�f�������s
            {
                // ���[�J���z��Ɋe�v�f�̃R�s�[���쐬(�l�����݂��Ȃ��ꍇ�̓f�t�H���g�l����)
                copyArray[i] = storageArchive[index][i] == null ? default : storageArchive[index][i].BaseClone(); 
            }

            controlCommand = copyArray;                                         // �Ǘ��R�}���h�����[�J���z��ɕύX
        }
    }
}