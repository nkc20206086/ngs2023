using UnityEngine;

namespace Player
{
    interface IStateGetter
    {
        /// <summary>
        /// 現在の状態を取得
        /// </summary>
        /// <returns></returns>
        public PlayerStateEnum StateGetter();

        /// <summary>
        /// 移動スピードを取得
        /// </summary>
        /// <returns></returns>
        public float SpeedGetter();

        /// <summary>
        /// ジャンプの高さと幅を取得
        /// </summary>
        /// <returns></returns>
        public Vector2 JumpPowerGetter();
    }
}
