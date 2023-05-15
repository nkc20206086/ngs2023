using UnityEngine;
using System;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// �Z�[�u���N���X
    /// </summary>
    public class SaveArchive
    {
        public int index { get; private set; }                  // ���̃N���X�����C���f�b�N�X���
        public Transform saveTransform { get; private set; }    // �Ώۂ̍��W�l��

        /// <summary>
        /// �R���X�g���N�^(�C���f�b�N�X�A�g�����X�t�H�[���ݒ�p)
        /// </summary>
        /// <param name="index">�ۑ��C���f�b�N�X</param>
        /// <param name="transform">�ۑ��g�����X�t�H�[��</param>
        public SaveArchive(int index, Transform transform)
        {
            // �e�l��ݒ肷��
            this.index = index;
            saveTransform = transform;
        }
    }
}