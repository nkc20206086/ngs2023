using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// �M�~�b�N�̃Z�[�u�����i�[����\����
    /// </summary>
    public struct GimmickArchive 
    {
        public MainCommand[] controlCommand { get; private set; }   // �Ǘ��R�}���h
        private MainCommand[] playCommand;                          // ���s�R�}���h
        private Vector3 position;                                   // ���W
        private Quaternion rotation;                                // ��]
        private Vector3 scale;                                      // �傫��
        private CommandState state;                                 // ���s���
        private int playIndex;                                      // ���s�C���f�b�N�X

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="control">�Ǘ��R�}���h</param>
        /// <param name="play">���s�R�}���h</param>
        /// <param name="transform">���W</param>
        /// <param name="state">���s���</param>
        /// <param name="playIndex">���s�C���f�b�N�X</param>
        public GimmickArchive(MainCommand[] control,MainCommand[] play,Transform transform,CommandState state,int playIndex)
        {
            MainCommand[] controlCopy = new MainCommand[control.Length];    // �Ǘ��R�}���h��ۑ����邽�߂̔z����쐬
            MainCommand[] playCopy = new MainCommand[play.Length];          // ���s�R�}���h��ۑ����邽�߂̔z����쐬

            // �Ǘ��R�}���h�̓��e���R�s�[���Ċi�[
            for (int i = 0;i < control.Length;i++)
            {
                controlCopy[i] = control[i] != null ? control[i].MainCommandClone() : default;
            }

            // ���s�R�}���h�̓��e���R�s�[���Ċi�[
            for (int i = 0;i < play.Length;i++)
            {
                playCopy[i] = play[i] != null ? play[i].MainCommandClone() : default;
            }

            // �e���ڂ��L�^
            controlCommand = controlCopy;
            playCommand = playCopy;
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
            this.state = state;
            this.playIndex = playIndex;
        }

        /// <summary>
        /// �e���ڂ��L�^����Ă�����e�ɏ���������֐�
        /// </summary>
        /// <param name="control">�Ǘ��R�}���h</param>
        /// <param name="play">���s�R�}���h</param>
        /// <param name="transform">���W</param>
        /// <param name="state">���s���</param>
        /// <param name="playIndex">���s�C���f�b�N�X</param>
        public void SetGimmickArchive(MainCommand[] control,MainCommand[] play,Transform transform,out CommandState state,out int playIndex)
        {
            // �Ǘ��R�}���h�ɋL�^���e�̃R�s�[��n��
            for (int i = 0;i < controlCommand.Length;i++)
            {
                control[i] = controlCommand[i] != null ? controlCommand[i].MainCommandClone() : default;
            }

            // ���s�R�}���h�ɋL�^���e�̃R�s�[��n��
            for (int i = 0;i < playCommand.Length;i++)
            {
                play[i] = playCommand[i] != null ? playCommand[i].MainCommandClone() : default;
            }

            // �e���ڂ�����������
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            state = this.state;
            playIndex = this.playIndex;
        }
    }
}