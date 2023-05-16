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
		/// <param name="interactKind">�C���^���N�g�̎��</param>
        public void ShowUI(ControllerType controllerType, InteractKinds interactKind);

        /// <summary>
		/// UI���\���ɂ���
		/// </summary>
        public void HideUI();
    }
}