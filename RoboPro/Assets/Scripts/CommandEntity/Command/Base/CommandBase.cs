using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Command;

namespace Command.Entity
{
    /// <summary>
    /// �R�}���h�̃x�[�X�N���X
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// �R�}���h�𕶎���ɕϊ��������̂�Ԃ�
        /// </summary>
        /// <returns>�ϊ���̕�����</returns>
        public abstract string GetString();

        /// <summary>
        /// �R�}���h�^�C�v���擾����֐�
        /// </summary>
        /// <returns></returns>
        public abstract CommandType GetCommandType();

        /// <summary>
        /// �R�}���h�x�[�X�^�Ƃ��ẴR�s�[��Ԃ��N���X
        /// </summary>
        /// <returns></returns>
        public CommandBase BaseClone()
        {
            return (CommandBase)MemberwiseClone();
        }
    }
}