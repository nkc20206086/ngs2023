using UnityEngine;

namespace Gimmick.Interface
{
    public interface IGimmickAccess
    {
        /// <summary>
        /// �L���͈͂ɂ���A�N�Z�X�|�C���g�̃C���f�b�N�X��Ԃ��܂�
        /// </summary>
        /// <param name="position">�Ώۂ̈ʒu</param>
        /// <returns>���s����(-1�̏ꍇ�͗L���Ȃ��̂͂���܂���)</returns>
        public int GetAccessPointIndex(Vector3 position);

        /// <summary>
        /// �R�}���h����ւ��������s
        /// </summary>
        /// <param name="index">���s�C���f�b�N�X</param>
        public void Access(int index);
    }
}