using System;

namespace Player
{
    interface IStateChange
    {
        /// <summary>
        /// �X�e�[�g�ύX�C�x���g
        /// </summary>
        event Action<PlayerStateEnum> stateChangeEvent;
    }
}