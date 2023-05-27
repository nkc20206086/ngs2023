using UnityEngine;

namespace InteractUI
{
    public interface IInteractUIControllable
    {
        /// <summary>
		/// UI�̏ꏊ��ݒ肷��
		/// </summary>
		/// <param name="pos">UI�̈ʒu</param>
        public void SetPosition(Vector3 pos);

        /// <summary>
		/// UI��\������
		/// </summary>
		/// <param name="controllerType">�R���g���[���[�̎��</param>
		/// <param name="displayAsset">�C���^���N�g�̎��</param>
        public void ShowUI(ControllerType controllerType, DisplayInteractCanvasAsset displayAsset);

        /// <summary>
		/// UI���\���ɂ���
		/// </summary>
        public void HideUI();

        /// <summary>
        /// UI��FillAmount�𒲐�����
        /// </summary>
        /// <param name="">Fill���銄��</param>
        public void SetFillAmount(float value);

        /// <summary>
        /// �o�c���UI��\������
        /// </summary>
        public void ShowCrossMarkUI();

        /// <summary>
        /// �h�N���̕t�����o�c���\������
        /// </summary>
        public void ShowSkullMark();

        /// <summary>
        /// �o�c���\��UI���\���ɂ���
        /// </summary>
        public void HideLockUI();
    }
}