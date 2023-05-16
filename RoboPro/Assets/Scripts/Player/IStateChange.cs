using System;

namespace Player
{
    interface IStateChange
    {
        /// <summary>
        /// ステート変更イベント
        /// </summary>
        event Action<PlayerStateEnum> stateChangeEvent;
    }
}