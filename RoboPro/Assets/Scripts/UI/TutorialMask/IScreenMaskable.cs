using UnityEngine;
using DG.Tweening;

namespace ScreenMask
{
    interface IScreenMaskable
    {
        /// <summary>
        /// �����ɖ��邢��Ԃɂ���
        /// </summary>
        void SetMaskFadeinMax();

        /// <summary>
        /// DoTween�őS�����邭����
        /// </summary>
        /// <param name="duration">���v����</param>
        /// <param name="ease">�C�[�W���O</param>
        void PlayMaskFadeinMax(float duration, Ease ease = Ease.InOutQuint);

        /// <summary>
        /// ���ڂ������Ƃ���ȊO���Â�����
        /// </summary>
        /// <param name="pos">�L�����o�X�̍��W</param>
        /// <param name="size">�~�̃T�C�Y</param>
        /// <param name="duration">���v����</param>
        /// <param name="ease">�C�[�W���O</param>
        void PlayMaskFadeout(Vector2 pos, Vector2 size, float duration, Ease ease = Ease.InOutQuint);

        /// <summary>
        /// �X�N���[���̐F��ύX����
        /// </summary>
        /// <param name="color">�X�N���[���̐F</param>
        void SetScreenColor(Color color);
    }
}