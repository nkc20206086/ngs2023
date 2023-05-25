using UnityEngine;
using DG.Tweening;

namespace ScreenMask
{
    interface IScreenMaskable
    {
        /// <summary>
        /// すぐに明るい状態にする
        /// </summary>
        void SetMaskFadeinMax();

        /// <summary>
        /// DoTweenで全部明るくする
        /// </summary>
        /// <param name="duration">所要時間</param>
        /// <param name="ease">イージング</param>
        void PlayMaskFadeinMax(float duration, Ease ease = Ease.InOutQuint);

        /// <summary>
        /// 注目したいところ以外を暗くする
        /// </summary>
        /// <param name="pos">キャンバスの座標</param>
        /// <param name="size">円のサイズ</param>
        /// <param name="duration">所要時間</param>
        /// <param name="ease">イージング</param>
        void PlayMaskFadeout(Vector2 pos, Vector2 size, float duration, Ease ease = Ease.InOutQuint);

        /// <summary>
        /// スクリーンの色を変更する
        /// </summary>
        /// <param name="color">スクリーンの色</param>
        void SetScreenColor(Color color);
    }
}