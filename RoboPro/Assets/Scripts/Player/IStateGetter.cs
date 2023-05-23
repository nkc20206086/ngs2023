using UnityEngine;

namespace Player
{
    interface IStateGetter
    {
        /// <summary>
        /// Animatorを取得
        /// </summary>
        /// <returns></returns>
        public Animator PlayerAnimatorGeter();

        /// <summary>
        /// Rigidbodyを取得
        /// </summary>
        /// <returns></returns>
        public Rigidbody rigidbodyGetter();

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
        /// 梯子の上り下りのスピードを取得
        /// </summary>
        /// <returns></returns>
        public float LadderUpDownSpeedGetter();

        /// <summary>
        /// ジャンプの高さと幅を取得
        /// </summary>
        /// <returns></returns>
        public Vector2 JumpPowerGetter();
    }
}
