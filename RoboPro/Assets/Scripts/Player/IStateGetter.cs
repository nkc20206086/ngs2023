using Gimmick.Interface;
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
        public Rigidbody RigidbodyGetter();

        /// <summary>
        /// 現在の状態を取得
        /// </summary>
        /// <returns></returns>
        public PlayerStateEnum StateGetter();

        /// <summary>
        /// アクセスポイントの取得
        /// </summary>
        /// <returns></returns>
        public IGimmickAccess GimmickAccessGetter();

        /// <summary>
        /// 床判定クラスの取得
        /// </summary>
        /// <returns></returns>
        public GroundChecker GroundCheckGetter();

        /// <summary>
        /// 梯子判定クラスの取得
        /// </summary>
        /// <returns></returns>
        public LadderChecker LadderCheckGetter();

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
